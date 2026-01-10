namespace MauiApp3.Models
{
    // JSON'dan gelen ana kök yapı
    public class HaberRoot
    {
        public string status { get; set; }
        public Feed feed { get; set; }
        public List<HaberItem> items { get; set; } // Bizim işimiz burayla
    }

    public class Feed
    {
        public string url { get; set; }
        public string title { get; set; }
        public string link { get; set; }
        public string author { get; set; }
        public string description { get; set; }
        public string image { get; set; }
    }

    // Her bir haberin özellikleri
    public class HaberItem
    {
        public string title { get; set; }       // Başlık
        public string pubDate { get; set; }     // Tarih
        public string link { get; set; }        // Haberin orjinal linki (Paylaşım için)
        public string guid { get; set; }
        public string author { get; set; }
        public string thumbnail { get; set; }   // Küçük Resim
        public string description { get; set; } // Özet/İçerik
        public string content { get; set; }     // Detaylı içerik

    }
}
