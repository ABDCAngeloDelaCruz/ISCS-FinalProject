using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Security.Cryptography;
using System.Text;

namespace FinalProject
{
    public partial class Register : UserControl
    {
        XmlDocument doc;
        XmlElement root;
        static string relativePath = @"XMLFiles\data.xml";
        string path = Path.Combine(Environment.CurrentDirectory, relativePath);
        public Register()
        {
            InitializeComponent();
            Register_Load(this, EventArgs.Empty);
        }

        public void Register_Load(object sender, EventArgs e)
        {
            doc = new XmlDocument();
            doc.Load(path);
            root = doc.DocumentElement;

            if (root != null){
                XmlNodeList users = root.SelectNodes("user");
                foreach (XmlNode user in users)
                {
                    string username = user["username"].InnerText;
                    string password = user["password"].InnerText;
                }
            }
            else
            {
                MessageBox.Show("No data found.");
            }
        }

        private void submit_Click(object sender, EventArgs e)
        {
            string name = username.Text;
            string passwordInfo = password.Text;

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(passwordInfo))
            {
                MessageBox.Show("Please fill in both the username and password.");
                return;
            }

            XmlNode existingUser = root.SelectSingleNode($"users/user[username='{name}']");
            if (existingUser != null)
            {
                MessageBox.Show("Username already exists. Please choose a different one.");
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

            XmlNode usersNode = root.SelectSingleNode("users");

            if (usersNode == null)
            {
                usersNode = doc.CreateElement("users");
                root.AppendChild(usersNode); 
            }

            usersNode.AppendChild(userElement);

            doc.Save(path);
            MessageBox.Show("Registration Successful");

            username.Text = string.Empty;
            password.Text = string.Empty;
        }
    }
    public class PasswordHelper
    {
        // Hash the password with a salt
        public static string HashPassword(string password)
        {
            // Create a salt (random bytes)
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[16];
                rng.GetBytes(salt); // Generate a 16-byte salt

                // Use PBKDF2 to hash the password with the salt
                using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000))  // 10,000 iterations
                {
                    byte[] hash = pbkdf2.GetBytes(20);  // Generate a 20-byte hash

                    // Combine the salt and hash into a single byte array to store
                    byte[] hashBytes = new byte[36];
                    Array.Copy(salt, 0, hashBytes, 0, 16);
                    Array.Copy(hash, 0, hashBytes, 16, 20);

                    // Convert the combined result to a base64 string and return
                    return Convert.ToBase64String(hashBytes);
                }
            }
        }

        internal static bool VerifyPassword(string enteredPassword, string storedHashedPassword)
        {
            throw new NotImplementedException();
        }
    }

}
