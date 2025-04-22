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
            panel1 = new Panel();
            pictureBox1 = new PictureBox();
            sidebar = new FlowLayoutPanel();
            postContainer = new FlowLayoutPanel();
            panel4 = new Panel();
            posts = new Button();
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
            menuTransition = new System.Windows.Forms.Timer(components);
            sidebarTransition = new System.Windows.Forms.Timer(components);
            panelMain = new Panel();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            sidebar.SuspendLayout();
            postContainer.SuspendLayout();
            panel4.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panel6.SuspendLayout();
            pnLogin.SuspendLayout();
            panel5.SuspendLayout();
            pnLogout.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(23, 24, 29);
            panel1.Controls.Add(pictureBox1);
            panel1.Dock = DockStyle.Top;
            panel1.ForeColor = Color.White;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1356, 62);
            panel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(71, 60);
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // sidebar
            // 
            sidebar.BackColor = Color.FromArgb(23, 24, 29);
            sidebar.Controls.Add(postContainer);
            sidebar.Controls.Add(panel6);
            sidebar.Controls.Add(pnLogin);
            sidebar.Controls.Add(panel5);
            sidebar.Controls.Add(pnLogout);
            sidebar.Dock = DockStyle.Left;
            sidebar.Location = new Point(0, 62);
            sidebar.Name = "sidebar";
            sidebar.Size = new Size(223, 745);
            sidebar.TabIndex = 1;
            // 
            // postContainer
            // 
            postContainer.BackColor = Color.FromArgb(32, 33, 36);
            postContainer.Controls.Add(panel4);
            postContainer.Controls.Add(panel2);
            postContainer.Controls.Add(panel3);
            postContainer.Location = new Point(3, 3);
            postContainer.Name = "postContainer";
            postContainer.Size = new Size(223, 48);
            postContainer.TabIndex = 6;
            // 
            // panel4
            // 
            panel4.Anchor = AnchorStyles.None;
            panel4.Controls.Add(posts);
            panel4.Location = new Point(3, 3);
            panel4.Name = "panel4";
            panel4.Size = new Size(229, 48);
            panel4.TabIndex = 5;
            // 
            // posts
            // 
            posts.BackColor = Color.FromArgb(23, 24, 29);
            posts.ForeColor = SystemColors.ControlLightLight;
            posts.Image = (Image)resources.GetObject("posts.Image");
            posts.ImageAlign = ContentAlignment.MiddleLeft;
            posts.Location = new Point(-21, -10);
            posts.Name = "posts";
            posts.Padding = new Padding(25, 0, 0, 0);
            posts.Size = new Size(255, 69);
            posts.TabIndex = 2;
            posts.Text = "Posts";
            posts.UseVisualStyleBackColor = false;
            posts.Click += posts_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(newPosts);
            panel2.Location = new Point(3, 57);
            panel2.Name = "panel2";
            panel2.Size = new Size(223, 48);
            panel2.TabIndex = 3;
            // 
            // newPosts
            // 
            newPosts.BackColor = Color.FromArgb(32, 33, 36);
            newPosts.ForeColor = SystemColors.ControlLightLight;
            newPosts.Image = (Image)resources.GetObject("newPosts.Image");
            newPosts.ImageAlign = ContentAlignment.MiddleLeft;
            newPosts.Location = new Point(-23, -9);
            newPosts.Name = "newPosts";
            newPosts.Padding = new Padding(25, 0, 0, 0);
            newPosts.Size = new Size(276, 68);
            newPosts.TabIndex = 2;
            newPosts.Text = "New";
            newPosts.UseVisualStyleBackColor = false;
            newPosts.Click += newPosts_Click;
            // 
            // panel3
            // 
            panel3.Controls.Add(trendingPosts);
            panel3.Location = new Point(3, 111);
            panel3.Name = "panel3";
            panel3.Size = new Size(223, 48);
            panel3.TabIndex = 4;
            // 
            // trendingPosts
            // 
            trendingPosts.BackColor = Color.FromArgb(32, 33, 36);
            trendingPosts.ForeColor = SystemColors.ControlLightLight;
            trendingPosts.Image = (Image)resources.GetObject("trendingPosts.Image");
            trendingPosts.ImageAlign = ContentAlignment.MiddleLeft;
            trendingPosts.Location = new Point(-23, -9);
            trendingPosts.Name = "trendingPosts";
            trendingPosts.Padding = new Padding(25, 0, 0, 0);
            trendingPosts.Size = new Size(276, 76);
            trendingPosts.TabIndex = 2;
            trendingPosts.Text = "Trending";
            trendingPosts.UseVisualStyleBackColor = false;
            trendingPosts.Click += trendingPosts_Click;
            // 
            // panel6
            // 
            panel6.Anchor = AnchorStyles.None;
            panel6.Controls.Add(addPost);
            panel6.Location = new Point(3, 57);
            panel6.Name = "panel6";
            panel6.Size = new Size(229, 48);
            panel6.TabIndex = 8;
            // 
            // addPost
            // 
            addPost.BackColor = Color.FromArgb(23, 24, 29);
            addPost.ForeColor = SystemColors.ControlLightLight;
            addPost.Image = (Image)resources.GetObject("addPost.Image");
            addPost.ImageAlign = ContentAlignment.MiddleLeft;
            addPost.Location = new Point(-21, -6);
            addPost.Name = "addPost";
            addPost.Padding = new Padding(25, 0, 0, 0);
            addPost.Size = new Size(255, 68);
            addPost.TabIndex = 2;
            addPost.Text = "Create";
            addPost.UseVisualStyleBackColor = false;
            addPost.Click += addPost_Click;
            // 
            // pnLogin
            // 
            pnLogin.Anchor = AnchorStyles.None;
            pnLogin.Controls.Add(login);
            pnLogin.Location = new Point(3, 111);
            pnLogin.Name = "pnLogin";
            pnLogin.Size = new Size(229, 48);
            pnLogin.TabIndex = 6;
            // 
            // login
            // 
            login.BackColor = Color.FromArgb(23, 24, 29);
            login.ForeColor = SystemColors.ControlLightLight;
            login.Image = (Image)resources.GetObject("login.Image");
            login.ImageAlign = ContentAlignment.MiddleLeft;
            login.Location = new Point(-21, -10);
            login.Name = "login";
            login.Padding = new Padding(25, 0, 0, 0);
            login.Size = new Size(255, 69);
            login.TabIndex = 2;
            login.Text = "Login";
            login.UseVisualStyleBackColor = false;
            login.Click += login_Click;
            // 
            // panel5
            // 
            panel5.Anchor = AnchorStyles.None;
            panel5.Controls.Add(register);
            panel5.Location = new Point(3, 165);
            panel5.Name = "panel5";
            panel5.Size = new Size(229, 48);
            panel5.TabIndex = 7;
            // 
            // register
            // 
            register.BackColor = Color.FromArgb(23, 24, 29);
            register.ForeColor = SystemColors.ControlLightLight;
            register.Image = (Image)resources.GetObject("register.Image");
            register.ImageAlign = ContentAlignment.MiddleLeft;
            register.Location = new Point(-21, -6);
            register.Name = "register";
            register.Padding = new Padding(25, 0, 0, 0);
            register.Size = new Size(255, 68);
            register.TabIndex = 2;
            register.Text = "Reigster";
            register.UseVisualStyleBackColor = false;
            register.Click += register_Click;
            // 
            // pnLogout
            // 
            pnLogout.Anchor = AnchorStyles.None;
            pnLogout.Controls.Add(logout);
            pnLogout.Location = new Point(3, 219);
            pnLogout.Name = "pnLogout";
            pnLogout.Size = new Size(229, 48);
            pnLogout.TabIndex = 9;
            pnLogout.Visible = false;
            // 
            // logout
            // 
            logout.BackColor = Color.FromArgb(23, 24, 29);
            logout.ForeColor = SystemColors.ControlLightLight;
            logout.Image = (Image)resources.GetObject("logout.Image");
            logout.ImageAlign = ContentAlignment.MiddleLeft;
            logout.Location = new Point(-21, -6);
            logout.Name = "logout";
            logout.Padding = new Padding(25, 0, 0, 0);
            logout.Size = new Size(255, 68);
            logout.TabIndex = 2;
            logout.Text = "Logout";
            logout.UseVisualStyleBackColor = false;
            logout.Click += logout_Click;
            // 
            // menuTransition
            // 
            menuTransition.Interval = 10;
            menuTransition.Tick += menuTransition_Tick;
            // 
            // sidebarTransition
            // 
            sidebarTransition.Interval = 10;
            sidebarTransition.Tick += sidebarTransition_Tick;
            // 
            // panelMain
            // 
            panelMain.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelMain.BackColor = Color.White;
            panelMain.Location = new Point(223, 62);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(1135, 747);
            panelMain.TabIndex = 3;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1356, 807);
            Controls.Add(panelMain);
            Controls.Add(sidebar);
            Controls.Add(panel1);
            IsMdiContainer = true;
            MinimumSize = new Size(800, 600);
            Name = "Form1";
            Text = "Form1";
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            sidebar.ResumeLayout(false);
            postContainer.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel6.ResumeLayout(false);
            pnLogin.ResumeLayout(false);
            panel5.ResumeLayout(false);
            pnLogout.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private PictureBox pictureBox1;
        private FlowLayoutPanel sidebar;
        private Button newPosts;
        private Panel panel2;
        private Panel panel3;
        private Button trendingPosts;
        private System.Windows.Forms.Timer menuTransition;
        private Panel panel4;
        private Button posts;
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
