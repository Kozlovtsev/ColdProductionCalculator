using ColdProductionCalculator.ViewModels;
using System.Windows;

namespace ColdProductionCalculator.Views
{
    public partial class MainWindow : Window
    {
        public MainViewModel ViewModel { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new MainViewModel();
            DataContext = ViewModel;
        }

        private void OpenHistory_Click(object sender, RoutedEventArgs e)
        {
            var window = new HistoryWindow();
            window.ShowDialog();
        }
    }
}