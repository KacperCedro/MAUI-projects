namespace NavigationTabMauiApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            TabbedPage tabbedPage = new TabbedPage();
            tabbedPage.Children.Add(new MainPage());
            tabbedPage.Children.Add(new AnotherPage());

            return new Window(tabbedPage);
        }
    }
}