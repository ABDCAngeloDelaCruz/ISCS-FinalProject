using System;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Xml;

namespace FinalProject
{
    public partial class Register : UserControl
    {
        private XmlDocument? doc;
        private XmlElement? root;
        private static readonly string relativePath = @"XMLFiles\data.xml";
        private readonly string path = Path.Combine(Environment.CurrentDirectory, relativePath);

        public Register()
        {
            InitializeComponent();
            LoadXmlData();
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
            string name = username.Text;
            string passwordInfo = password.Text;

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

            XmlElement userElement = doc.CreateElement("user");

            XmlElement usernameElement = doc.CreateElement("username");
            usernameElement.InnerText = name;
            userElement.AppendChild(usernameElement);

            XmlElement passwordElement = doc.CreateElement("password");
            passwordElement.InnerText = hashedPassword;
            userElement.AppendChild(passwordElement);

            XmlNode? usersNode = root.SelectSingleNode("/data/users");

            if (usersNode == null)
            {
                usersNode = doc.CreateElement("users");
                root.AppendChild(usersNode);
            }

            usersNode.AppendChild(userElement);

            doc.Save(path);
            MessageBox.Show("Registration Successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Navigate to the login view
            var form = FindForm() as Form1;
            form?.LoadView(new Login());
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
