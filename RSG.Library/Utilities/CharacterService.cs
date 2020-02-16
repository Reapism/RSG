namespace RSG.Library.Utilities
{
    public static class CharacterService
    {
        static CharacterService()
        {
            Lowercase = "abcdefghijklmnopqrstuvwxyz";
            Uppercase = "ABCDEFGHIJKLMNAOPQRSTUVWXYZ";
            Numbers = "0123456789";
            SpecialCharacters = " !@#$%^&*()-=_+,./;\'\\<>?:\"[]{}|`~";
            PasswordSet = "";
        }

        public static string Lowercase { get; }
        public static string Uppercase { get; }
        public static string Numbers { get; }
        public static string SpecialCharacters { get; }
        public static string PasswordSet { get; }
    }
}
