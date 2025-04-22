namespace FinalProject
{
    partial class createPost
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
            mainPanel = new Panel();
            headerLabel = new Label();
            label1 = new Label();
            titleBox = new TextBox();
            label2 = new Label();
            contentBox = new RichTextBox();
            submitPostBtn = new Button();
            mainPanel.SuspendLayout();
            SuspendLayout();
            //
            // mainPanel
            //
            mainPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            mainPanel.BackColor = Color.White;
            mainPanel.BorderStyle = BorderStyle.FixedSingle;
            mainPanel.Controls.Add(headerLabel);
            mainPanel.Controls.Add(submitPostBtn);
            mainPanel.Controls.Add(contentBox);
            mainPanel.Controls.Add(titleBox);
            mainPanel.Controls.Add(label1);
            mainPanel.Controls.Add(label2);
            mainPanel.Location = new Point(50, 50);
            mainPanel.Name = "mainPanel";
            mainPanel.Size = new Size(800, 500);
            mainPanel.Padding = new Padding(30);
            mainPanel.TabIndex = 0;
            //
            // headerLabel
            //
            headerLabel.AutoSize = true;
            headerLabel.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            headerLabel.Location = new Point(30, 30);
            headerLabel.Name = "headerLabel";
            headerLabel.Size = new Size(200, 32);
            headerLabel.TabIndex = 6;
            headerLabel.Text = "Create New Post";
            //
            // label1
            //
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(30, 90);
            label1.Name = "label1";
            label1.Size = new Size(44, 21);
            label1.TabIndex = 0;
            label1.Text = "Title";
            //
            // titleBox
            //
            titleBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            titleBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            titleBox.Location = new Point(30, 115);
            titleBox.Name = "titleBox";
            titleBox.Size = new Size(740, 29);
            titleBox.TabIndex = 1;
            //
            // label2
            //
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(30, 165);
            label2.Name = "label2";
            label2.Size = new Size(73, 21);
            label2.TabIndex = 2;
            label2.Text = "Content";
            //
            // contentBox
            //
            contentBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            contentBox.BorderStyle = BorderStyle.FixedSingle;
            contentBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            contentBox.Location = new Point(30, 190);
            contentBox.Name = "contentBox";
            contentBox.Size = new Size(740, 200);
            contentBox.TabIndex = 4;
            contentBox.Text = "";
            //
            // submitPostBtn
            //
            submitPostBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            submitPostBtn.BackColor = Color.FromArgb(23, 24, 29);
            submitPostBtn.FlatStyle = FlatStyle.Flat;
            submitPostBtn.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            submitPostBtn.ForeColor = Color.White;
            submitPostBtn.Location = new Point(630, 410);
            submitPostBtn.Name = "submitPostBtn";
            submitPostBtn.Size = new Size(140, 40);
            submitPostBtn.TabIndex = 5;
            submitPostBtn.Text = "Submit Post";
            submitPostBtn.UseVisualStyleBackColor = false;
            submitPostBtn.Click += submitPostBtn_Click;
            //
            // createPost
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            Controls.Add(mainPanel);
            Name = "createPost";
            Size = new Size(900, 600);
            mainPanel.ResumeLayout(false);
            mainPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel mainPanel;
        private Label headerLabel;
        private Label label1;
        private TextBox titleBox;
        private Label label2;
        private RichTextBox contentBox;
        private Button submitPostBtn;
    }
}
