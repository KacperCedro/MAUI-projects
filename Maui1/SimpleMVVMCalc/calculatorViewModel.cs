using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMVVMCalc
{
    public class calculatorViewModel : BindableObject
    {
        private Command calculateCommand;

        public Command CalculateCommand
        {
            get { return calculateCommand; }
            set
            {
                calculateCommand = value;
            }
        }

        public string FirstNumber { get; set; }
        public string SecondNumber { get; set; }

        private string result;

        public string Result
        {
            get { return result; }
            set
            {
                result = value;
                OnPropertyChanged();
            }
        }
        public bool IsAddition { get; set; }
        public bool IsSubstraction { get; set; }
        public bool IsMultiplication { get; set; }
        public bool IsDivision { get; set; }
        public calculatorViewModel()
        {
            CalculateCommand = new Command(Calculate);
        }
        private void Calculate()
        {

            int firstNumber;
            if (!int.TryParse(FirstNumber, out firstNumber))
            {
                //komunikat o błędzie
                Result = "Niepoprawna pierwsza liczba";
                //resultLabel.TextColor = Colors.Red;
                return;
            }

            if (!int.TryParse(SecondNumber, out int secondNumber))
            {
                //komunikat o błędzie
                Result = "Niepoprawna druga liczba";
                //resultLabel.TextColor = Colors.Red;
                return;
            }

            int result1 = 0;
            if (IsAddition)
            {
                result1 = firstNumber + secondNumber;
            }
            else if (IsSubstraction)
            {
                result1 = firstNumber - secondNumber;
            }
            else if (IsMultiplication)
            {
                result1 = firstNumber * secondNumber;
            }
            else if (IsDivision)
            {
                result1 = firstNumber / secondNumber;
            }
            Result = $"Wynik operacji {result1}";
            //resultLabel.TextColor = Colors.Pink;
        }
    }
}
