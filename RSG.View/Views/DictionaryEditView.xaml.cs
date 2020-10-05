using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RSG.View.Views
{
    /// <summary>
    /// Interaction logic for DictionaryEditView.xaml
    /// </summary>
    public partial class DictionaryEditView : UserControl
    {
        public DictionaryEditView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var tasks = new Task[10];
            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = Task.Run(() =>
                {
                    //MessageBox.Show($"Start - {i}");
                    Thread.Sleep(2000);
                    //MessageBox.Show($"End - {i}");
                });
            }

            try
            {
                Task.WaitAll(tasks);
            }
            catch (AggregateException ae)
            {
                foreach(var ex in ae.Flatten().InnerExceptions)
                {
                    MessageBox.Show($"{ex.Message}");
                }
            }

            MessageBox.Show("Done");
        }
    }
}
