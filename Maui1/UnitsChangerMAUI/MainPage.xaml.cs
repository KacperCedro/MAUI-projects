using System.Globalization;

namespace UnitsChangerMAUI
{
    public partial class MainPage : ContentPage
    {
        public List<string> UnitsList { get; set; }
        public string FirstSelectedUnit { get; set; }
        public string SecondSelectedUnit { get; set; }
        public string Number { get; set; }
        private string resultMessage;
        public Dictionary<string, double> UnitsDictionary { get; set; }

        public string ResultMessage
        {
            get { return resultMessage; }
            set
            {
                resultMessage = value;
                OnPropertyChanged();
            }
        }

        public MainPage()
        {
            UnitsList = new List<string>();
            UnitsList.Add("mm");
            UnitsList.Add("cm");
            UnitsList.Add("dm");
            UnitsList.Add("m");
            UnitsList.Add("hm");
            UnitsList.Add("km");
            UnitsList.Add("Mm");


            UnitsDictionary = new Dictionary<string, double>();
            UnitsDictionary.Add("mm", 0.001d);
            UnitsDictionary.Add("cm", 0.01d);
            UnitsDictionary.Add("dm", 0.1d);
            UnitsDictionary.Add("m", 1d);
            UnitsDictionary.Add("hm", 100d);
            UnitsDictionary.Add("km", 1000d);

            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            ResultMessage = $"Wartość zmieniona z {FirstSelectedUnit} na {SecondSelectedUnit} to {double.Parse(Number) * (UnitsDictionary[FirstSelectedUnit] / UnitsDictionary[SecondSelectedUnit])} {SecondSelectedUnit} ";
        } 

    }
}
