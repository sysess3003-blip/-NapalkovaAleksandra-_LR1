using System;

namespace PinkWpfCalculator.Models
{
    public class CalculatorModel
    {
        public double Calculate(double first, double second, string operation)
        {
            switch (operation)
            {
                case "+":
                    return first + second;
                case "-":
                    return first - second;
                case "*":
                    return first * second;
                case "/":
                    if (second == 0)
                        throw new DivideByZeroException("На ноль делить нельзя! 🌸");
                    return first / second;
                case "%":
                    return first * second / 100;
                default:
                    return second;
            }
        }
    }
}