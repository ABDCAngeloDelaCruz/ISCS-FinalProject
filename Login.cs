using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace FinalProject
{
    public partial class Login : UserControl
    {
        string IPAddress = "127.0.0.1";
        XmlElement root;
        static string relativePath = @"XMLFiles\data.xml";
        string path = Path.Combine(Environment.CurrentDirectory, relativePath);

        public Login()
        {
            InitializeComponent();
        }

        private void submit_Click(object sender, EventArgs e)
        {
            string enteredUsername = username.Text.Trim();
            string enteredPassword = password.Text.Trim();

            if (string.IsNullOrEmpty(enteredUsername) || string.IsNullOrEmpty(enteredPassword))
            {
                MessageBox.Show("Please fill in both the username and password.");
                return;
            }

            var loginRequest = new
            {
                action = "login",
                username = enteredUsername,
                password = enteredPassword
            };

            string requestJson = JsonSerializer.Serialize(loginRequest);

            try
            {
                using (TcpClient client = new TcpClient(IPAddress, 8888))
                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                using (StreamWriter writer = new StreamWriter(stream) { AutoFlush = true })
                {
                    writer.WriteLine(requestJson);
                    string responseJson = reader.ReadLine();

                    var response = JsonSerializer.Deserialize<Dictionary<string, string>>(responseJson);

                    if (response["status"] == "success")
                    {
                        Session.LoggedInUsername = enteredUsername;
                        MessageBox.Show("Login successful!");
                    }
                    else
                    {
                        MessageBox.Show(response["message"]);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error: {e.Message}");
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
