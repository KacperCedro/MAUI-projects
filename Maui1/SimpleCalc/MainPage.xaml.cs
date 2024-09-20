namespace SimpleCalc
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            string strFirstNumber = firstNumberEntry.Text;
            int firstNumber;
            if (!int.TryParse(strFirstNumber, out firstNumber))
            {
                return;
                result.Text = "pal gume";
            }
            if (!int.TryParse(secondNumberEntry.Text, out int secodnNumber))
            {
                return;
                result.Text = "pal gume";
            }
            int resultNumber = 0;
            if (addition.IsChecked)
            {
                resultNumber = firstNumber + secodnNumber;
            }
            if (sub.IsChecked)
            {
                resultNumber = firstNumber - secodnNumber;
            }
            if (multi.IsChecked)
            {
                resultNumber = firstNumber * secodnNumber;
            }
            if (div.IsChecked)
            {
                resultNumber = firstNumber / secodnNumber;
            }
            result.Text =  resultNumber.ToString();           
        }
    }

}
