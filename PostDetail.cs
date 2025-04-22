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
using System.Runtime.InteropServices;

namespace FinalProject
{
    // Using the GraphicsPathExtensions class from newPosts.cs

    public partial class PostDetail : UserControl
    {
        // Import the CreateRoundRectRgn function from the Windows API
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );
        private XmlDocument doc;
        private XmlElement root;
        private static string relativePath = @"XMLFiles\data.xml";
        private string path = Path.Combine(Environment.CurrentDirectory, relativePath);
        private string postId;

        public PostDetail(string postId)
        {
            InitializeComponent();
            this.postId = postId;
            this.Resize += PostDetail_Resize;

            // Add hover effects to buttons
            btnAddComment.MouseEnter += (s, e) => btnAddComment.BackColor = Color.FromArgb(90, 90, 130);
            btnAddComment.MouseLeave += (s, e) => btnAddComment.BackColor = Color.FromArgb(80, 80, 120);
            btnBack.MouseEnter += (s, e) => btnBack.BackColor = Color.FromArgb(70, 70, 90);
            btnBack.MouseLeave += (s, e) => btnBack.BackColor = Color.FromArgb(60, 60, 80);

            LoadPostDetails();
        }

        private void PostDetail_Resize(object sender, EventArgs e)
        {
            AdjustControlPositions();

            // Update comment panel widths when the control is resized
            foreach (Control control in commentsPanel.Controls)
            {
                if (control is Panel panel)
                {
                    panel.Width = commentsPanel.Width - 25;
                    panel.MinimumSize = new Size(600, 0);
                    panel.MaximumSize = new Size(2000, 0); // Allow very wide panels

                    // Update the region for rounded corners
                    panel.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, panel.Width, panel.Height, 10, 10));

                    // Update the width of labels inside the panel
                    foreach (Control childControl in panel.Controls)
                    {
                        if (childControl is Label label && label.Name != "commentMeta")
                        {
                            label.MaximumSize = new Size(panel.Width - 40, 0); // Use panel width minus padding
                        }
                    }
                }
            }

            // Force a refresh of the layout
            commentsPanel.PerformLayout();
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
            // Calculate the height of the content text - use full width of the window
            int contentWidth = this.Width - 80; // Leave some padding on the sides
            int contentHeight = TextRenderer.MeasureText(lblContent.Text, lblContent.Font,
                                new Size(contentWidth, 0),
                                TextFormatFlags.WordBreak).Height;

            // Update the maximum width of the content label to match the panel width
            lblContent.MaximumSize = new Size(contentWidth, 0);

            // Make sure the title can also use the full width
            lblTitle.MaximumSize = new Size(contentWidth, 0);

            // Update the styling of the content and title
            lblTitle.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(40, 40, 55);
            lblContent.Font = new Font("Segoe UI", 12, FontStyle.Regular);
            lblContent.ForeColor = Color.FromArgb(60, 60, 80);
            lblAuthor.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            lblAuthor.ForeColor = Color.FromArgb(100, 100, 120);
            lblTimestamp.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            lblTimestamp.ForeColor = Color.FromArgb(100, 100, 120);
            lblComments.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblComments.ForeColor = Color.FromArgb(40, 40, 55);

            // Adjust the position of author and timestamp labels
            lblAuthor.Location = new Point(lblAuthor.Location.X, lblContent.Location.Y + contentHeight + 20);
            lblTimestamp.Location = new Point(lblTimestamp.Location.X, lblAuthor.Location.Y + lblAuthor.Height + 5);

            // Adjust the position of comments section
            lblComments.Location = new Point(lblComments.Location.X, lblTimestamp.Location.Y + lblTimestamp.Height + 20);
            commentsPanel.Location = new Point(commentsPanel.Location.X, lblComments.Location.Y + lblComments.Height + 10);

            // Set the width of the comments panel to use full width of the window
            commentsPanel.Width = this.Width - 60;

            // Set a fixed height for the comments panel to prevent it from expanding too much
            commentsPanel.Height = 200;
            commentsPanel.MinimumSize = new Size(600, 200);
            commentsPanel.MaximumSize = new Size(2000, 200); // Increase max width to allow for larger windows

            // Adjust the position of comment input - fixed position at the bottom
            int commentY = commentsPanel.Location.Y + commentsPanel.Height + 20;
            txtComment.Location = new Point(30, commentY);
            btnAddComment.Location = new Point(this.Width - btnAddComment.Width - 30, commentY);
            txtComment.Width = this.Width - btnAddComment.Width - 60; // Use full width minus button width and padding

            // Style the back button
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.BackColor = Color.FromArgb(60, 60, 80);
            btnBack.ForeColor = Color.White;
            btnBack.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            // Make sure the comment controls are always visible
            txtComment.BringToFront();
            btnAddComment.BringToFront();
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
                    BackColor = Color.White,
                    BorderStyle = BorderStyle.None,
                    Margin = new Padding(0, 0, 0, 15),
                    Padding = new Padding(15),
                    Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top,
                    MinimumSize = new Size(600, 60),
                    MaximumSize = new Size(2000, 60) // Allow very wide panels
                };

                // Apply rounded corners
                noCommentsPanel.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, noCommentsPanel.Width, noCommentsPanel.Height, 10, 10));

                // Add shadow effect using custom paint event
                noCommentsPanel.Paint += (s, e) => {
                    // Draw shadow
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    Rectangle rect = new Rectangle(0, 0, noCommentsPanel.Width, noCommentsPanel.Height);
                    using (System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath())
                    {
                        path.AddRoundedRectangle(rect, 10);
                        using (Pen pen = new Pen(Color.FromArgb(20, 0, 0, 0), 1))
                        {
                            e.Graphics.DrawPath(pen, path);
                        }
                    }
                };

                Label noCommentsLabel = new Label
                {
                    Text = "No comments yet. Be the first to comment!",
                    AutoSize = true,
                    Font = new Font("Segoe UI", 11, FontStyle.Italic),
                    ForeColor = Color.FromArgb(100, 100, 120),
                    Location = new Point(15, 20)
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
                MinimumSize = new Size(600, 0),
                MaximumSize = new Size(2000, 0), // Allow very wide panels
                BackColor = Color.White,
                BorderStyle = BorderStyle.None,
                Margin = new Padding(0, 0, 0, 15),
                Padding = new Padding(15),
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top
            };

            // Apply rounded corners
            panel.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, panel.Width, panel.Height, 10, 10));

            // Add shadow effect using custom paint event
            panel.Paint += (s, e) => {
                // Draw shadow
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                Rectangle rect = new Rectangle(0, 0, panel.Width, panel.Height);
                using (System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath())
                {
                    path.AddRoundedRectangle(rect, 10);
                    using (Pen pen = new Pen(Color.FromArgb(20, 0, 0, 0), 1))
                    {
                        e.Graphics.DrawPath(pen, path);
                    }
                }
            };

            Label commentText = new Label
            {
                Text = text,
                AutoSize = true,
                MaximumSize = new Size(panel.Width - 40, 0), // Use panel width minus padding
                Location = new Point(15, 15),
                Font = new Font("Segoe UI", 11),
                ForeColor = Color.FromArgb(60, 60, 80),
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top
            };

            Label commentMeta = new Label
            {
                Text = $"Comment by {author} • {DateTime.Parse(timestamp):g}",
                AutoSize = true,
                ForeColor = Color.FromArgb(100, 100, 120),
                Font = new Font("Segoe UI", 9),
                Location = new Point(15, commentText.Bottom + 10)
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
