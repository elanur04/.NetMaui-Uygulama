using MauiApp3.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace MauiApp3.Views;

public partial class HaberlerPage : ContentPage
{
    // 1. DÜZELTME: HttpClient'ý sýnýf seviyesine çýkardýk (Static veya tek instance)
    private readonly HttpClient _client = new HttpClient();

    // 2. DÜZELTME: Verileri saklamak için bir "Önbellek (Cache)" listesi
    // String (Kategori Adý) -> Veri Listesi eþleþmesi tutar.
    private Dictionary<string, List<HaberItem>> _haberCache = new Dictionary<string, List<HaberItem>>();

    public HaberlerPage()
    {
        InitializeComponent();
        HaberleriGetir("manset");
    }

    private void KategoriSec(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button != null)
        {
            string kategori = button.CommandParameter.ToString();
            HaberleriGetir(kategori);
        }
    }

    private async void HaberleriGetir(string kategori)
    {
        try
        {
            Yukleniyor.IsRunning = true;
            Yukleniyor.IsVisible = true;
            CvHaberler.ItemsSource = null; // Listeyi geçici olarak boþalt

            // 3. DÜZELTME: Önce hafýzaya bak, daha önce çektik mi?
            if (_haberCache.ContainsKey(kategori))
            {
                // Varsa direkt hafýzadan getir (SÜRE: 0 Saniye)
                CvHaberler.ItemsSource = _haberCache[kategori];
                Yukleniyor.IsRunning = false;
                Yukleniyor.IsVisible = false;
                return; // Ýnternete gitme, iþlemi bitir.
            }

            // Yoksa Ýnternete Git
            string rssUrl = $"https://www.trthaber.com/{kategori}_articles.rss";
            string apiUrl = $"https://api.rss2json.com/v1/api.json?rss_url={rssUrl}";

            // _client nesnesini tekrar kullanýyoruz (Hýz kazandýrýr)
            var jsonString = await _client.GetStringAsync(apiUrl);
            var haberData = JsonConvert.DeserializeObject<HaberRoot>(jsonString);

            if (haberData != null && haberData.items != null)
            {
                // Veriyi ekrana bas
                CvHaberler.ItemsSource = haberData.items;

                // 4. EKLEME: Veriyi hafýzaya kaydet ki bir daha sormasýn
                _haberCache.Add(kategori, haberData.items);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Hata", "Haberler yüklenemedi. Ýnternetinizi kontrol edin.", "Tamam");
        }
        finally
        {
            Yukleniyor.IsRunning = false;
            Yukleniyor.IsVisible = false;
        }
    }

    private async void HaberSecildi(object sender, SelectionChangedEventArgs e)
    {
        var secilenHaber = e.CurrentSelection.FirstOrDefault() as HaberItem;
        if (secilenHaber != null)
        {
            await Navigation.PushAsync(new HaberDetay(secilenHaber));
        }
        ((CollectionView)sender).SelectedItem = null;
    }
}