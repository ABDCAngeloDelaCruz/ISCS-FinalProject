using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Drawing.Text;

namespace FinalProject
{
    public partial class PostDetail : UserControl
    {
        private XmlDocument doc;
        private XmlElement root;
        private static string relativePath = @"XMLFiles\data.xml";
        private string path = Path.Combine(Environment.CurrentDirectory, relativePath);
        private string postId;

        public PostDetail(string postId)
        {
            InitializeComponent();
            this.postId = postId;
            LoadPostDetails();
        }

        private void LoadPostDetails()
        {
            try
            {
                doc = new XmlDocument();
                doc.Load(path);
                root = doc.DocumentElement;

                XmlNode post = root.SelectSingleNode($"/data/posts/post[@id='{postId}']");

                if (post != null)
                {
                    // Display post details
                    lblTitle.Text = post["title"].InnerText;
                    lblContent.Text = post["content"].InnerText;
                    lblAuthor.Text = $"Posted by {post["author"].InnerText}";
                    lblTimestamp.Text = $"{DateTime.Parse(post["timestamp"].InnerText):g}";

                    // Adjust the position of author and timestamp based on content height
                    AdjustControlPositions();

                    // Load comments
                    LoadComments(post);
                }
                else
                {
                    MessageBox.Show("Post not found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading post details: {ex.Message}");
            }
        }

        private void AdjustControlPositions()
        {
            // Calculate the height of the content text
            int contentHeight = TextRenderer.MeasureText(lblContent.Text, lblContent.Font,
                                new Size(lblContent.MaximumSize.Width, 0),
                                TextFormatFlags.WordBreak).Height;

            // Adjust the position of author and timestamp labels
            lblAuthor.Location = new Point(lblAuthor.Location.X, lblContent.Location.Y + contentHeight + 20);
            lblTimestamp.Location = new Point(lblTimestamp.Location.X, lblAuthor.Location.Y + lblAuthor.Height + 5);

            // Adjust the position of comments section
            lblComments.Location = new Point(lblComments.Location.X, lblTimestamp.Location.Y + lblTimestamp.Height + 20);
            commentsPanel.Location = new Point(commentsPanel.Location.X, lblComments.Location.Y + lblComments.Height + 10);

            // Adjust the position of comment input
            txtComment.Location = new Point(txtComment.Location.X, commentsPanel.Location.Y + commentsPanel.Height + 20);
            btnAddComment.Location = new Point(btnAddComment.Location.X, txtComment.Location.Y);
        }

        private void LoadComments(XmlNode post)
        {
            commentsPanel.Controls.Clear();

            XmlNodeList comments = post.SelectNodes("comments/comment");

            if (comments != null && comments.Count > 0)
            {
                foreach (XmlNode comment in comments)
                {
                    string commentText = comment["text"].InnerText;
                    string commentAuthor = comment["author"].InnerText;
                    string commentTimestamp = comment["timestamp"].InnerText;

                    Panel commentPanel = CreateCommentPanel(commentText, commentAuthor, commentTimestamp);
                    commentsPanel.Controls.Add(commentPanel);
                }
            }
            else
            {
                Panel noCommentsPanel = new Panel
                {
                    Width = commentsPanel.Width - 25,
                    Height = 60,
                    BackColor = Color.FromArgb(248, 249, 250),
                    BorderStyle = BorderStyle.None,
                    Margin = new Padding(0, 0, 0, 10),
                    Padding = new Padding(10)
                };

                Label noCommentsLabel = new Label
                {
                    Text = "No comments yet. Be the first to comment!",
                    AutoSize = true,
                    Font = new Font("Segoe UI", 10, FontStyle.Italic),
                    ForeColor = Color.Gray,
                    Location = new Point(10, 20)
                };

                noCommentsPanel.Controls.Add(noCommentsLabel);
                commentsPanel.Controls.Add(noCommentsPanel);
            }
        }

        private Panel CreateCommentPanel(string text, string author, string timestamp)
        {
            Panel panel = new Panel
            {
                Width = commentsPanel.Width - 25,
                BackColor = Color.FromArgb(248, 249, 250),
                BorderStyle = BorderStyle.None,
                Margin = new Padding(0, 0, 0, 10),
                Padding = new Padding(10)
            };

            Label commentText = new Label
            {
                Text = text,
                AutoSize = true,
                MaximumSize = new Size(panel.Width - 20, 0),
                Location = new Point(10, 10),
                Font = new Font("Segoe UI", 10)
            };

            Label commentMeta = new Label
            {
                Text = $"Comment by {author} • {DateTime.Parse(timestamp):g}",
                AutoSize = true,
                ForeColor = Color.Gray,
                Font = new Font("Segoe UI", 8),
                Location = new Point(10, commentText.Bottom + 5)
            };

            panel.Controls.Add(commentText);
            panel.Controls.Add(commentMeta);

            // Calculate the height based on the content
            int textHeight = TextRenderer.MeasureText(text, commentText.Font,
                             new Size(commentText.MaximumSize.Width, 0),
                             TextFormatFlags.WordBreak).Height;

            commentText.Height = textHeight;
            commentMeta.Location = new Point(10, commentText.Top + textHeight + 5);
            panel.Height = commentMeta.Bottom + 10;

            return panel;
        }

        private void btnAddComment_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session.LoggedInUsername))
            {
                MessageBox.Show("You must be logged in to add a comment.");
                return;
            }

            string commentText = txtComment.Text.Trim();
            if (string.IsNullOrEmpty(commentText))
            {
                MessageBox.Show("Please enter a comment.");
                return;
            }

            try
            {
                XmlNode post = root.SelectSingleNode($"/data/posts/post[@id='{postId}']");
                if (post != null)
                {
                    // Find or create comments section
                    XmlNode commentsNode = post.SelectSingleNode("comments");
                    if (commentsNode == null)
                    {
                        commentsNode = doc.CreateElement("comments");
                        post.AppendChild(commentsNode);
                    }

                    // Create new comment
                    XmlElement commentElement = doc.CreateElement("comment");

                    XmlElement textElement = doc.CreateElement("text");
                    textElement.InnerText = commentText;
                    commentElement.AppendChild(textElement);

                    XmlElement authorElement = doc.CreateElement("author");
                    authorElement.InnerText = Session.LoggedInUsername;
                    commentElement.AppendChild(authorElement);

                    XmlElement timestampElement = doc.CreateElement("timestamp");
                    timestampElement.InnerText = DateTime.Now.ToString("o");
                    commentElement.AppendChild(timestampElement);

                    commentsNode.AppendChild(commentElement);
                    doc.Save(path);

                    // Clear comment box and reload comments
                    txtComment.Text = "";
                    LoadComments(post);

                    MessageBox.Show("Comment added successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding comment: {ex.Message}");
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            // Navigate back to posts list
            Form1 mainForm = (Form1)this.FindForm();
            mainForm.LoadView(new newPosts());
        }
    }
}
