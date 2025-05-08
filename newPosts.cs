using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Net.Sockets;
using System.Text.Json;
using System.IO;

namespace FinalProject
{
    public partial class newPosts : UserControl
    {
        private readonly string IPAddress = "127.0.0.1";
        private XmlDocument? doc;
        private readonly string path = @"C:\Users\Zygos\Documents\ISCS\FinalProject\ForumServer\bin\Debug\net9.0\XMLFiles\data.xml";

        public newPosts()
        {
            InitializeComponent();
            LoadPosts();
            Resize += NewPosts_Resize;

            // Make sure the template post card is not visible
            postCard.Visible = false;

            // Set up the post card template
            SetupPostCardTemplate();

            // Load posts after initialization
            Load += (s, e) => {
                LoadPosts();
            };

            // Also load posts immediately
            NewPosts_Load(this, EventArgs.Empty);
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

        private void NewPosts_Resize(object? sender, EventArgs e)
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
                    panel.MaximumSize = new Size(1200, 0); // 0 for height means no maximum height

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

        public void NewPosts_Load(object? sender, EventArgs e)
        {
            try
            {
                doc = new XmlDocument();
                doc.Load(path);

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

            // Try to load posts from server first
            var posts = NewPostsFromServer();

            if (posts.Count > 0)
            {
                // Sort posts by timestamp (most recent first)
                var sortedPosts = posts
                    .OrderByDescending(p => DateTime.Parse(p["timestamp"]));

                // Create post cards for each post
                foreach (var post in sortedPosts)
                {
                    try
                    {
                        // Create a new post card
                        Panel postCard = CreatePostCard(
                            post["title"],
                            post["content"],
                            post["author"],
                            post["timestamp"],
                            post["id"]
                        );

                        // Add the post card to the container
                        postContainer.Controls.Add(postCard);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error creating post card: {ex.Message}");
                    }
                }
                return;
            }

            // Fallback to XML if server posts failed or returned empty
            try
            {
                XmlDocument xmlDoc = new();
                xmlDoc.Load(path);

                // Make sure we're using the correct XPath query
                XmlNodeList? xmlPosts = xmlDoc.SelectNodes("/data/posts/post");

                // If no posts found, try a different XPath query
                if (xmlPosts == null || xmlPosts.Count == 0)
                {
                    xmlPosts = xmlDoc.SelectNodes("//post");
                }

                // If still no posts, show a message
                if (xmlPosts == null || xmlPosts.Count == 0)
                {
                    Label noPostsLabel = new()
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

                // Sort posts by timestamp
                var sortedXmlPosts = xmlPosts
                    .Cast<XmlNode>()
                    .OrderByDescending(p => DateTime.Parse(p["timestamp"]?.InnerText ?? DateTime.Now.ToString()));

                // Create post cards for each post
                foreach (XmlNode xmlPost in sortedXmlPosts)
                {
                    try
                    {
                        // Extract post data
                        string title = xmlPost["title"]?.InnerText ?? "No Title";
                        string content = xmlPost["content"]?.InnerText ?? "No Content";
                        string author = xmlPost["author"]?.InnerText ?? "Unknown";
                        string timestamp = xmlPost["timestamp"]?.InnerText ?? DateTime.Now.ToString();

                        // Get or create post ID
                        string postId;
                        XmlAttribute? idAttr = xmlPost.Attributes?["id"];

                        if (idAttr == null)
                        {
                            // Create a new ID for this post
                            postId = Guid.NewGuid().ToString();
                            XmlAttribute newIdAttr = xmlDoc.CreateAttribute("id");
                            newIdAttr.Value = postId;
                            xmlPost.Attributes?.Append(newIdAttr);
                            xmlDoc.Save(path);
                        }
                        else
                        {
                            postId = idAttr.Value;
                        }

                        // Create a new post card
                        Panel postCard = CreatePostCard(title, content, author, timestamp, postId);

                        // Add the post card to the container
                        postContainer.Controls.Add(postCard);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error creating post card: {ex.Message}");
                        MessageBox.Show($"Error creating post card: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading posts: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private Panel CreatePostCard(string title, string content, string author, string timestamp, string postId)
        {
            // Calculate a width that's appropriate for the container
            int cardWidth = Math.Max(800, postContainer.Width - 50);

            // Create the panel
            Panel panel = new()
            {
                Size = new Size(cardWidth, 150),
                BackColor = Color.White,
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
            Label titleLabel = new()
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
                displayContent = string.Concat(content.AsSpan(0, 197), "...");
            }

            Label contentLabel = new()
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

            // Create the meta label
            Label metaLabel = new()
            {
                Text = $"Posted by {author} • {DateTime.Parse(timestamp):g}",
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

            // Add the labels to the panel
            panel.Controls.Add(titleLabel);
            panel.Controls.Add(contentLabel);
            panel.Controls.Add(metaLabel);

            // Add click event to the panel
            panel.Click += PostCard_Click;

            // Add hover effects
            panel.MouseEnter += (sender, e) => {
                panel.BackColor = Color.FromArgb(248, 249, 250);
            };

            panel.MouseLeave += (sender, e) => {
                panel.BackColor = Color.White;
            };

            // Add click event to all controls inside the panel
            foreach (Control ctrl in panel.Controls)
            {
                ctrl.Click += (sender, e) => PostCard_Click(panel, e);
                ctrl.MouseEnter += (sender, e) => panel.BackColor = Color.FromArgb(248, 249, 250);
                ctrl.MouseLeave += (sender, e) => panel.BackColor = Color.White;
            }

            return panel;
        }

        private void PostCard_Click(object? sender, EventArgs e)
        {
            if (sender is Panel panel)
            {
                // Get the post ID from the panel's Tag property
                string postId = panel.Tag?.ToString() ?? string.Empty;

                // Navigate to post detail view
                if (FindForm() is Form1 mainForm)
                    mainForm.LoadPostDetail(postId);
            }
        }

        private List<Dictionary<string, string>> NewPostsFromServer()
        {
            List<Dictionary<string, string>> posts = [];

            try
            {
                TcpClient client = new(IPAddress, 8888);
                using NetworkStream stream = client.GetStream();
                using StreamWriter writer = new(stream) { AutoFlush = true };
                using StreamReader reader = new(stream);

                var request = new { action = "getPosts" };
                string jsonRequest = JsonSerializer.Serialize(request);
                writer.WriteLine(jsonRequest);

                string? jsonResponse = reader.ReadLine();
                if (jsonResponse != null)
                {
                    var doc = JsonDocument.Parse(jsonResponse);
                    var root = doc.RootElement;

                    if (root.GetProperty("status").GetString() == "success")
                    {
                        foreach (var post in root.GetProperty("posts").EnumerateArray())
                        {
                            posts.Add(new Dictionary<string, string>
                            {
                                { "id", post.GetProperty("id").GetString() ?? string.Empty },
                                { "title", post.GetProperty("title").GetString() ?? string.Empty },
                                { "content", post.GetProperty("content").GetString() ?? string.Empty },
                                { "author", post.GetProperty("author").GetString() ?? string.Empty },
                                { "timestamp", post.GetProperty("timestamp").GetString() ?? DateTime.Now.ToString() }
                            });
                        }
                    }
                }

                client.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to fetch posts: {ex.Message}");
            }

            return posts;
        }
    }
}
