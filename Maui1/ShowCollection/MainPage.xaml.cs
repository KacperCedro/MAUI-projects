using System.Collections.ObjectModel;

namespace ShowCollection
{
    public partial class MainPage : ContentPage
    {

        public ObservableCollection<string> FruitsCollection { get; set; }
        public string SelectedFruit { get; set; }
        private string returnMessage;
        public string TextEntry { get; set; }
        public string ReturnMessage
        {
            get { return returnMessage; }
            set
            {
                returnMessage = value;
                OnPropertyChanged();
            }
        }

        public MainPage()
        {
            FruitsCollection = new ObservableCollection<string>();
            FruitsCollection.Add("jabłko");
            FruitsCollection.Add("banana");
            FruitsCollection.Add("mandarynka");
            SelectedFruit = FruitsCollection.First();
            InitializeComponent();

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            ReturnMessage = "wybrany owoc to: " + SelectedFruit;
        }

        private void newFruitButton_Clicked(object sender, EventArgs e)
        {
            FruitsCollection.Add(TextEntry);
        }

        private void removeFruitButton_Clicked(object sender, EventArgs e)
        {
            FruitsCollection.Remove(SelectedFruit);
        }
    }

}
