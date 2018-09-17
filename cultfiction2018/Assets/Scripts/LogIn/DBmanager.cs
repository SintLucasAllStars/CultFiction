namespace LogIn
{
    public static class DBmanager
    {
        public static string Username;
        public static int Score;
        public static int Money;
        
        public static bool LoggedIn
        {
            get { return Username != null; }
        }

        public static int HeadbandValue;
        public static int GlassesValue;
        public static int JewelryValue;
        public static int ShoeValue;

        public static bool UnlockedHeadband;
        public static bool UnlockedGlasses;
        public static bool UnlockedJewelry;
        public static bool UnlockedShoes;

        

        public static void LogOut()
        {
            Username = null;
        }
    }
}