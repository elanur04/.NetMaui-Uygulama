# .NET MAUI · Günlük Yaşam Uygulaması

C# ve XAML ile hazırlanmış, haberler, hava durumu, döviz kurları ve yapılacaklar ekranlarını bir araya getiren mobil/masaüstü öğrenme projesi.

## Teknik içerik

- .NET 8 MAUI; proje hedefleri Android ve Windows.
- Firebase oturum ve veri servisleri: `MauiApp3/Services/`.
- Ekranlar: `MauiApp3/Views/`; veri modelleri: `MauiApp3/Models/`.

## Projeyi inceleme ve çalıştırma

`MauiApp3.sln` dosyasını .NET MAUI iş yükü kurulu Visual Studio ile açın, NuGet paketlerini geri yükleyin ve Android emülatörü ya da Windows hedefini seçin. Dış servis özellikleri için kendi servis yapılandırmanız ve ağ erişiminiz gerekir.

Bu depo bir eğitim projesidir. Firebase erişim kuralları ve istemci yapılandırması yayına almadan önce gözden geçirilmelidir; mobil istemci içine sunucu sırları yerleştirilmemelidir. Otomatik test ve CI derleme doğrulaması henüz eklenmemiştir.
