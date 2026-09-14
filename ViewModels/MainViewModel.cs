using System;
using System.Windows.Input;
using PinkWpfCalculator.Infrastructure;
using PinkWpfCalculator.Models;
using PinkWpfCalculator.Services;

namespace PinkWpfCalculator.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private string _displayText = "0";
        private double _firstValue = 0;
        private string _operation = "";
        private bool _isOperationPerformed = false;

        private readonly CalculatorModel _model = new CalculatorModel();
        private readonly IDialogService _dialogService = new DialogService();

        public string DisplayText
        {
            get => _displayText;
            set { _displayText = value; OnPropertyChanged(); }
        }

        public ICommand NumCommand { get; }
        public ICommand OpCommand { get; }
        public ICommand EqualsCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand BackspaceCommand { get; }
        public ICommand CommaCommand { get; }
        public ICommand PlusMinusCommand { get; }

        public MainViewModel()
        {
            NumCommand = new RelayCommand(p => OnNumClick(p?.ToString()));
            OpCommand = new RelayCommand(p => OnOpClick(p?.ToString()));
            EqualsCommand = new RelayCommand(_ => Calculate());
            ClearCommand = new RelayCommand(_ => Clear());
            BackspaceCommand = new RelayCommand(_ => Backspace());
            CommaCommand = new RelayCommand(_ => AddComma());
            PlusMinusCommand = new RelayCommand(_ => ChangeSign());
        }

        private void OnNumClick(string digit)
        {
            if (DisplayText == "0" || _isOperationPerformed || DisplayText == "Ошибка")
                DisplayText = "";

            _isOperationPerformed = false;
            DisplayText += digit;
        }

        private void OnOpClick(string op)
        {
            if (DisplayText == "Ошибка") return;
            if (_firstValue != 0 && !_isOperationPerformed) Calculate();

            if (double.TryParse(DisplayText, out _firstValue))
            {
                _operation = op;
                _isOperationPerformed = true;
            }
        }

        private void Calculate()
        {
            if (string.IsNullOrEmpty(_operation) || !double.TryParse(DisplayText, out double secondValue)) return;

            try
            {
                double result = _model.Calculate(_firstValue, secondValue, _operation);
                DisplayText = result.ToString();
                _firstValue = result;
            }
            catch (DivideByZeroException ex)
            {
                DisplayText = "Ошибка";
                _dialogService.ShowWarning(ex.Message, "Ошибка");
            }

            _isOperationPerformed = true;
            _operation = "";
        }

        private void Clear()
        {
            DisplayText = "0";
            _firstValue = 0;
            _operation = "";
            _isOperationPerformed = false;
        }

        private void Backspace()
        {
            if (DisplayText == "Ошибка") { Clear(); return; }
            DisplayText = DisplayText.Length > 1 ? DisplayText.Substring(0, DisplayText.Length - 1) : "0";
        }

        private void AddComma()
        {
            if (_isOperationPerformed || DisplayText == "Ошибка") { DisplayText = "0,"; _isOperationPerformed = false; return; }
            if (!DisplayText.Contains(",")) DisplayText += ",";
        }

        private void ChangeSign()
        {
            if (DisplayText == "Ошибка" || DisplayText == "0") return;
            if (double.TryParse(DisplayText, out double val)) DisplayText = (-val).ToString();
        }
    }
}