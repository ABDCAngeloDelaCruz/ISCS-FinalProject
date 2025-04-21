using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace FinalProject
{
    public partial class Login : UserControl
    {
        XmlDocument doc;
        XmlElement root;
        static string relativePath = @"XMLFiles\data.xml";
        string path = Path.Combine(Environment.CurrentDirectory, relativePath);

        public Login()
        {
            InitializeComponent();
            Login_Load(this, EventArgs.Empty);
        }

        public void Login_Load(object sender, EventArgs e)
        {
            doc = new XmlDocument();
            doc.Load(path);
            root = doc.DocumentElement;
            if (root != null)
            {
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
            string enteredUsername = username.Text;
            string enteredPassword = password.Text;

            if (string.IsNullOrEmpty(enteredUsername) || string.IsNullOrEmpty(enteredPassword))
            {
                MessageBox.Show("Please fill in both the username and password.");
                return;
            }

            bool isLoginSuccessful = VerifyLogin(enteredUsername, enteredPassword);

            if (isLoginSuccessful)
            {
                Session.LoggedInUsername = enteredUsername;
                MessageBox.Show("Login successful!");
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }

        private bool VerifyLogin(string enteredUsername, string enteredPassword)
        {
            XmlNode userNode = root.SelectSingleNode($"users/user[username='{enteredUsername}']");

            if (userNode != null)
            {
                string storedHashedPassword = userNode["password"].InnerText;

                return LoginPasswordHelper.VerifyPassword(enteredPassword, storedHashedPassword);
            }

            return false;
        }
    }

    public static class LoginPasswordHelper
    {
        public static string HashPassword(string password)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[16];
                rng.GetBytes(salt);

                using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000))
                {
                    byte[] hash = pbkdf2.GetBytes(20);

                    byte[] hashBytes = new byte[36];
                    Array.Copy(salt, 0, hashBytes, 0, 16);
                    Array.Copy(hash, 0, hashBytes, 16, 20);

                    return Convert.ToBase64String(hashBytes);
                }
            }
        }

        public static bool VerifyPassword(string enteredPassword, string storedHash)
        {
            byte[] hashBytes = Convert.FromBase64String(storedHash);

            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            using (var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, salt, 10000))
            {
                byte[] hash = pbkdf2.GetBytes(20); 

                for (int i = 0; i < 20; i++)
                {
                    if (hashBytes[i + 16] != hash[i]) 
                    {
                        return false; 
                    }
                }
            }

            return true; 
        }
    }
}
