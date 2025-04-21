namespace FinalProject
{
    partial class Register
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
            submit = new Button();
            password = new TextBox();
            label1 = new Label();
            username = new TextBox();
            label2 = new Label();
            SuspendLayout();
            // 
            // submit
            // 
            submit.Location = new Point(205, 230);
            submit.Name = "submit";
            submit.Size = new Size(75, 23);
            submit.TabIndex = 10;
            submit.Text = "Submit";
            submit.UseVisualStyleBackColor = true;
            submit.Click += submit_Click;
            // 
            // password
            // 
            password.Location = new Point(157, 181);
            password.Name = "password";
            password.PasswordChar = '*';
            password.Size = new Size(216, 23);
            password.TabIndex = 9;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(157, 163);
            label1.Name = "label1";
            label1.Size = new Size(60, 15);
            label1.TabIndex = 8;
            label1.Text = "Password:";
            // 
            // username
            // 
            username.Location = new Point(157, 121);
            username.Name = "username";
            username.Size = new Size(216, 23);
            username.TabIndex = 7;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(157, 103);
            label2.Name = "label2";
            label2.Size = new Size(68, 15);
            label2.TabIndex = 6;
            label2.Text = "User Name:";
            // 
            // Register
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(submit);
            Controls.Add(password);
            Controls.Add(label1);
            Controls.Add(username);
            Controls.Add(label2);
            Name = "Register";
            Size = new Size(530, 357);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button submit;
        private TextBox password;
        private Label label1;
        private TextBox username;
        private Label label2;
    }
}
