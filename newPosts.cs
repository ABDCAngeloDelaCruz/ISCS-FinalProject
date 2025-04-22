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

namespace FinalProject
{
    public partial class newPosts : UserControl
    {
        XmlDocument doc;
        XmlElement root;
        static string relativePath = @"XMLFiles\data.xml";
        string path = Path.Combine(Environment.CurrentDirectory, relativePath);

        public newPosts()
        {
            InitializeComponent();
            newPosts_Load(this, EventArgs.Empty);
        }

        public void newPosts_Load(object sender, EventArgs e)
        {
            try
            {
                doc = new XmlDocument();
                doc.Load(path);
                root = doc.DocumentElement;

                if (root != null)
                {
                    XmlNodeList posts = root.SelectNodes("post");
                    foreach (XmlNode post in posts)
                    {
                        string title = post["title"].InnerText;
                        string content = post["content"].InnerText;
                        string author = post["author"].InnerText;
                        string timestamp = post["timestamp"].InnerText;
                    }
                }
                else
                {
                    MessageBox.Show("No data found.");
                }

                LoadPosts();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading XML: {ex.Message}");
            }
        }

        public void LoadPosts()
        {
            postContainer.Controls.Clear();

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNodeList posts = doc.SelectNodes("/data/posts/post");

                var sortedPosts = posts
                    .Cast<XmlNode>()
                    .OrderByDescending(p => DateTime.Parse(p["timestamp"].InnerText));

                foreach (XmlNode post in sortedPosts)
                {
                    string title = post["title"].InnerText;
                    string content = post["content"].InnerText;
                    string author = post["author"].InnerText;
                    string timestamp = post["timestamp"].InnerText;

                    // Get or create post ID
                    string postId;
                    XmlAttribute idAttr = post.Attributes["id"];

                    if (idAttr == null)
                    {
                        // Create a new ID for this post
                        postId = Guid.NewGuid().ToString();
                        XmlAttribute newIdAttr = doc.CreateAttribute("id");
                        newIdAttr.Value = postId;
                        post.Attributes.Append(newIdAttr);
                        doc.Save(path);
                    }
                    else
                    {
                        postId = idAttr.Value;
                    }

                    Panel postCardClone = ClonePostCard();
                    postCardClone.Visible = true;
                    postCardClone.Tag = postId; // Store post ID in the Tag property

                    foreach (Control control in postCardClone.Controls)
                    {
                        if (control.Name == "postTitle")
                            control.Text = title;
                        else if (control.Name == "postConts")
                            control.Text = content;
                        else if (control.Name == "postMeta")
                            control.Text = $"Posted by {author} • {DateTime.Parse(timestamp):g}";
                    }

                    postContainer.Controls.Add(postCardClone);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading posts: {ex.Message}");
            }
        }


        private Panel ClonePostCard()
        {
            Panel newPanel = new Panel
            {
                Size = postCard.Size,
                BackColor = postCard.BackColor,
                BorderStyle = postCard.BorderStyle,
                Margin = postCard.Margin,
                Cursor = Cursors.Hand,
                Padding = postCard.Padding
            };

            foreach (Control ctrl in postCard.Controls)
            {
                Control newCtrl = (Control)Activator.CreateInstance(ctrl.GetType());
                newCtrl.Text = ctrl.Text;
                newCtrl.Size = ctrl.Size;
                newCtrl.Location = ctrl.Location;
                newCtrl.Font = ctrl.Font;
                newCtrl.Name = ctrl.Name;
                newCtrl.Cursor = Cursors.Hand;
                newPanel.Controls.Add(newCtrl);
            }

            // Add click event to the panel
            newPanel.Click += PostCard_Click;

            // Add hover effects
            newPanel.MouseEnter += (sender, e) => {
                newPanel.BackColor = Color.FromArgb(248, 249, 250);
            };

            newPanel.MouseLeave += (sender, e) => {
                newPanel.BackColor = postCard.BackColor;
            };

            // Add click event to all controls inside the panel
            foreach (Control ctrl in newPanel.Controls)
            {
                ctrl.Click += (sender, e) => PostCard_Click(newPanel, e);
                ctrl.MouseEnter += (sender, e) => newPanel.BackColor = Color.FromArgb(248, 249, 250);
                ctrl.MouseLeave += (sender, e) => newPanel.BackColor = postCard.BackColor;
            }

            return newPanel;
        }

        private void PostCard_Click(object sender, EventArgs e)
        {
            if (sender is Panel panel)
            {
                // Get the post ID from the panel's Tag property
                string postId = panel.Tag.ToString();

                // Navigate to post detail view
                Form1 mainForm = (Form1)this.FindForm();
                mainForm.LoadPostDetail(postId);
            }
        }
    }
}
