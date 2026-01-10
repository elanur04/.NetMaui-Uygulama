namespace MauiApp3.Views;

public partial class AyarlarPage : ContentPage
{
    public AyarlarPage()
    {
        InitializeComponent();

        // Sayfa açýldýðýnda mevcut temayý kontrol et
        // Eðer þu anki tema "Dark" ise, Switch açýk gelsin.
        if (Application.Current.UserAppTheme == AppTheme.Dark)
        {
            SwTema.IsToggled = true;
        }
        else
        {
            SwTema.IsToggled = false;
        }
    }

    // Switch açýlýp kapandýðýnda çalýþýr
    private void OnTemaDegisti(object sender, ToggledEventArgs e)
    {
        if (e.Value) // Switch Açýldýysa -> Koyu Tema
        {
            Application.Current.UserAppTheme = AppTheme.Dark;
        }
        else // Switch Kapandýysa -> Açýk Tema
        {
            Application.Current.UserAppTheme = AppTheme.Light;
        }
    }
}