using RSG.View.Converters;
using System;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace RSG.View.Managers
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum Page
    {
        [Description("Nothing")]
        None = -1,
        [Description("String Generator")]
        String = 0,
        [Description("About")]
        About = 1,
        [Description("Dictionary Generator")]
        Dictionary = 2,
        [Description("Search strings")]
        Search = 3,
        [Description("Settings")]
        Settings = 4
    }

    internal class PageManager
    {
        public PageManager()
        {

        }

        public Page GetPage(string tagValue)
        {
            var page = GetEnumValue<Page>(tagValue);
            return page;
        }

        public void SetNavigationalMenuTabPage(Selector selector, string tag)
        {
            selector.SelectedIndex = (int)GetPage(tag);
        }

        public T GetEnumValue<T>(string value) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new Exception("T must be an Enumeration type.");
            }
            T val = ((T[])Enum.GetValues(typeof(T)))[0];
            if (!string.IsNullOrEmpty(value))
            {
                foreach (T enumValue in (T[])Enum.GetValues(typeof(T)))
                {
                    if (enumValue.ToString().ToUpper().Equals(value.ToUpper()))
                    {
                        val = enumValue;
                        break;
                    }
                }
            }

            return val;
        }

        private void TransitionPages()
        {

        }
    }
}
