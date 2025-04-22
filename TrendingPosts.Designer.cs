namespace FinalProject
{
    partial class TrendingPosts
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
            postContainer = new FlowLayoutPanel();
            postCard = new Panel();
            postMeta = new Label();
            postConts = new Label();
            postTitle = new Label();
            postContainer.SuspendLayout();
            postCard.SuspendLayout();
            SuspendLayout();
            //
            // postContainer
            //
            postContainer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            postContainer.Dock = DockStyle.Fill;
            postContainer.AutoScroll = true;
            postContainer.BackColor = Color.WhiteSmoke;
            postContainer.Controls.Add(postCard);
            postContainer.FlowDirection = FlowDirection.TopDown;
            postContainer.Location = new Point(0, 0);
            postContainer.Name = "postContainer";
            postContainer.Padding = new Padding(20, 20, 20, 100); // Extra padding at bottom
            postContainer.Size = new Size(900, 600);
            postContainer.TabIndex = 0;
            postContainer.WrapContents = false;
            //
            // postCard
            //
            postCard.BackColor = Color.White;
            postCard.BorderStyle = BorderStyle.FixedSingle;
            postCard.Controls.Add(postMeta);
            postCard.Controls.Add(postConts);
            postCard.Controls.Add(postTitle);
            postCard.Location = new Point(25, 25);
            postCard.Margin = new Padding(5);
            postCard.Name = "postCard";
            postCard.Padding = new Padding(10);
            postCard.Size = new Size(800, 150);
            postCard.TabIndex = 1;
            postCard.Visible = false;
            //
            // postMeta
            //
            postMeta.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            postMeta.Location = new Point(3, 123);
            postMeta.Name = "postMeta";
            postMeta.Size = new Size(780, 18);
            postMeta.TabIndex = 2;
            postMeta.Text = "Meta";
            postMeta.TextAlign = ContentAlignment.TopRight;
            //
            // postConts
            //
            postConts.Location = new Point(3, 25);
            postConts.MaximumSize = new Size(780, 85);
            postConts.MinimumSize = new Size(780, 20);
            postConts.Name = "postConts";
            postConts.Size = new Size(780, 85);
            postConts.TabIndex = 1;
            postConts.Text = "label1";
            //
            // postTitle
            //
            postTitle.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            postTitle.Location = new Point(0, 0);
            postTitle.MaximumSize = new Size(780, 25);
            postTitle.Name = "postTitle";
            postTitle.Size = new Size(780, 25);
            postTitle.TabIndex = 0;
            postTitle.Text = "Title";
            //
            // TrendingPosts
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(postContainer);
            Name = "TrendingPosts";
            Size = new Size(900, 600);
            postContainer.ResumeLayout(false);
            postCard.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel postContainer;
        private Panel postCard;
        private Label postMeta;
        private Label postConts;
        private Label postTitle;
    }
}
