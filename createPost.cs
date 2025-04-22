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

namespace FinalProject
{
    public partial class createPost : UserControl
    {
        XmlDocument doc;
        XmlElement root;
        static string relativePath = @"XMLFiles\data.xml";
        string path = Path.Combine(Environment.CurrentDirectory, relativePath);
        public createPost()
        {
            InitializeComponent();
        }
        public void createPost_Load() {
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
            doc = new XmlDocument();
            doc.Load(path);
            root = doc.DocumentElement;

            XmlNode postsNode = root.SelectSingleNode("posts");
            if (postsNode == null)
            {
                postsNode = doc.CreateElement("posts");
                root.AppendChild(postsNode);
            }

            XmlElement postElement = doc.CreateElement("post");

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

            postsNode.AppendChild(postElement);
            doc.Save(path);

            MessageBox.Show("Post created successfuly!");

            titleBox.Text = "";
            contentBox.Text = "";


        }
    }
}
