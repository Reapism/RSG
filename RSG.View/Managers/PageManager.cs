using RSG.Core.Extensions;
using RSG.View.Converters;
using System;
using System.ComponentModel;
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

    public class PageManager
    {
        public PageManager()
        {

        }

        public Page GetPage(string tagValue)
        {
            var page = EnumExtensions.GetEnumValue<Page>(tagValue);

            return page;
        }

        public void SetNavigationalMenuTabPage(Selector selector, string tag)
        {
            if (string.IsNullOrEmpty(tag))
            {
                throw new Exception($"{tag} cannot be null or empty.");
            }

            selector.SelectedIndex = (int)GetPage(tag);
        }

        private void TransitionPages()
        {

        }
    }
}
