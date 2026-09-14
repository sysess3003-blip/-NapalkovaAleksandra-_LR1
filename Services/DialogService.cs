using System.Windows;

namespace PinkWpfCalculator.Services
{
    public class DialogService : IDialogService
    {
        public void ShowWarning(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}