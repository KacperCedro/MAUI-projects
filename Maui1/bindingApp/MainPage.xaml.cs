namespace bindingApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        public string Message { get; set; }
        private string returnMessage;

        public string ReturnMessage
        {
            get { return returnMessage; }
            set
            {
                returnMessage = value;
                OnPropertyChanged();
            }
        }

        private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            LabelRotation.Rotation = SliderRotation.Value;
            LabelRotation.RotationX = SliderRotation.Value;
            LabelRotation.RotationY = SliderRotation.Value;

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            ReturnMessage = "wartosc " + Message;
            //OnPropertyChanged(nameof(ReturnMessage));

        }
    }

}
