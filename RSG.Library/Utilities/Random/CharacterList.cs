using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace RSG.Library.Utilities.Random
{
    public class CharacterList
    {
        public static string CharacterSet { get; set; }

        public static string Lowercase { get; } = "abcdefghijklmnopqrstuvwxyz";
        public static string Uppercase { get; } = "ABCDEFGHIJKLMNAOPQRSTUVWXYZ";
        public static string Numbers { get; } = "0123456789";
        public static string SpecialCharacters { get; } = " !@#$%^&*()-=_+,./;\'\\<>?:\"[]{}|`~";
        public static string PasswordSet { get; }
    }
}
