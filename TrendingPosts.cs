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
    public partial class TrendingPosts : UserControl
    {
        XmlDocument doc;
        XmlElement root;
        static string relativePath = @"XMLFiles\data.xml";
        string path = Path.Combine(Environment.CurrentDirectory, relativePath);

        public TrendingPosts()
        {
            InitializeComponent();
            this.Resize += TrendingPosts_Resize;

            // Make sure the template post card is not visible
            postCard.Visible = false;

            // Set up the post card template
            SetupPostCardTemplate();

            // Load posts after initialization
            this.Load += (s, e) => {
                LoadPosts();
            };

            // Also load posts immediately
            TrendingPosts_Load(this, EventArgs.Empty);
        }

        private void SetupPostCardTemplate()
        {
            // Ensure the template post card is properly configured
            postCard.Size = new Size(800, 150);
            postCard.Visible = false;

            // Configure the labels in the template
            foreach (Control ctrl in postCard.Controls)
            {
                if (ctrl.Name == "postTitle")
                {
                    ctrl.Location = new Point(15, 15);
                    ctrl.Size = new Size(770, 25);
                    ctrl.Font = new Font("Segoe UI", 14, FontStyle.Bold);
                }
                else if (ctrl.Name == "postConts")
                {
                    ctrl.Location = new Point(15, 45);
                    ctrl.Size = new Size(770, 60);
                }
                else if (ctrl.Name == "postMeta")
                {
                    ctrl.Location = new Point(15, 110);
                    ctrl.Size = new Size(770, 20);
                }
            }
        }

        private void TrendingPosts_Resize(object sender, EventArgs e)
        {
            // Update post card widths when the control is resized
            foreach (Control control in postContainer.Controls)
            {
                if (control is Panel panel && panel != postCard) // Skip the template
                {
                    // Set a fixed width that's wider than before
                    int newWidth = Math.Max(800, postContainer.Width - 50);
                    panel.Width = newWidth;
                    panel.MinimumSize = new Size(800, panel.Height);
                    panel.MaximumSize = new Size(1200, 0);

                    // Update the width of labels inside the panel
                    foreach (Control childControl in panel.Controls)
                    {
                        if (childControl is Label label)
                        {
                            if (label.Name == "postTitle" || label.Name == "postConts" || label.Name == "postMeta")
                            {
                                label.Width = panel.Width - 40;
                                label.MaximumSize = new Size(panel.Width - 40, label.MaximumSize.Height);

                                // Make sure the label is visible
                                label.Visible = true;

                                // Ensure proper positioning
                                if (label.Name == "postTitle")
                                {
                                    label.Location = new Point(15, 15);
                                }
                                else if (label.Name == "postConts")
                                {
                                    label.Location = new Point(15, 45);
                                }
                                else if (label.Name == "postMeta")
                                {
                                    label.Location = new Point(15, 110);
                                }
                            }
                        }
                    }
                }
            }

            // Force a refresh of the layout
            postContainer.PerformLayout();
        }

        public void TrendingPosts_Load(object sender, EventArgs e)
        {
            try
            {
                doc = new XmlDocument();
                doc.Load(path);
                root = doc.DocumentElement;

                // Ensure the post container is visible and properly sized
                postContainer.Visible = true;
                postContainer.BringToFront();

                // Make sure the template post card is not visible
                if (postCard != null)
                {
                    postCard.Visible = false;
                }

                // Load the posts
                LoadPosts();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading XML: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LoadPosts()
        {
            postContainer.Controls.Clear();

            // No debug label needed

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                // Make sure we're using the correct XPath query
                XmlNodeList posts = doc.SelectNodes("/data/posts/post");

                // If no posts found, try a different XPath query
                if (posts == null || posts.Count == 0)
                {
                    posts = doc.SelectNodes("//post");
                }

                // No debug label needed

                // Add more debug information
                Console.WriteLine($"Found {posts?.Count ?? 0} posts in XML file.");
                if (posts != null)
                {
                    foreach (XmlNode post in posts)
                    {
                        Console.WriteLine($"Post: {post["title"]?.InnerText ?? "No title"}");
                    }
                }

                if (posts == null || posts.Count == 0)
                {
                    Label noPostsLabel = new Label
                    {
                        Text = "No posts found. Create a new post to get started!",
                        AutoSize = true,
                        Font = new Font("Segoe UI", 12, FontStyle.Regular),
                        ForeColor = Color.Gray,
                        Margin = new Padding(20),
                        Dock = DockStyle.Top
                    };
                    postContainer.Controls.Add(noPostsLabel);
                    return;
                }

                // Sort posts by comment count (most comments first)
                var sortedPosts = posts
                    .Cast<XmlNode>()
                    .Select(post => new
                    {
                        Node = post,
                        CommentCount = post.SelectNodes("comments/comment")?.Count ?? 0
                    })
                    .OrderByDescending(p => p.CommentCount)
                    .Select(p => p.Node);

                foreach (XmlNode post in sortedPosts)
                {
                    try
                    {
                        // Extract post data
                        string title = post["title"]?.InnerText ?? "No Title";
                        string content = post["content"]?.InnerText ?? "No Content";
                        string author = post["author"]?.InnerText ?? "Unknown";
                        string timestamp = post["timestamp"]?.InnerText ?? DateTime.Now.ToString();

                        // Get comment count
                        int commentCount = post.SelectNodes("comments/comment")?.Count ?? 0;

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

                        // Add debug information
                        Console.WriteLine($"Creating trending post card for: {title} (ID: {postId}, Comments: {commentCount})");

                        // Create a new post card from scratch
                        Panel postCard = CreatePostCard(title, content, author, timestamp, postId, commentCount);

                        // Add the post card to the container
                        postContainer.Controls.Add(postCard);

                        // Add debug information
                        Console.WriteLine($"Added trending post card to container: {title}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error creating trending post card: {ex.Message}");
                        MessageBox.Show($"Error creating trending post card: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading posts: {ex.Message}");
            }
        }


        private Panel CreatePostCard(string title, string content, string author, string timestamp, string postId, int commentCount)
        {
            // Calculate a width that's appropriate for the container
            int cardWidth = Math.Max(800, postContainer.Width - 50);

            // Create the panel with a background color based on comment count
            Color panelColor = Color.White;
            if (commentCount > 5)
                panelColor = Color.FromArgb(255, 250, 250, 250); // Slightly different for very popular posts
            else if (commentCount > 2)
                panelColor = Color.FromArgb(255, 252, 252, 252); // Slightly different for popular posts

            Panel panel = new Panel
            {
                Size = new Size(cardWidth, 150),
                BackColor = panelColor,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(5, 10, 5, 10),
                Cursor = Cursors.Hand,
                Padding = new Padding(15),
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top,
                Width = cardWidth,
                MinimumSize = new Size(800, 150),
                MaximumSize = new Size(1200, 150),
                Visible = true,
                AutoSize = false,
                Height = 150,
                Tag = postId // Store post ID in the Tag property
            };

            // Create the title label
            Label titleLabel = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Location = new Point(15, 15),
                Size = new Size(cardWidth - 40, 25),
                MaximumSize = new Size(cardWidth - 40, 25),
                AutoSize = false,
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top,
                Name = "postTitle",
                Visible = true
            };

            // Create the content label
            string displayContent = content;
            if (content.Length > 200)
            {
                displayContent = content.Substring(0, 197) + "...";
            }

            Label contentLabel = new Label
            {
                Text = displayContent,
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                Location = new Point(15, 45),
                Size = new Size(cardWidth - 40, 60),
                MaximumSize = new Size(cardWidth - 40, 60),
                AutoSize = false,
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top,
                Name = "postConts",
                Visible = true
            };

            // Create the meta label with comment count
            Label metaLabel = new Label
            {
                Text = $"Posted by {author} • {DateTime.Parse(timestamp):g} • {commentCount} {(commentCount == 1 ? "comment" : "comments")}",
                Font = new Font("Segoe UI", 8, FontStyle.Regular),
                Location = new Point(15, 110),
                Size = new Size(cardWidth - 40, 20),
                MaximumSize = new Size(cardWidth - 40, 20),
                AutoSize = false,
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top,
                Name = "postMeta",
                TextAlign = ContentAlignment.MiddleRight,
                Visible = true
            };

            // Create a comment count badge
            Label commentBadge = new Label
            {
                Text = commentCount.ToString(),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(cardWidth - 50, 15),
                Size = new Size(30, 30),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = commentCount > 5 ? Color.FromArgb(23, 24, 29) :
                           commentCount > 2 ? Color.FromArgb(32, 33, 36) :
                           Color.FromArgb(50, 50, 55),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.None,
                Visible = true
            };

            // Make the badge circular
            commentBadge.Paint += (sender, e) => {
                System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                path.AddEllipse(0, 0, commentBadge.Width, commentBadge.Height);
                commentBadge.Region = new Region(path);
            };

            // Add the labels to the panel
            panel.Controls.Add(titleLabel);
            panel.Controls.Add(contentLabel);
            panel.Controls.Add(metaLabel);
            panel.Controls.Add(commentBadge);

            // Add click event to the panel
            panel.Click += PostCard_Click;

            // Store the original color for hover effects
            Color originalColor = panel.BackColor;
            Color hoverColor = Color.FromArgb(
                Math.Min(255, originalColor.R + 10),
                Math.Min(255, originalColor.G + 10),
                Math.Min(255, originalColor.B + 10));

            // Add hover effects
            panel.MouseEnter += (sender, e) => {
                panel.BackColor = hoverColor;
            };

            panel.MouseLeave += (sender, e) => {
                panel.BackColor = originalColor;
            };

            // Add click event to all controls inside the panel
            foreach (Control ctrl in panel.Controls)
            {
                ctrl.Click += (sender, e) => PostCard_Click(panel, e);
                ctrl.MouseEnter += (sender, e) => panel.BackColor = hoverColor;
                ctrl.MouseLeave += (sender, e) => panel.BackColor = originalColor;
            }

            return panel;
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
