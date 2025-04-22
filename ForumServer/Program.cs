using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text.Json;
using System.Xml;

class Program
{
    static string relativePath = @"XMLFiles\data.xml";
    static string path = Path.Combine(Environment.CurrentDirectory, relativePath);

    static void Main()
    {
        TcpListener listener = new TcpListener(IPAddress.Any, 8888);
        listener.Start();
        Console.WriteLine("Server is running and listening on port 8888\n");

        while (true)
        {
            TcpClient client = listener.AcceptTcpClient();
            Console.WriteLine("New client connected.");

            using (NetworkStream stream = client.GetStream())
            using (StreamReader reader = new StreamReader(stream))
            using (StreamWriter writer = new StreamWriter(stream) { AutoFlush = true })
            {
                string requestJson = reader.ReadLine();
                Console.WriteLine("Received JSON:");
                Console.WriteLine(requestJson);

                JsonDocument doc = JsonDocument.Parse(requestJson);
                JsonElement root = doc.RootElement;
                string action = root.GetProperty("action").GetString();

                object response;

                if (action == "register")
                {
                    string username = root.GetProperty("username").GetString();
                    string password = root.GetProperty("password").GetString();

                    if (UsernameExists(username))
                    {
                        response = new { status = "error", message = "Username already taken." };
                    }
                    else
                    {
                        AddUserToXML(username, password);
                        response = new { status = "success", message = "Registration successful." };
                    }
                }
                else if (action == "login")
                {
                    string username = root.GetProperty("username").GetString();
                    string password = root.GetProperty("password").GetString();

                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(path);
                    XmlNode userNode = xmlDoc.SelectSingleNode($"//users/user[username='{username}']");

                    if (userNode != null)
                    {
                        string storedHash = userNode["password"].InnerText;

                        if (VerifyPassword(password, storedHash))
                        {
                            response = new { status = "success", message = "Login successful." };
                        }
                        else
                        {
                            response = new { status = "error", message = "Incorrect password." };
                        }
                    }
                    else
                    {
                        response = new { status = "error", message = "Username not found." };
                    }
                }
                else if (action == "createPost")
                {

                }
                else if (action == "getPosts")
                {
                    
                }
                else
                {
                    response = new { status = "error", message = "An error occurred." };
                }

                string responseJson = JsonSerializer.Serialize(response);
                writer.Write(responseJson);
            }

            client.Close();
        }
    }

    static bool UsernameExists(string username)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(path);
        XmlNode userNode = doc.SelectSingleNode($"//users/user[username='{username}']");
        return userNode != null;
    }

    static void AddUserToXML(string username, string hashedPassword)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(path);

        XmlNode usersNode = doc.SelectSingleNode("//users");
        if (usersNode == null)
        {
            usersNode = doc.CreateElement("users");
            doc.DocumentElement.AppendChild(usersNode);
        }

        XmlElement user = doc.CreateElement("user");

        XmlElement userElem = doc.CreateElement("username");
        userElem.InnerText = username;
        user.AppendChild(userElem);

        XmlElement passElem = doc.CreateElement("password");
        passElem.InnerText = hashedPassword;
        user.AppendChild(passElem);

        usersNode.AppendChild(user);
        doc.Save(path);
    }

    static bool VerifyPassword(string enteredPassword, string storedHash)
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
                    return false;
            }
        }

        return true;
    }
}
