using System.Text.Json.Serialization;
namespace MauiApp3.Models
{
    internal class SehirHavaDurumu
    {
        public string Ad { get; set; } // Ekranda görünen isim (Örn: ÇANKIRI)

        // Bu özellik JSON'a kaydedilmesin, sadece okuma amaçlı
        [JsonIgnore]
        public string ResimUrl => GetHavaDurumuResmi(Ad);

        private string GetHavaDurumuResmi(string sehirAdi)
        {
            if (string.IsNullOrEmpty(sehirAdi)) return "";

           
            string temizAd = sehirAdi.ToUpper(new System.Globalization.CultureInfo("tr-TR"))
         .Replace('Ç', 'C')
         .Replace('Ğ', 'G')
         .Replace('İ', 'I')
         .Replace('I', 'I') // Iğdır gibi iller için
         .Replace('Ö', 'O')
         .Replace('Ş', 'S')
         .Replace('Ü', 'U')
         .Replace(" ", ""); // Boşlukları sil (NEW YORK -> NEWYORK)


            return $"https://www.mgm.gov.tr/sunum/tahmin-klasik-5070.aspx?m={temizAd}&basla=1&bitir=5&rC=000000&rZ=ffffff";
        }
    }
}
