using RSG.View.Managers;
using System;
using System.Windows;
using System.Windows.Controls;

namespace RSG.View.Windows
{
    /// <summary>
    /// Interaction logic for RsgWindow.xaml
    /// </summary>
    public partial class RsgWindow : Window
    {
        private PageManager pageManager;

        public RsgWindow()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception e)
            {
                throw e;
            }
            InitializeDependencies();
        }

        private void InitializeDependencies()
        {
            pageManager = IocContainer.Container.GetInstance<PageManager>();
        }

        private void NavigationMenu_Click(object sender, RoutedEventArgs e)
        {
            var selectedMenuButton = sender as Button;

            pageManager.SetNavigationalMenuTabPage(Navigation, selectedMenuButton.Tag.ToString());
        }
    }
}
