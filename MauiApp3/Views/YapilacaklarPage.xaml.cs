using MauiApp3.Model;
using MauiApp3.Services;

namespace MauiApp3.Views
{
    public partial class YapilacaklarPage : ContentPage
    {
        public YapilacaklarPage()
        {
            InitializeComponent();
        }

        // Sayfa her ekrana geldiðinde listeyi yenile
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadData();
        }

        private async Task LoadData()
        {
            var items = await FirebaseServices.GetTodos();
            TodoList.ItemsSource = items;
        }

        // + Butonuna basýnca Ekleme Sayfasýna git
        private async void OnAddClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddToDoPage());
        }

        // Düzenle Butonu
        private async void OnEditClicked(object sender, EventArgs e)
        {
            var btn = sender as ImageButton;
            var item = btn.CommandParameter as ToDoItem;
            if (item != null)
            {
                await Navigation.PushAsync(new AddToDoPage(item));
            }
        }

        // Sil Butonu 
        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            var btn = sender as ImageButton;
            var item = btn.CommandParameter as ToDoItem;

            bool cevap = await DisplayAlert("Silinsin mi?", "Silmeyi onayla", "OK", "CANCEL");
            if (cevap && item != null)
            {
                await FirebaseServices.DeleteTodo(item.Id);
                await LoadData(); // Listeyi yenile
            }
        }
    }
}