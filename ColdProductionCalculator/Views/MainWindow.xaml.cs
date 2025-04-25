using System.Windows;

namespace ColdProductionCalculator.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
           
        }

        private void OpenHistory_Click(object sender, RoutedEventArgs e)
        {
            var window = new HistoryWindow();
            window.ShowDialog();
        }
    }
}