// Views/HavaDurumuPage.xaml.cs
using MauiApp3.Models;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace MauiApp3.Views;

public partial class HavaDurumuPage : ContentPage
{
    // Listeyi ObservableCollection yapýyoruz ki ekleyince hemen ekranda gözüksün
    ObservableCollection<SehirHavaDurumu> Sehirler = new ObservableCollection<SehirHavaDurumu>();

    public HavaDurumuPage()
    {
        InitializeComponent();
        CvSehirler.ItemsSource = Sehirler;
        SehirleriYukle(); // Uygulama açýlýnca kayýtlýlarý getir
    }

    // Ekle Butonu
    private async void OnEkleClicked(object sender, EventArgs e)
    {
        string girilenSehir = TxtSehir.Text?.Trim();

        if (!string.IsNullOrEmpty(girilenSehir))
        {
            // Yeni þehir oluþtur
            var yeniSehir = new SehirHavaDurumu { Ad = girilenSehir.ToUpper() };

            Sehirler.Add(yeniSehir); // Listeye ekle
            Kaydet(); // JSON'a kaydet

            TxtSehir.Text = ""; // Kutuyu temizle
        }
        else
        {
            await DisplayAlert("Uyarý", "Lütfen bir þehir adý girin.", "Tamam");
        }
    }

    // Sil Butonu
    private async void OnSilClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var silinecekSehir = button.CommandParameter as SehirHavaDurumu;

        if (silinecekSehir != null)
        {
            bool cevap = await DisplayAlert("Silinsin mi?", $"{silinecekSehir.Ad} þehri silinsin mi?", "Evet", "Hayýr");
            if (cevap)
            {
                Sehirler.Remove(silinecekSehir); // Listeden sil
                Kaydet(); // Son halini kaydet
            }
        }
    }

    // Yenile Butonu (Resimleri güncellemek için)
    private void OnYenileClicked(object sender, EventArgs e)
    {
        // Listeyi yenileyerek resimlerin tekrar yüklenmesini saðlar
        CvSehirler.ItemsSource = null;
        CvSehirler.ItemsSource = Sehirler;
    }

    // Ödev Kuralý: JSON formatýnda kaydet [Kaynak: 250]
    private void Kaydet()
    {
        try
        {
            string dosyaYolu = Path.Combine(FileSystem.AppDataDirectory, "sehirler.json");
            string jsonVeri = JsonSerializer.Serialize(Sehirler);
            File.WriteAllText(dosyaYolu, jsonVeri);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Hata: " + ex.Message);
        }
    }

    private void SehirleriYukle()
    {
        try
        {
            string dosyaYolu = Path.Combine(FileSystem.AppDataDirectory, "sehirler.json");
            if (File.Exists(dosyaYolu))
            {
                string jsonVeri = File.ReadAllText(dosyaYolu);
                var kayitliListe = JsonSerializer.Deserialize<ObservableCollection<SehirHavaDurumu>>(jsonVeri);

                if (kayitliListe != null)
                {
                    foreach (var sehir in kayitliListe)
                    {
                        Sehirler.Add(sehir);
                    }
                }
            }
        }
        catch (Exception ex)
        {
           
        }
    }
}