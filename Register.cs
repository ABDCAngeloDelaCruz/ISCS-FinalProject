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
using System.Text.Json;
using System.Net.Sockets;

namespace FinalProject
{
    public partial class Register : UserControl
    {
        string IPAddress = "127.0.0.1";

        public Register()
        {
            InitializeComponent();
        }

        private void submit_Click(object sender, EventArgs e)
        {
            string name = username.Text.Trim();
            string passwordInfo = password.Text.Trim();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(passwordInfo))
            {
                MessageBox.Show("Please fill in both the username and password.");
                return;
            }

            string hashedPassword = PasswordHelper.HashPassword(passwordInfo);

            var request = new
            {
                action = "register",
                username = name,
                password = hashedPassword,
            };

            string requestJson = JsonSerializer.Serialize(request);

            try
            {
                using (TcpClient client = new TcpClient(IPAddress, 8888))
                using (NetworkStream stream = client.GetStream())
                using (StreamWriter writer = new StreamWriter(stream) { AutoFlush = true })
                using (StreamReader reader = new StreamReader(stream))
                {
                    writer.WriteLine(requestJson);

                    string responseJson = reader.ReadLine();
                    var response = JsonSerializer.Deserialize<Dictionary<string, string>>(responseJson);

                    if (response["status"] == "success")
                    {
                        MessageBox.Show("Registration successful!");
                        username.Text = string.Empty;
                        password.Text = string.Empty;
                    }
                    else
                    {
                        MessageBox.Show($"Error: {response["message"]}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Server error: {ex.Message}");
            }
        }
    }
    public class PasswordHelper
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
    }
}
