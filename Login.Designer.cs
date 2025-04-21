namespace FinalProject
{
    partial class Login
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
            label2 = new Label();
            username = new TextBox();
            password = new TextBox();
            label1 = new Label();
            submit = new Button();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(32, 43);
            label2.Name = "label2";
            label2.Size = new Size(111, 15);
            label2.TabIndex = 1;
            label2.Text = "User Name (LOGIN)";
            // 
            // username
            // 
            username.Location = new Point(32, 61);
            username.Name = "username";
            username.Size = new Size(216, 23);
            username.TabIndex = 2;
            // 
            // password
            // 
            password.Location = new Point(32, 121);
            password.Name = "password";
            password.Size = new Size(216, 23);
            password.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(32, 103);
            label1.Name = "label1";
            label1.Size = new Size(60, 15);
            label1.TabIndex = 3;
            label1.Text = "Password:";
            // 
            // submit
            // 
            submit.Location = new Point(80, 170);
            submit.Name = "submit";
            submit.Size = new Size(75, 23);
            submit.TabIndex = 5;
            submit.Text = "Submit";
            submit.UseVisualStyleBackColor = true;
            submit.Click += submit_Click;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(submit);
            Controls.Add(password);
            Controls.Add(label1);
            Controls.Add(username);
            Controls.Add(label2);
            Name = "Login";
            Size = new Size(687, 402);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label2;
        private TextBox username;
        private TextBox password;
        private Label label1;
        private Button submit;
    }
}
