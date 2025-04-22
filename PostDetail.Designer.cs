namespace FinalProject
{
    partial class PostDetail
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblContent = new System.Windows.Forms.Label();
            this.lblAuthor = new System.Windows.Forms.Label();
            this.lblTimestamp = new System.Windows.Forms.Label();
            this.commentsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.btnAddComment = new System.Windows.Forms.Button();
            this.lblComments = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            //
            // lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitle.Location = new System.Drawing.Point(20, 50);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(121, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Post Title";
            //
            // lblContent
            //
            this.lblContent.AutoSize = true;
            this.lblContent.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblContent.Location = new System.Drawing.Point(20, 100);
            this.lblContent.MaximumSize = new System.Drawing.Size(900, 0);
            this.lblContent.Name = "lblContent";
            this.lblContent.Size = new System.Drawing.Size(95, 20);
            this.lblContent.TabIndex = 1;
            this.lblContent.Text = "Post Content";
            this.lblContent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            //
            // lblAuthor
            //
            this.lblAuthor.AutoSize = true;
            this.lblAuthor.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblAuthor.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblAuthor.Location = new System.Drawing.Point(20, 200);
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Size = new System.Drawing.Size(44, 15);
            this.lblAuthor.TabIndex = 2;
            this.lblAuthor.Text = "Author";
            //
            // lblTimestamp
            //
            this.lblTimestamp.AutoSize = true;
            this.lblTimestamp.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTimestamp.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblTimestamp.Location = new System.Drawing.Point(20, 220);
            this.lblTimestamp.Name = "lblTimestamp";
            this.lblTimestamp.Size = new System.Drawing.Size(65, 15);
            this.lblTimestamp.TabIndex = 3;
            this.lblTimestamp.Text = "Timestamp";
            //
            // commentsPanel
            //
            this.commentsPanel.AutoScroll = true;
            this.commentsPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.commentsPanel.Location = new System.Drawing.Point(30, 300);
            this.commentsPanel.Name = "commentsPanel";
            this.commentsPanel.Size = new System.Drawing.Size(840, 200);
            this.commentsPanel.TabIndex = 4;
            this.commentsPanel.WrapContents = false;
            this.commentsPanel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.commentsPanel.BackColor = System.Drawing.Color.FromArgb(245, 245, 250);
            this.commentsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.commentsPanel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            //
            // txtComment
            //
            this.txtComment.Location = new System.Drawing.Point(30, 520);
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.PlaceholderText = "Write a comment...";
            this.txtComment.Size = new System.Drawing.Size(730, 60);
            this.txtComment.TabIndex = 5;
            this.txtComment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtComment.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtComment.BackColor = System.Drawing.Color.White;
            this.txtComment.ForeColor = System.Drawing.Color.FromArgb(60, 60, 80);
            //
            // btnAddComment
            //
            this.btnAddComment.Location = new System.Drawing.Point(770, 520);
            this.btnAddComment.Name = "btnAddComment";
            this.btnAddComment.Size = new System.Drawing.Size(100, 60);
            this.btnAddComment.TabIndex = 6;
            this.btnAddComment.Text = "Add Comment";
            this.btnAddComment.UseVisualStyleBackColor = false;
            this.btnAddComment.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnAddComment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddComment.FlatAppearance.BorderSize = 0;
            this.btnAddComment.BackColor = System.Drawing.Color.FromArgb(80, 80, 120);
            this.btnAddComment.ForeColor = System.Drawing.Color.White;
            this.btnAddComment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddComment.Click += new System.EventHandler(this.btnAddComment_Click);
            //
            // lblComments
            //
            this.lblComments.AutoSize = true;
            this.lblComments.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblComments.Location = new System.Drawing.Point(20, 270);
            this.lblComments.Name = "lblComments";
            this.lblComments.Size = new System.Drawing.Size(90, 21);
            this.lblComments.TabIndex = 7;
            this.lblComments.Text = "Comments";
            //
            // btnBack
            //
            this.btnBack.Location = new System.Drawing.Point(20, 10);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 30);
            this.btnBack.TabIndex = 8;
            this.btnBack.Text = "← Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            //
            // panel1
            //
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.btnBack);
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Controls.Add(this.lblContent);
            this.panel1.Controls.Add(this.lblAuthor);
            this.panel1.Controls.Add(this.lblTimestamp);
            this.panel1.Controls.Add(this.lblComments);
            this.panel1.Controls.Add(this.commentsPanel);
            this.panel1.Controls.Add(this.txtComment);
            this.panel1.Controls.Add(this.btnAddComment);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(900, 600);
            this.panel1.TabIndex = 9;
            this.panel1.BackColor = System.Drawing.Color.FromArgb(245, 245, 250);
            this.panel1.Padding = new System.Windows.Forms.Padding(30, 30, 30, 100); // Extra padding at bottom
            //
            // PostDetail
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "PostDetail";
            this.Size = new System.Drawing.Size(900, 600);
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Label lblTitle;
        private Label lblContent;
        private Label lblAuthor;
        private Label lblTimestamp;
        private FlowLayoutPanel commentsPanel;
        private TextBox txtComment;
        private Button btnAddComment;
        private Label lblComments;
        private Button btnBack;
        private Panel panel1;
    }
}
