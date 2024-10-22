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

        public CalculatorViewModel()
        {

        }
    }
}
