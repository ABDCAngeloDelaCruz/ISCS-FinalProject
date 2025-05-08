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
        private readonly string IPAddress = "127.0.0.1";
        private static readonly string relativePath = @"XMLFiles\data.xml";
        private readonly string path = @"C:\Users\Zygos\Documents\ISCS\FinalProject\ForumServer\bin\Debug\net9.0\XMLFiles\data.xml";
        private XmlDocument? doc;
        private XmlElement? root;

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




        }
    }
}
