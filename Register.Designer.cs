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
            mainPanel = new Panel();
            headerLabel = new Label();
            submit = new Button();
            password = new TextBox();
            label1 = new Label();
            username = new TextBox();
            label2 = new Label();
            mainPanel.SuspendLayout();
            SuspendLayout();
            //
            // mainPanel
            //
            mainPanel.Anchor = AnchorStyles.None;
            mainPanel.BackColor = Color.White;
            mainPanel.BorderStyle = BorderStyle.FixedSingle;
            mainPanel.Controls.Add(headerLabel);
            mainPanel.Controls.Add(submit);
            mainPanel.Controls.Add(password);
            mainPanel.Controls.Add(label1);
            mainPanel.Controls.Add(username);
            mainPanel.Controls.Add(label2);
            mainPanel.Location = new Point(250, 100);
            mainPanel.Name = "mainPanel";
            mainPanel.Size = new Size(400, 400);
            mainPanel.Padding = new Padding(30);
            mainPanel.TabIndex = 0;
            //
            // headerLabel
            //
            headerLabel.AutoSize = true;
            headerLabel.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            headerLabel.Location = new Point(140, 30);
            headerLabel.Name = "headerLabel";
            headerLabel.Size = new Size(120, 32);
            headerLabel.TabIndex = 11;
            headerLabel.Text = "Register";
            //
            // submit
            //
            submit.BackColor = Color.FromArgb(23, 24, 29);
            submit.FlatStyle = FlatStyle.Flat;
            submit.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            submit.ForeColor = Color.White;
            submit.Location = new Point(230, 250);
            submit.Name = "submit";
            submit.Size = new Size(140, 40);
            submit.TabIndex = 10;
            submit.Text = "Register";
            submit.UseVisualStyleBackColor = false;
            submit.Click += Submit_Click;
            //
            // password
            //
            password.Location = new Point(30, 185);
            password.Name = "password";
            password.PasswordChar = '*';
            password.Size = new Size(340, 29);
            password.TabIndex = 9;
            password.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            //
            // label1
            //
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(30, 160);
            label1.Name = "label1";
            label1.Size = new Size(83, 21);
            label1.TabIndex = 8;
            label1.Text = "Password";
            //
            // username
            //
            username.Location = new Point(30, 115);
            username.Name = "username";
            username.Size = new Size(340, 29);
            username.TabIndex = 7;
            username.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            //
            // label2
            //
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(30, 90);
            label2.Name = "label2";
            label2.Size = new Size(87, 21);
            label2.TabIndex = 6;
            label2.Text = "Username";
            //
            // Register
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            Controls.Add(mainPanel);
            Name = "Register";
            Size = new Size(900, 600);
            Dock = DockStyle.Fill;
            mainPanel.ResumeLayout(false);
            mainPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel mainPanel;
        private Label headerLabel;
        private Button submit;
        private TextBox password;
        private Label label1;
        private TextBox username;
        private Label label2;
    }
}
