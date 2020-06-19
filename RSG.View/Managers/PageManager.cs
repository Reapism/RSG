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
        private readonly Page selectedPage;
        private readonly Page previousPage;

        public PageManager()
        {

        }

        public PageManager(Page previousPage, Page selectedPage)
        {
            this.previousPage = previousPage;
            this.selectedPage = selectedPage;
        }

        public static Page GetPage(int index)
        {
            var page = (Page)index;
            return page;
        }

        public void SetNavigationalMenuTabPage(Selector selector)
        {
            selector.SelectedIndex = (int)selectedPage;
        }

        private void TransitionPages()
        {

        }
    }
}
