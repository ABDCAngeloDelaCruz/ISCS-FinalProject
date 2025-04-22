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
            InitializeUI();
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
        public void InitializeUI()
        {
            // Subscribe to login status changes
            Session.LoginStatusChanged += Session_LoginStatusChanged;

            // Initialize UI based on current login status
            UpdateUIForLoginStatus(Session.IsLoggedIn);

            // Set default button colors
            newPosts.BackColor = Color.FromArgb(23, 24, 29); // Active button - darker
            trendingPosts.BackColor = Color.FromArgb(32, 33, 36); // Inactive button
            // addPost should keep its original color from Designer.cs

            // Resize button images to make them smaller
            ResizeButtonImages();
        }

        private void ResizeButtonImages()
        {
            // Set a smaller size for all button images
            int imageSize = 20; // Smaller image size

            // Resize images for all sidebar buttons
            if (newPosts.Image != null)
                newPosts.Image = ResizeImage(newPosts.Image, imageSize, imageSize);

            if (trendingPosts.Image != null)
                trendingPosts.Image = ResizeImage(trendingPosts.Image, imageSize, imageSize);

            if (addPost.Image != null)
                addPost.Image = ResizeImage(addPost.Image, imageSize, imageSize);

            if (login.Image != null)
                login.Image = ResizeImage(login.Image, imageSize, imageSize);

            if (register.Image != null)
                register.Image = ResizeImage(register.Image, imageSize, imageSize);

            if (logout.Image != null)
                logout.Image = ResizeImage(logout.Image, imageSize, imageSize);
        }

        bool menuExpand = false;


        private Image ResizeImage(Image image, int width, int height)
        {
            // Create a new bitmap with the desired size
            Bitmap result = new Bitmap(width, height);

            // Create a graphics object to draw the resized image
            using (Graphics g = Graphics.FromImage(result))
            {
                // Set the interpolation mode for better quality
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

                // Draw the image with the new size
                g.DrawImage(image, 0, 0, width, height);
            }

            return result;
        }

        private void Session_LoginStatusChanged(object sender, LoginStatusChangedEventArgs e)
        {
            // Update UI based on login status
            UpdateUIForLoginStatus(e.IsLoggedIn);
        }

        private void UpdateUIForLoginStatus(bool isLoggedIn)
        {
            // Update UI elements based on login status
            pnLogin.Visible = !isLoggedIn;
            panel5.Visible = !isLoggedIn; // Register panel
            pnLogout.Visible = isLoggedIn;
        }
        public void LoadView(UserControl view)
        {
            // Clear existing controls
            panelMain.Controls.Clear();

            // Set dock style to fill
            view.Dock = DockStyle.Fill;

            // Add the view to the panel
            panelMain.Controls.Add(view);

            // Ensure the view is visible
            view.Visible = true;
            view.BringToFront();

            // If it's a newPosts or TrendingPosts view, make sure to load posts
            if (view is newPosts postsView)
            {
                postsView.LoadPosts();
            }
            else if (view is TrendingPosts trendingView)
            {
                trendingView.LoadPosts();
            }
        }

        public void LoadPostDetail(string postId)
        {
            LoadView(new PostDetail(postId));
        }

        // Removed dropdown functionality
        // Removed sidebar toggle functionality

        // Removed burger menu functionality

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
            // Reset Posts tab button colors
            newPosts.BackColor = Color.FromArgb(32, 33, 36); // Inactive
            trendingPosts.BackColor = Color.FromArgb(32, 33, 36); // Inactive

            // The Create button (addPost) color should remain unchanged
            // as it's not part of the Posts tab group

            LoadView(new createPost());
        }

        private void newPosts_Click(object sender, EventArgs e)
        {
            // Reset button colors
            trendingPosts.BackColor = Color.FromArgb(32, 33, 36); // Inactive
            // Don't change addPost button color

            // Highlight the new posts button
            newPosts.BackColor = Color.FromArgb(23, 24, 29); // Active - darker

            LoadView(new newPosts());
        }

        private void trendingPosts_Click(object sender, EventArgs e)
        {
            // Reset button colors
            newPosts.BackColor = Color.FromArgb(32, 33, 36); // Inactive
            // Don't change addPost button color

            // Highlight the trending button
            trendingPosts.BackColor = Color.FromArgb(23, 24, 29); // Active - darker

            LoadView(new TrendingPosts());
        }

        private void logout_Click(object sender, EventArgs e)
        {
            // Confirm logout
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Log out the user
                Session.Logout();

                // Show a message
                MessageBox.Show("You have been logged out successfully.", "Logout Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Reset Posts tab button colors
                newPosts.BackColor = Color.FromArgb(23, 24, 29); // Active button - darker
                trendingPosts.BackColor = Color.FromArgb(32, 33, 36); // Inactive button

                // Navigate to the posts view
                LoadView(new newPosts());
            }
        }
    }
}
