using System.Windows;
using PinkWpfCalculator.ViewModels;

namespace PinkWpfCalculator.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Подключаем наш ViewModel к интерфейсу
            DataContext = new MainViewModel();
        }
    }
}