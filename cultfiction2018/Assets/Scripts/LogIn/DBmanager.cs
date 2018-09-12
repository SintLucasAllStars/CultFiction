using UnityEditor;
using UnityEngine;

namespace LogIn
{
    public static class DBmanager
    {
        public static string Username;
        public static int Score;
        
        public static bool LoggedIn
        {
            get { return Username != null; }
        }

        public static void LogOut()
        {
            Username = null;
        }
    }
}