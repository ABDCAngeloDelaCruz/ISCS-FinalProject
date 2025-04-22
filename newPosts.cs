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
using System.Net.Sockets;
using System.Text.Json;

namespace FinalProject
{
    public partial class newPosts : UserControl
    {
        string IPAddress = "127.0.0.1";

        public newPosts()
        {
            InitializeComponent();
            LoadPosts();
        }

        public void LoadPosts()
        {
            postContainer.Controls.Clear();

            var posts = NewPostsFromServer();

            var sortedPosts = posts
                .OrderByDescending(p => DateTime.Parse(p["timestamp"]));

            foreach (var post in sortedPosts)
            {
                Panel postCardClone = ClonePostCard();
                postCardClone.Visible = true;
                postCardClone.Tag = post["id"];

                foreach (Control control in postCardClone.Controls)
                {
                    if (control.Name == "postTitle")
                        control.Text = post["title"];
                    else if (control.Name == "postConts")
                        control.Text = post["content"];
                    else if (control.Name == "postMeta")
                        control.Text = $"Posted by {post["author"]} • {DateTime.Parse(post["timestamp"]):g}";
                }

                postContainer.Controls.Add(postCardClone);
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

        private List<Dictionary<string, string>> NewPostsFromServer()
        {
            List<Dictionary<string, string>> posts = new();

            try
            {
                TcpClient client = new TcpClient(IPAddress, 8888);
                using NetworkStream stream = client.GetStream();
                using StreamWriter writer = new StreamWriter(stream) { AutoFlush = true };
                using StreamReader reader = new StreamReader(stream);

                var request = new { action = "getPosts" };
                string jsonRequest = JsonSerializer.Serialize(request);
                writer.WriteLine(jsonRequest);

                string jsonResponse = reader.ReadLine();
                var doc = JsonDocument.Parse(jsonResponse);
                var root = doc.RootElement;

                if (root.GetProperty("status").GetString() == "success")
                {
                    foreach (var post in root.GetProperty("posts").EnumerateArray())
                    {
                        posts.Add(new Dictionary<string, string>
                {
                    { "id", post.GetProperty("id").GetString() },
                    { "title", post.GetProperty("title").GetString() },
                    { "content", post.GetProperty("content").GetString() },
                    { "author", post.GetProperty("author").GetString() },
                    { "timestamp", post.GetProperty("timestamp").GetString() }
                });
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
