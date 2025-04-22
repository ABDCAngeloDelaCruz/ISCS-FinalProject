using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text.Json;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace FinalProject
{
    public partial class Register : UserControl
    {
        private readonly string IPAddress = "127.0.0.1";
        private XmlDocument? doc;
        private XmlElement? root;
        private static readonly string relativePath = @"XMLFiles\data.xml";
        private readonly string path = @"C:\Users\Zygos\Documents\ISCS\FinalProject\ForumServer\bin\Debug\net9.0\XMLFiles\data.xml";

        public Register()
        {
            InitializeComponent();
            LoadXmlData();

            // Set focus to the username field when the form loads
            Load += (s, e) => username.Focus();

            // Add hover effect to submit button
            submit.MouseEnter += (s, e) => submit.BackColor = Color.FromArgb(32, 33, 36);
            submit.MouseLeave += (s, e) => submit.BackColor = Color.FromArgb(23, 24, 29);

            // Add Enter key handling for password field
            password.KeyDown += (s, e) => {
                if (e.KeyCode == Keys.Enter)
                {
                    Submit_Click(submit, EventArgs.Empty);
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
            };
        }

        private void LoadXmlData()
        {
            try
            {
                doc = new XmlDocument();
                doc.Load(path);
                root = doc.DocumentElement;

                if (root == null)
                {
                    MessageBox.Show("No data found in XML file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading XML data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Submit_Click(object sender, EventArgs e)
        {
            string name = username.Text.Trim();
            string passwordInfo = password.Text.Trim();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(passwordInfo))
            {
                MessageBox.Show("Please fill in both the username and password.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (doc == null || root == null)
            {
                MessageBox.Show("Error: XML data not loaded properly.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            XmlNode? existingUser = root.SelectSingleNode($"/data/users/user[username='{name}']");
            if (existingUser != null)
            {
                MessageBox.Show("Username already exists. Please choose a different one.", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string hashedPassword = PasswordHelper.HashPassword(passwordInfo);

            var request = new
            {
                action = "register",
                username = name,
                password = hashedPassword,
            };

            try
            {
                string requestJson = JsonSerializer.Serialize(request);

                using TcpClient client = new(IPAddress, 8888);
                using NetworkStream stream = client.GetStream();
                using StreamWriter writer = new(stream) { AutoFlush = true };
                using StreamReader reader = new(stream);

                writer.WriteLine(requestJson);

                string? responseJson = reader.ReadLine();
                if (responseJson != null)
                {
                    var response = JsonSerializer.Deserialize<Dictionary<string, string>>(responseJson);

                    if (response != null && response.TryGetValue("status", out string? status) && status == "success")
                    {
                        MessageBox.Show("Registration successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        username.Text = string.Empty;
                        password.Text = string.Empty;

                        // Navigate to the login view
                        if (FindForm() is Form1 form)
                        {
                            form.LoadView(new Login());
                        }
                        return;
                    }
                    else if (response != null && response.TryGetValue("message", out string? message))
                    {
                        MessageBox.Show($"Error: {message}", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                MessageBox.Show("Registration failed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Server error: {ex.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    public class PasswordHelper
    {
        public static string HashPassword(string password)
        {
            byte[] salt = new byte[16];
            RandomNumberGenerator.Fill(salt);

            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                10000,
                HashAlgorithmName.SHA256,
                20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            return Convert.ToBase64String(hashBytes);
        }

        public static bool VerifyPassword(string enteredPassword, string storedHash)
        {
            try
            {
                byte[] hashBytes = Convert.FromBase64String(storedHash);

                byte[] salt = new byte[16];
                Array.Copy(hashBytes, 0, salt, 0, 16);

                byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                    enteredPassword,
                    salt,
                    10000,
                    HashAlgorithmName.SHA256,
                    20);

                for (int i = 0; i < 20; i++)
                {
                    if (hashBytes[i + 16] != hash[i])
                    {
                        return false;
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
