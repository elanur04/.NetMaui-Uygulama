using MauiApp3.Models;

namespace MauiApp3.Views;

public partial class HaberDetay : ContentPage
{
    private HaberItem _secilenHaber;

    public HaberDetay(HaberItem haber)
    {
        InitializeComponent();

        _secilenHaber = haber;

        // WebView'e linki veriyoruz
        if (_secilenHaber != null && !string.IsNullOrEmpty(_secilenHaber.link))
        {
            WebHaber.Source = _secilenHaber.link;
        }
    }

    private async void OnShareClicked(object sender, EventArgs e)
    {
        if (_secilenHaber == null || string.IsNullOrEmpty(_secilenHaber.link))
        {
            await DisplayAlert("Uyarý", "Paylaþýlacak bir link bulunamadý.", "Tamam");
            return;
        }

        try
        {
            await Share.Default.RequestAsync(new ShareTextRequest
            {
                Uri = _secilenHaber.link,
                Title = _secilenHaber.title
            });
        }
        catch (Exception ex)
        {
            await DisplayAlert("Hata", "Paylaþým yapýlamadý: " + ex.Message, "Tamam");
        }
    }
}