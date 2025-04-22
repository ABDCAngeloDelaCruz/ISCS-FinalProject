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
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(300, 150);
            label2.Name = "label2";
            label2.Size = new Size(111, 15);
            label2.TabIndex = 1;
            label2.Text = "Username";
            //
            // username
            //
            username.Location = new Point(250, 180);
            username.Name = "username";
            username.Size = new Size(250, 30);
            username.TabIndex = 2;
            username.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            //
            // password
            //
            password.Location = new Point(250, 250);
            password.Name = "password";
            password.PasswordChar = '*';
            password.Size = new Size(250, 30);
            password.TabIndex = 4;
            password.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            //
            // label1
            //
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(300, 220);
            label1.Name = "label1";
            label1.Size = new Size(60, 15);
            label1.TabIndex = 3;
            label1.Text = "Password";
            //
            // submit
            //
            submit.Location = new Point(325, 300);
            submit.Name = "submit";
            submit.Size = new Size(100, 40);
            submit.TabIndex = 5;
            submit.Text = "Login";
            submit.UseVisualStyleBackColor = true;
            submit.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            submit.FlatStyle = FlatStyle.Flat;
            submit.Click += Submit_Click;
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
            Dock = DockStyle.Fill;
            BackColor = Color.White;
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
