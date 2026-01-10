namespace MauiApp3
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            // İlk açılışta Login sayfasını göster
            MainPage = new Views.LoginPage();

        }
    }
}
