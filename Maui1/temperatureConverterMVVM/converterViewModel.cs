using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace temperatureConverterMVVM
{
    public class converterViewModel : BindableObject
    {
        public List<string> Units { get; set; }
        public string SelectedUnit { get; set; }
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
        public string Number { get; set; }
        public Command ButtonClicked { get; set; }

        public converterViewModel()
        {
            ButtonClicked = new Command(Convert);
            Units = new List<string>();
            Units.Add("F to C");
            Units.Add("C to F");

        }
        public void Convert()
        {
            if (float.TryParse(Number, out float tmpNumber))
            {
                switch (SelectedUnit)
                {
                    case "F to C":
                        Result = $"{(((tmpNumber - 32) * 5) / 9)}";
                        break;
                    case "C to F":
                        Result = $"{((tmpNumber * 9 / 5) + 32)}";
                        break;
                    default:
                        break;
                }
            }
            else
                Result = "Podaj liczbe";

        }
    }

}
