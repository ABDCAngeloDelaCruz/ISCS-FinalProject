namespace FinalProject
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            sidebar = new FlowLayoutPanel();
            sidebarHeader = new Label();
            postContainer = new FlowLayoutPanel();
            panel2 = new Panel();
            newPosts = new Button();
            panel3 = new Panel();
            trendingPosts = new Button();
            panel6 = new Panel();
            addPost = new Button();
            pnLogin = new Panel();
            login = new Button();
            panel5 = new Panel();
            register = new Button();
            pnLogout = new Panel();
            logout = new Button();
            panelMain = new Panel();
            menuTransition = new System.Windows.Forms.Timer(components);
            sidebarTransition = new System.Windows.Forms.Timer(components);
            sidebar.SuspendLayout();
            // Removed menuButton initialization
            postContainer.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panel6.SuspendLayout();
            pnLogin.SuspendLayout();
            panel5.SuspendLayout();
            pnLogout.SuspendLayout();
            SuspendLayout();
            //
            // sidebar
            //
            sidebar.BackColor = Color.FromArgb(40, 40, 55);
            sidebar.Controls.Add(sidebarHeader);
            sidebar.Controls.Add(postContainer);
            sidebar.Controls.Add(panel6);
            sidebar.Controls.Add(pnLogin);
            sidebar.Controls.Add(panel5);
            sidebar.Controls.Add(pnLogout);
            sidebar.Dock = DockStyle.Left;
            sidebar.Location = new Point(0, 0);
            sidebar.Name = "sidebar";
            sidebar.Padding = new Padding(0, 10, 0, 0);
            sidebar.Size = new Size(250, 807);
            sidebar.TabIndex = 1;
            // Removed menuButton
            //
            // sidebarHeader
            //
            sidebarHeader.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            sidebarHeader.ForeColor = Color.White;
            sidebarHeader.Location = new Point(3, 46);
            sidebarHeader.Name = "sidebarHeader";
            sidebarHeader.Padding = new Padding(0, 5, 0, 5);
            sidebarHeader.Size = new Size(247, 30);
            sidebarHeader.TabIndex = 0;
            sidebarHeader.Text = "NAVIGATION";
            sidebarHeader.TextAlign = ContentAlignment.MiddleCenter;
            //
            // postContainer
            //
            postContainer.BackColor = Color.FromArgb(50, 50, 65);
            postContainer.Controls.Add(panel2);
            postContainer.Controls.Add(panel3);
            postContainer.Location = new Point(0, 76);
            postContainer.Margin = new Padding(0, 0, 0, 10);
            postContainer.Name = "postContainer";
            postContainer.Size = new Size(247, 96);
            postContainer.TabIndex = 6;
            //
            // panel2
            //
            panel2.Controls.Add(newPosts);
            panel2.Location = new Point(0, 0);
            panel2.Margin = new Padding(0);
            panel2.Name = "panel2";
            panel2.Size = new Size(247, 48);
            panel2.TabIndex = 3;
            //
            // newPosts
            //
            newPosts.BackColor = Color.FromArgb(50, 50, 65);
            newPosts.FlatAppearance.BorderSize = 0;
            newPosts.FlatStyle = FlatStyle.Flat;
            newPosts.Font = new Font("Segoe UI", 10F);
            newPosts.ForeColor = Color.White;
            newPosts.Image = (Image)resources.GetObject("newPosts.Image");
            newPosts.ImageAlign = ContentAlignment.MiddleLeft;
            newPosts.Location = new Point(0, 0);
            newPosts.Name = "newPosts";
            newPosts.Padding = new Padding(10, 0, 0, 0);
            newPosts.Size = new Size(247, 48);
            newPosts.TabIndex = 2;
            newPosts.Text = "New";
            newPosts.TextAlign = ContentAlignment.MiddleLeft;
            newPosts.TextImageRelation = TextImageRelation.ImageBeforeText;
            newPosts.UseVisualStyleBackColor = false;
            newPosts.Click += newPosts_Click;
            //
            // panel3
            //
            panel3.Controls.Add(trendingPosts);
            panel3.Location = new Point(0, 48);
            panel3.Margin = new Padding(0);
            panel3.Name = "panel3";
            panel3.Size = new Size(247, 48);
            panel3.TabIndex = 4;
            //
            // trendingPosts
            //
            trendingPosts.BackColor = Color.FromArgb(50, 50, 65);
            trendingPosts.FlatAppearance.BorderSize = 0;
            trendingPosts.FlatStyle = FlatStyle.Flat;
            trendingPosts.Font = new Font("Segoe UI", 10F);
            trendingPosts.ForeColor = Color.White;
            trendingPosts.Image = (Image)resources.GetObject("trendingPosts.Image");
            trendingPosts.ImageAlign = ContentAlignment.MiddleLeft;
            trendingPosts.Location = new Point(0, 0);
            trendingPosts.Name = "trendingPosts";
            trendingPosts.Padding = new Padding(10, 0, 0, 0);
            trendingPosts.Size = new Size(247, 48);
            trendingPosts.TabIndex = 2;
            trendingPosts.Text = "Trending";
            trendingPosts.TextAlign = ContentAlignment.MiddleLeft;
            trendingPosts.TextImageRelation = TextImageRelation.ImageBeforeText;
            trendingPosts.UseVisualStyleBackColor = false;
            trendingPosts.Click += trendingPosts_Click;
            //
            // panel6
            //
            panel6.Anchor = AnchorStyles.None;
            panel6.Controls.Add(addPost);
            panel6.Location = new Point(0, 192);
            panel6.Margin = new Padding(0, 10, 0, 10);
            panel6.Name = "panel6";
            panel6.Size = new Size(247, 48);
            panel6.TabIndex = 8;
            //
            // addPost
            //
            addPost.BackColor = Color.FromArgb(80, 80, 120);
            addPost.FlatAppearance.BorderSize = 0;
            addPost.FlatStyle = FlatStyle.Flat;
            addPost.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            addPost.ForeColor = Color.White;
            addPost.Image = (Image)resources.GetObject("addPost.Image");
            addPost.ImageAlign = ContentAlignment.MiddleLeft;
            addPost.Location = new Point(0, 0);
            addPost.Name = "addPost";
            addPost.Padding = new Padding(10, 0, 0, 0);
            addPost.Size = new Size(247, 48);
            addPost.TabIndex = 2;
            addPost.Text = "Create Post";
            addPost.TextAlign = ContentAlignment.MiddleLeft;
            addPost.TextImageRelation = TextImageRelation.ImageBeforeText;
            addPost.UseVisualStyleBackColor = false;
            addPost.Click += addPost_Click;
            //
            // pnLogin
            //
            pnLogin.Anchor = AnchorStyles.None;
            pnLogin.Controls.Add(login);
            pnLogin.Location = new Point(0, 250);
            pnLogin.Margin = new Padding(0);
            pnLogin.Name = "pnLogin";
            pnLogin.Size = new Size(247, 48);
            pnLogin.TabIndex = 6;
            //
            // login
            //
            login.BackColor = Color.FromArgb(60, 60, 80);
            login.FlatAppearance.BorderSize = 0;
            login.FlatStyle = FlatStyle.Flat;
            login.Font = new Font("Segoe UI", 11F);
            login.ForeColor = Color.White;
            login.Image = (Image)resources.GetObject("login.Image");
            login.ImageAlign = ContentAlignment.MiddleLeft;
            login.Location = new Point(0, 0);
            login.Name = "login";
            login.Padding = new Padding(10, 0, 0, 0);
            login.Size = new Size(247, 48);
            login.TabIndex = 2;
            login.Text = "Login";
            login.TextAlign = ContentAlignment.MiddleLeft;
            login.TextImageRelation = TextImageRelation.ImageBeforeText;
            login.UseVisualStyleBackColor = false;
            login.Click += login_Click;
            //
            // panel5
            //
            panel5.Anchor = AnchorStyles.None;
            panel5.Controls.Add(register);
            panel5.Location = new Point(0, 298);
            panel5.Margin = new Padding(0);
            panel5.Name = "panel5";
            panel5.Size = new Size(247, 48);
            panel5.TabIndex = 7;
            //
            // register
            //
            register.BackColor = Color.FromArgb(60, 60, 80);
            register.FlatAppearance.BorderSize = 0;
            register.FlatStyle = FlatStyle.Flat;
            register.Font = new Font("Segoe UI", 11F);
            register.ForeColor = Color.White;
            register.Image = (Image)resources.GetObject("register.Image");
            register.ImageAlign = ContentAlignment.MiddleLeft;
            register.Location = new Point(0, 0);
            register.Name = "register";
            register.Padding = new Padding(10, 0, 0, 0);
            register.Size = new Size(247, 48);
            register.TabIndex = 2;
            register.Text = "Register";
            register.TextAlign = ContentAlignment.MiddleLeft;
            register.TextImageRelation = TextImageRelation.ImageBeforeText;
            register.UseVisualStyleBackColor = false;
            register.Click += register_Click;
            //
            // pnLogout
            //
            pnLogout.Anchor = AnchorStyles.None;
            pnLogout.Controls.Add(logout);
            pnLogout.Location = new Point(0, 346);
            pnLogout.Margin = new Padding(0);
            pnLogout.Name = "pnLogout";
            pnLogout.Size = new Size(247, 48);
            pnLogout.TabIndex = 9;
            pnLogout.Visible = false;
            //
            // logout
            //
            logout.BackColor = Color.FromArgb(180, 70, 70);
            logout.FlatAppearance.BorderSize = 0;
            logout.FlatStyle = FlatStyle.Flat;
            logout.Font = new Font("Segoe UI", 11F);
            logout.ForeColor = Color.White;
            logout.Image = (Image)resources.GetObject("logout.Image");
            logout.ImageAlign = ContentAlignment.MiddleLeft;
            logout.Location = new Point(0, 0);
            logout.Name = "logout";
            logout.Padding = new Padding(10, 0, 0, 0);
            logout.Size = new Size(247, 48);
            logout.TabIndex = 2;
            logout.Text = "Logout";
            logout.TextAlign = ContentAlignment.MiddleLeft;
            logout.TextImageRelation = TextImageRelation.ImageBeforeText;
            logout.UseVisualStyleBackColor = false;
            logout.Click += logout_Click;
            //
            // panelMain
            //
            panelMain.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelMain.BackColor = Color.FromArgb(245, 245, 250);
            panelMain.Location = new Point(250, 0);
            panelMain.Name = "panelMain";
            panelMain.Padding = new Padding(20);
            panelMain.Size = new Size(1106, 807);
            panelMain.TabIndex = 3;
            //
            // menuTransition
            //
            menuTransition.Interval = 10;
            //
            // sidebarTransition
            //
            sidebarTransition.Interval = 10;
            // Removed sidebarTransition_Tick event handler
            //
            // Form1
            //
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(40, 40, 55);
            ClientSize = new Size(1356, 807);
            Controls.Add(panelMain);
            Controls.Add(sidebar);
            Font = new Font("Segoe UI", 10F);
            IsMdiContainer = true;
            MinimumSize = new Size(900, 600);
            Name = "Form1";
            Text = "Social Forum";
            sidebar.ResumeLayout(false);
            // Removed menuButton cleanup
            postContainer.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel6.ResumeLayout(false);
            pnLogin.ResumeLayout(false);
            panel5.ResumeLayout(false);
            pnLogout.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        // Removed top bar elements
        private Label sidebarHeader;
        private FlowLayoutPanel sidebar;
        private Button newPosts;
        private Panel panel2;
        private Panel panel3;
        private Button trendingPosts;
        private System.Windows.Forms.Timer menuTransition;
        // Removed dropdown elements
        private FlowLayoutPanel postContainer;
        private System.Windows.Forms.Timer sidebarTransition;
        private Panel pnLogin;
        private Button login;
        private Panel panelMain;
        private Panel panel5;
        private Button register;
        private Panel panel6;
        private Button addPost;
        private Panel pnLogout;
        private Button logout;
    }
}
