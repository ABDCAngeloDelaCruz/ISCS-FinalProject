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
            label1 = new Label();
            titleBox = new TextBox();
            label2 = new Label();
            contentBox = new RichTextBox();
            submitPostBtn = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(44, 39);
            label1.Name = "label1";
            label1.Size = new Size(30, 15);
            label1.TabIndex = 0;
            label1.Text = "Title";
            // 
            // titleBox
            // 
            titleBox.Location = new Point(44, 57);
            titleBox.Name = "titleBox";
            titleBox.Size = new Size(154, 23);
            titleBox.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(44, 103);
            label2.Name = "label2";
            label2.Size = new Size(50, 15);
            label2.TabIndex = 2;
            label2.Text = "Content";
            // 
            // contentBox
            // 
            contentBox.Location = new Point(44, 121);
            contentBox.Name = "contentBox";
            contentBox.Size = new Size(525, 126);
            contentBox.TabIndex = 4;
            contentBox.Text = "";
            // 
            // submitPostBtn
            // 
            submitPostBtn.Location = new Point(44, 269);
            submitPostBtn.Name = "submitPostBtn";
            submitPostBtn.Size = new Size(75, 23);
            submitPostBtn.TabIndex = 5;
            submitPostBtn.Text = "Submit";
            submitPostBtn.UseVisualStyleBackColor = true;
            submitPostBtn.Click += submitPostBtn_Click;
            // 
            // createPost
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(submitPostBtn);
            Controls.Add(contentBox);
            Controls.Add(titleBox);
            Controls.Add(label1);
            Controls.Add(label2);
            Name = "createPost";
            Size = new Size(622, 469);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox titleBox;
        private Label label2;
        private RichTextBox contentBox;
        private Button submitPostBtn;
    }
}
