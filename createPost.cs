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

namespace FinalProject
{
    public partial class createPost : UserControl
    {
        string IPAddress = "127.0.0.1";

        public createPost()
        {
            InitializeComponent();

            // Set focus to the title box when the control loads
            this.Load += (s, e) => titleBox.Focus();

            // Add placeholder text to the content box
            contentBox.GotFocus += (s, e) => {
                if (contentBox.Text == "Write your post content here...")
                {
                    contentBox.Text = "";
                    contentBox.ForeColor = Color.Black;
                }
            };

            contentBox.LostFocus += (s, e) => {
                if (string.IsNullOrWhiteSpace(contentBox.Text))
                {
                    contentBox.Text = "Write your post content here...";
                    contentBox.ForeColor = Color.Gray;
                }
            };

            // Initialize placeholder text
            contentBox.Text = "Write your post content here...";
            contentBox.ForeColor = Color.Gray;

            // Add hover effect to submit button
            submitPostBtn.MouseEnter += (s, e) => submitPostBtn.BackColor = Color.FromArgb(32, 33, 36);
            submitPostBtn.MouseLeave += (s, e) => submitPostBtn.BackColor = Color.FromArgb(23, 24, 29);
        }

        private void submitPostBtn_Click(object sender, EventArgs e)
        {
            // Check if user is logged in
            if (string.IsNullOrEmpty(Session.LoggedInUsername))
            {
                MessageBox.Show("You must be logged in to create a post.", "Login Required",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Get the post data
            string title = titleBox.Text.Trim();
            string content = contentBox.Text.Trim();

            // Check if content is the placeholder text
            if (content == "Write your post content here...")
            {
                content = "";
            }

            string author = Session.LoggedInUsername;
            string timestamp = DateTime.Now.ToString("o");

            // Validate input
            if (string.IsNullOrEmpty(title))
            {
                MessageBox.Show("Please enter a title for your post.", "Missing Title",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                titleBox.Focus();
                return;
            }

            if (string.IsNullOrEmpty(content))
            {
                MessageBox.Show("Please enter content for your post.", "Missing Content",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                contentBox.Focus();
                contentBox.Text = "";
                contentBox.ForeColor = Color.Black;
                return;
            }

            var request = new
            {
                action = "createPost",
                title = title,
                content = content,
                author = author,
                timestamp = timestamp
            };

            try
            {
                using (TcpClient client = new TcpClient(IPAddress, 8888))
                using (NetworkStream stream = client.GetStream())
                using (StreamWriter writer = new StreamWriter(stream) { AutoFlush = true })
                using (StreamReader reader = new StreamReader(stream))
                {
                    string json = System.Text.Json.JsonSerializer.Serialize(request);
                    writer.WriteLine(json);

                    string responseJson = reader.ReadLine();
                    var response = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(responseJson);

                    if (response["status"] == "success")
                    {
                        MessageBox.Show("Post created successfully!");
                        titleBox.Text = "";
                        contentBox.Text = "";
                    }
                    else
                    {
                        MessageBox.Show(response["message"]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

            XmlElement postElement = doc.CreateElement("post");

            // Add a unique ID attribute to the post
            XmlAttribute idAttr = doc.CreateAttribute("id");
            idAttr.Value = Guid.NewGuid().ToString();
            postElement.Attributes.Append(idAttr);

            XmlElement titleElement = doc.CreateElement("title");
            titleElement.InnerText = title;
            postElement.AppendChild(titleElement);

            XmlElement contentElement = doc.CreateElement("content");
            contentElement.InnerText = content;
            postElement.AppendChild(contentElement);

            XmlElement authorElement = doc.CreateElement("author");
            authorElement.InnerText = author;
            postElement.AppendChild(authorElement);

            XmlElement timestampElement = doc.CreateElement("timestamp");
            timestampElement.InnerText = timestamp;
            postElement.AppendChild(timestampElement);

            // Create an empty comments section
            XmlElement commentsElement = doc.CreateElement("comments");
            postElement.AppendChild(commentsElement);

            postsNode.AppendChild(postElement);
            doc.Save(path);

            // Show success message
            MessageBox.Show("Your post has been created successfully!", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Reset form fields
            titleBox.Text = "";
            contentBox.Text = "Write your post content here...";
            contentBox.ForeColor = Color.Gray;

            // Set focus back to title for a new post
            titleBox.Focus();


        }
    }
}
