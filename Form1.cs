using System;
using System.Windows.Forms;
using System.Xml;
using Timer = System.Timers.Timer;

namespace FinalProject
{
    public partial class Form1 : Form
    {
        private Timer postUpdateTimer;

        public Form1()
        {
            InitializeComponent();
            InitializeXML();
            SetupPeriodicPostUpdate();
        }

        public void InitializeXML()
        {
            string serverProjectDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\ForumServer\bin\Debug\net9.0");
            string relativePath = @"XMLFiles\data.xml";
            string path = Path.Combine(serverProjectDirectory, relativePath);

            if (!File.Exists(path))
            {
                XmlDocument doc = new XmlDocument();
                XmlElement root = doc.CreateElement("data");
                doc.AppendChild(root);
                doc.Save(path);
            }
        }
        public void LoadView(UserControl view)
        {
            panelMain.Controls.Clear();
            view.Dock = DockStyle.Fill;
            panelMain.Controls.Add(view);
        }

        public void LoadPostDetail(string postId)
        {
            LoadView(new PostDetail(postId));
        }

        bool menuExpand = false;

        private void SetupPeriodicPostUpdate()
        {
            postUpdateTimer = new Timer();
            postUpdateTimer.Interval = 2000;
            postUpdateTimer.Elapsed += (sender, e) => newPosts_Click(sender, e);
            postUpdateTimer.Start();
        }
        private void menuTransition_Tick(object sender, EventArgs e)
        {
            if (menuExpand == false)
            {
                postContainer.Height += 10;
                if (postContainer.Height >= 166)
                {

                    menuTransition.Stop();
                    menuExpand = true;
                }
            }
            else
            {
                postContainer.Height -= 10;
                if (postContainer.Height <= 48)
                {
                    menuTransition.Stop();
                    menuExpand = false;
                }
            }
        }

        private void posts_Click(object sender, EventArgs e)
        {
            menuTransition.Start();
        }
        bool sidebarExpand = true;
        private void sidebarTransition_Tick(object sender, EventArgs e)
        {
            if (sidebarExpand)
            {
                sidebar.Width -= 10;
                if (sidebar.Width <= 81)
                {
                    sidebarExpand = false;
                    sidebarTransition.Stop();
                }
            }
            else
            {
                sidebar.Width += 10;
                if (sidebar.Width >= 223)
                {
                    sidebarExpand = true;
                    sidebarTransition.Stop();
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            sidebarTransition.Start();
        }

        private void login_Click(object sender, EventArgs e)
        {
            LoadView(new Login());
        }

        private void register_Click(object sender, EventArgs e)
        {
            LoadView(new Register());
        }

        private void addPost_Click(object sender, EventArgs e)
        {
            LoadView(new createPost());
        }

        private void newPosts_Click(object sender, EventArgs e)
        {
            LoadView(new newPosts());
        }

        private void trendingPosts_Click(object sender, EventArgs e)
        {
            LoadView(new createPost());
        }
    }
}
