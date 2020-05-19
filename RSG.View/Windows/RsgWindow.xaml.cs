using Microsoft.Extensions.DependencyInjection;
using RSG.Core;
using RSG.Core.Factories;
using RSG.Core.Interfaces;
using RSG.Core.Models;
using RSG.Core.Services;
using RSG.Core.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace RSG.View.Windows
{
    /// <summary>
    /// Interaction logic for RsgWindow.xaml
    /// </summary>
    public partial class RsgWindow : Window
    {
        private DictionaryService dictionaryService;

        public RsgWindow()
        {
            InitializeComponent();
            InitializeDependencies();
        }

        private void InitializeDependencies()
        {
            dictionaryService = App.Container.Provider.GetService<DictionaryService>();
        }
    }
}
