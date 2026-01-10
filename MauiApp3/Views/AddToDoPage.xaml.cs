using MauiApp3.Model;
using MauiApp3.Services;

namespace MauiApp3.Views
{
    public partial class AddToDoPage : ContentPage
    {
        ToDoItem Todo;

        // 1. AppShell Menüsü için gerekli olan BOÞ Yapýcý Metot
        public AddToDoPage()
        {
            InitializeComponent();
            // Menüden týklandýðýnda yeni, boþ bir görev oluþturuyoruz
            Todo = new ToDoItem();
            this.BindingContext = Todo;
        }

        // 2. Düzenleme iþlemi için parametre alan Yapýcý Metot 
        public AddToDoPage(ToDoItem item)
        {
            InitializeComponent();
            Todo = item;
            this.BindingContext = item;
        }

      private async void AddNewTodo(object sender, EventArgs e)
{
    // --- 1. VALÝDASYON (KONTROL) KISMI BAÞLANGICI ---

    // Baþlýk kontrolü: Eðer boþsa veya sadece boþluktan oluþuyorsa
    if (string.IsNullOrWhiteSpace(Todo.Title))
    {
        await DisplayAlert("Uyarý", "Lütfen görev baþlýðýný giriniz.", "Tamam");
        return; // Kodun devam etmesini engeller, fonksiyondan çýkar.
    }

    // Detay kontrolü: Eðer boþsa
    if (string.IsNullOrWhiteSpace(Todo.Description))
    {
        await DisplayAlert("Uyarý", "Lütfen görev detayýný giriniz.", "Tamam");
        return; // Kodun devam etmesini engeller.
    }

    // --- VALÝDASYON KISMI BÝTÝÞÝ ---


    // Eðer yukarýdaki kontrollere takýlmazsa kayýt iþlemi baþlar
    bool isSuccess = await FirebaseServices.AddNewTodo(Todo);

    if (isSuccess)
    {
        await DisplayAlert("Baþarýlý", "Kayýt Baþarýlý", "Tamam");
        
        // Ýþlem bitince listeye geri dön
        await Navigation.PopAsync(); 
    }
    else
    {
        await DisplayAlert("Hata", "Kayýt Baþarýsýz", "Tamam");
    }
}
    }
}