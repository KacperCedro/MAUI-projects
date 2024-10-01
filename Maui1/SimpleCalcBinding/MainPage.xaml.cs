namespace SimpleCalcBinding
{
    public partial class MainPage : ContentPage
    {
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
        public MainPage()
        {
            InitializeComponent();
        }
        private void Button_Clicked(object sender, EventArgs e)
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
