using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    internal class Session
    {
        private static string _loggedInUsername;

        public static string LoggedInUsername
        {
            get { return _loggedInUsername; }
            set
            {
                _loggedInUsername = value;
                // Raise event when login status changes
                OnLoginStatusChanged(new LoginStatusChangedEventArgs { IsLoggedIn = !string.IsNullOrEmpty(value) });
            }
        }

        public static bool IsLoggedIn
        {
            get { return !string.IsNullOrEmpty(LoggedInUsername); }
        }

        public static void Logout()
        {
            LoggedInUsername = null;
        }

        // Event for login status changes
        public static event EventHandler<LoginStatusChangedEventArgs> LoginStatusChanged;

        private static void OnLoginStatusChanged(LoginStatusChangedEventArgs e)
        {
            LoginStatusChanged?.Invoke(null, e);
        }
    }

    public class LoginStatusChangedEventArgs : EventArgs
    {
        public bool IsLoggedIn { get; set; }
    }
}
