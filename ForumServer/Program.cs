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

            using NetworkStream stream = client.GetStream();
            using StreamReader reader = new StreamReader(stream);
            using StreamWriter writer = new StreamWriter(stream) { AutoFlush = true };

            object response;

            try
            {
                string requestJson = reader.ReadLine();
                Console.WriteLine($"Received JSON:\n{requestJson}");

                JsonDocument doc = JsonDocument.Parse(requestJson);
                JsonElement root = doc.RootElement;

                string action = root.GetProperty("action").GetString();

                switch (action)
                {
                    case "register":
                        response = HandleRegister(root);
                        break;

                    case "login":
                        response = HandleLogin(root);
                        break;

                    case "createPost":
                        response = HandleCreatePost(root);
                        break;

                    case "newPosts":
                        response = HandleNewPosts();
                        break;

                    default:
                        response = new { status = "error", message = "Invalid action." };
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Server error: {ex.Message}");
                response = new { status = "error", message = "Invalid request or internal error." };
            }

            string responseJson = JsonSerializer.Serialize(response);
            writer.WriteLine(responseJson);

            client.Close();
        }
    }

    static object HandleRegister(JsonElement root)
    {
        string username = root.GetProperty("username").GetString();
        string password = root.GetProperty("password").GetString();

        if (UsernameExists(username))
        {
            return new { status = "error", message = "Username already taken." };
        }

        AddUserToXML(username, password);
        return new { status = "success", message = "Registration successful." };
    }
    static object HandleLogin(JsonElement root)
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
                return new { status = "success", message = "Login successful." };
            }
            else
            {
                return new { status = "error", message = "Incorrect password." };
            }
        }

        return new { status = "error", message = "Username not found." };
    }

    static object HandleCreatePost(JsonElement root)
    {
        string title = root.GetProperty("title").GetString();
        string content = root.GetProperty("content").GetString();
        string author = root.GetProperty("author").GetString();
        string timestamp = root.GetProperty("timestamp").GetString();

        try
        {
            AddPostToXML(title, content, author, timestamp);
            return new { status = "success", message = "Post created successfully." };
        }
        catch (Exception ex)
        {
            return new { status = "error", message = $"Failed to create post: {ex.Message}" };
        }
    }
    static object HandleNewPosts()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(path);
        XmlNodeList posts = doc.SelectNodes("/data/posts/post");

        List<object> postList = new List<object>();

        foreach (XmlNode post in posts)
        {
            postList.Add(new
            {
                id = post.Attributes["id"]?.Value ?? "",
                title = post["title"]?.InnerText ?? "",
                content = post["content"]?.InnerText ?? "",
                author = post["author"]?.InnerText ?? "",
                timestamp = post["timestamp"]?.InnerText ?? ""
            });
        }

        return new { status = "success", posts = postList };
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

        XmlNode usersNode = doc.SelectSingleNode("//users") ?? doc.DocumentElement.AppendChild(doc.CreateElement("users"));

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

    static void AddPostToXML(string title, string content, string author, string timestamp)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(path);
        XmlNode rootNode = xmlDoc.DocumentElement;

        XmlNode postsNode = rootNode.SelectSingleNode("posts");
        if (postsNode == null)
        {
            postsNode = xmlDoc.CreateElement("posts");
            rootNode.AppendChild(postsNode);
        }

        XmlElement postElement = xmlDoc.CreateElement("post");
        postElement.SetAttribute("id", Guid.NewGuid().ToString());

        void AddChild(string tag, string text)
        {
            XmlElement elem = xmlDoc.CreateElement(tag);
            elem.InnerText = text;
            postElement.AppendChild(elem);
        }

        AddChild("title", title);
        AddChild("content", content);
        AddChild("author", author);
        AddChild("timestamp", timestamp);
        postElement.AppendChild(xmlDoc.CreateElement("comments"));

        postsNode.AppendChild(postElement);
        xmlDoc.Save(path);
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
