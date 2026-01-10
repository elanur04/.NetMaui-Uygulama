using MauiApp3.Models;
using System.Collections.ObjectModel;
using Newtonsoft.Json.Linq;


namespace MauiApp3.Views;

public partial class KurlarPage : ContentPage
{
    // Listeyi canlý tutmak için ObservableCollection kullanýyoruz
    public ObservableCollection<Doviz> KurlarListesi { get; set; } = new ObservableCollection<Doviz>();
    public KurlarPage()
    {
        InitializeComponent();
        // Tasarýmdaki liste kaynaðýný buradaki listeye baðlýyoruz
        DovizListesi.ItemsSource = KurlarListesi;
        VerileriGetir();
    }
    private async void OnYenileClicked(object sender, EventArgs e)
    {
        await VerileriGetir();
    }

    private async Task VerileriGetir()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;
            Yukleniyor.IsRunning = true;
            Yukleniyor.IsVisible = true;

            // URL'nin doðru olduðundan emin olun (Https önemlidir)
            string url = "https://finans.truncgil.com/today.json";

            using (HttpClient client = new HttpClient())
            {
                // Sunucunun bizi tarayýcý sanmasý için User-Agent ekliyoruz !!!
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");

                var jsonString = await client.GetStringAsync(url);

                // Veri gelip gelmediðini kontrol etmek için debug çýktýsý (Output penceresinde görünür)
                System.Diagnostics.Debug.WriteLine("GELEN VERÝ: " + jsonString);

                var jsonNesnesi = JObject.Parse(jsonString);

                // Veriler iþlenmeden önce listeyi temizleyelim
                KurlarListesi.Clear();

                foreach (var item in jsonNesnesi)
                {
                    if (item.Key == "Update_Date") continue;

                    try
                    {
                        var detay = item.Value;

                        // Null kontrolü yaparak veri çekiyoruz
                        var yeniDoviz = new Doviz
                        {
                            Isim = item.Key.Replace("_", " "),
                            // API bazen "Alýþ" bazen "Alis" döndürebilir, her ihtimale karþý kontrol edelim:
                            Alis = (detay["Alýþ"] ?? detay["Alis"])?.ToString(),
                            Satis = (detay["Satýþ"] ?? detay["Satis"])?.ToString(),
                            Degisim = (detay["Deðiþim"] ?? detay["Degisim"])?.ToString(),
                            Tur = (detay["Tür"] ?? detay["Tur"])?.ToString()
                        };

                        KurlarListesi.Add(yeniDoviz);
                    }
                    catch (Exception icHata)
                    {
                        // Döngü içindeki hatayý görmek için
                        System.Diagnostics.Debug.WriteLine($"Satýr hatasý: {icHata.Message}");
                        continue;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Genel hatayý ekrana basalým
            await DisplayAlert("Hata", "Veri çekilemedi: " + ex.Message, "Tamam");
        }
        finally
        {
            IsBusy = false;
            Yukleniyor.IsRunning = false;
            Yukleniyor.IsVisible = false;
        }
    }
}
