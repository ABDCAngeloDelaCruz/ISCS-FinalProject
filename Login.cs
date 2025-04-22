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
    public partial class Login : UserControl
    {
        private readonly string IPAddress = "127.0.0.1";
        private XmlDocument? doc;
        private XmlElement? root;
        private static readonly string relativePath = @"XMLFiles\data.xml";
        private readonly string path = @"C:\Users\Zygos\Documents\ISCS\FinalProject\ForumServer\bin\Debug\net9.0\XMLFiles\data.xml";

        public Login()
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
            string enteredUsername = username.Text.Trim();
            string enteredPassword = password.Text.Trim();

            if (string.IsNullOrEmpty(enteredUsername) || string.IsNullOrEmpty(enteredPassword))
            {
                MessageBox.Show("Please fill in both the username and password.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // First try to authenticate with XML file
            if (AuthenticateWithXml(enteredUsername, enteredPassword))
            {
                LoginSuccessful(enteredUsername);
                return;
            }

            // If XML authentication fails, try server authentication
            try
            {
                var loginRequest = new
                {
                    action = "login",
                    username = enteredUsername,
                    password = enteredPassword
                };

                string requestJson = JsonSerializer.Serialize(loginRequest);

                using TcpClient client = new(IPAddress, 8888);
                using NetworkStream stream = client.GetStream();
                using StreamReader reader = new(stream);
                using StreamWriter writer = new(stream) { AutoFlush = true };

                writer.WriteLine(requestJson);
                string? responseJson = reader.ReadLine();

                if (responseJson != null)
                {
                    var response = JsonSerializer.Deserialize<Dictionary<string, string>>(responseJson);

                    if (response != null && response.TryGetValue("status", out string? status) && status == "success")
                    {
                        LoginSuccessful(enteredUsername);
                        return;
                    }
                }

                // If we get here, login failed
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Server connection failed, but we already tried XML authentication
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"Server connection error: {ex.Message}");
            }
        }

        private bool AuthenticateWithXml(string username, string password)
        {
            try
            {
                if (doc == null || root == null)
                {
                    LoadXmlData();
                }

                if (root == null) return false;

                // Find the user in the XML file
                XmlNode? userNode = root.SelectSingleNode($"//users/user[username='{username}']");

                if (userNode == null)
                {
                    // Try a different XPath if the first one doesn't work
                    userNode = root.SelectSingleNode($"//user[username='{username}']");
                }

                if (userNode != null)
                {
                    XmlNode? passwordNode = userNode.SelectSingleNode("password");
                    if (passwordNode != null)
                    {
                        string storedPassword = passwordNode.InnerText;

                        // First try direct comparison (for testing/development)
                        if (password == storedPassword)
                        {
                            return true;
                        }

                        // Then try hashed password verification
                        if (LoginPasswordHelper.VerifyPassword(password, storedPassword))
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"XML authentication error: {ex.Message}");
                return false;
            }
        }

        private void LoginSuccessful(string username)
        {
            Session.LoggedInUsername = username;
            MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Navigate to the posts view
            if (FindForm() is Form1 form)
            {
                form.LoadView(new newPosts());
            }
        }
    }

    public static class LoginPasswordHelper
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
