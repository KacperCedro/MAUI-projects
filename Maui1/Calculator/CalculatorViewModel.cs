using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    internal class CalculatorViewModel : BindableObject
    {
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

        private Command numericButtonCommand;

        public Command NumericButtonCommand
        {
            get
            {
                if (numericButtonCommand == null)
                {
                    numericButtonCommand = new Command<string>((string number) =>
                    {
                        Result += number;
                    });
                }
                return numericButtonCommand;
            }
            set { numericButtonCommand = value; }
        }
        private Command operationCommand;
        public Command OperationCommand
        {
            get
            {
                if (operationCommand == null)
                    operationCommand = new Command<string>((string operatorSign) =>
                    {
                        if (ifOperationExecute)
                            return;

                        int firstNumber = prevNumber;
                        int secondNumber = int.Parse(result);
                        Result = GetOperatorResult(prevOperatorSign, firstNumber, secondNumber).ToString();
                        prevOperatorSign = operatorSign;
                        prevNumber = int.Parse(result);
                        ifOperationExecute = true;
                    });
                return operationCommand;
            }
            set { operationCommand = value; }
        }

        private string prevOperatorSign = "+";
        private int prevNumber = 0;
        private bool ifOperationExecute = false;
        int GetOperatorResult(string operatorSign, int firstNumber, int secondNumber)
        {
            int result = operatorSign switch
            {
                "+" => firstNumber + secondNumber,
                "-" => firstNumber - secondNumber,
                "*" => firstNumber * secondNumber,
                "/" => firstNumber / secondNumber,
                _ => 0,
            };
            return result;
        }

        /*
         Command="{Binding NumberCommand}"
         CommandParameter="{Binding Text,Source={x:RelativeSource Self}}"
        **/
        public CalculatorViewModel()
        {

        }
    }
}
