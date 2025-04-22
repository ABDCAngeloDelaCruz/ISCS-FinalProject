using System;
using System.Windows.Forms;

namespace FinalProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Subscribe to login status changes
            Session.LoginStatusChanged += Session_LoginStatusChanged;

            // Initialize UI based on current login status
            UpdateUIForLoginStatus(Session.IsLoggedIn);

            // Set default button colors
            newPosts.BackColor = Color.FromArgb(23, 24, 29); // Active button - darker
            trendingPosts.BackColor = Color.FromArgb(32, 33, 36); // Inactive button
            // addPost should keep its original color from Designer.cs

            // Load the default view
            LoadView(new newPosts());
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

        bool menuExpand = false;
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
