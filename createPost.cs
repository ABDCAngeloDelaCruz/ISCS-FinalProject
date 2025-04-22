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
        }

        private void submitPostBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session.LoggedInUsername))
            {
                MessageBox.Show("You must be logged in to create a post.");
                return;
            }

            string title = titleBox.Text.Trim();
            string content = contentBox.Text.Trim();
            string author = Session.LoggedInUsername;
            string timestamp = DateTime.Now.ToString("o");

            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(content))
            {
                MessageBox.Show("Please enter both title and content.");
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
