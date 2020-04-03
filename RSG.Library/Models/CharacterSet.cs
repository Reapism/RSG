using System;
using System.Collections.Generic;
using System.Text;

namespace RSG.Library.Models
{
    public class CharacterSet
    {
        public CharacterSet()
        {
            UseCustom = false;
            UseLowercase = true;
            UseNumbers = true;
            UsePunctuation = false;
            UseSymbols = false;
            UseUppercase = true;
        }

        public string Current { get; set; }
        
        public bool UseLowercase { get; set; }
        public bool UseUppercase { get; set; }
        public bool UseNumbers { get; set; }
        public bool UseSymbols { get; set; }
        public bool UsePunctuation { get; set; }
        public bool UseCustom { get; set; }

        // Static 
        private static string Lowercase { get; }
        private static string Uppercase { get; }
        private static string Numbers { get; }
        private static string Symbols { get; }
        private static string Punctuation { get; }
    }
}
