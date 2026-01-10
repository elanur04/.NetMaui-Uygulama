
using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Database;
using Firebase.Database.Query;


using MauiApp3.Model;

namespace MauiApp3.Services
{
    internal static class FirebaseServices
    {
        static string projectId = "odev-3-8cee9";
        static string apiKey = "AIzaSyDg5X75a9ixmyy6Bi8dBQuOqifVDC6It5M";
        static string authDomain = "odev-3-8cee9.firebaseapp.com";
        static string storeBucked = "odev-3-8cee9.firebasestorage.app";


        static FirebaseAuthConfig config = new FirebaseAuthConfig()
        {
            ApiKey = apiKey,
            AuthDomain = authDomain,
           Providers = new FirebaseAuthProvider[] {  new EmailProvider()  }
        };
        public static Task<bool> Register(string username, string email, string password, ref string message)
        {
            message = "";
            try
            {

                var client = new FirebaseAuthClient(config);
                var res = client.CreateUserWithEmailAndPasswordAsync(email, password, username);

                return Task.FromResult(res.Result.User != null ? true : false);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Task.FromResult(false);
        }

        public static Task<bool> Login(string email, string password, ref string message)
        {
            message = "";
            try
            {
                var client = new FirebaseAuthClient(config);
                var res = client.SignInWithEmailAndPasswordAsync(email, password);

                return Task.FromResult(res.Result?.User != null ? true : false);
            }
            catch(Exception ex)
            {
                message = ex.Message;
            }
            return Task.FromResult(false);
        }

        const string ConnectionString = "https://odev-3-8cee9-default-rtdb.firebaseio.com";
        static FirebaseClient firebaseClient = new FirebaseClient(ConnectionString);

        // 1. LİSTEYİ GETİRME METODU (LİSTE SAYFASI İÇİN GEREKLİ)
        public static async Task<List<ToDoItem>> GetTodos()
        {
            try
            {
                var todos = await firebaseClient
                    .Child("todos")
                    .OnceAsync<ToDoItem>();

                return todos.Select(x => x.Object).ToList();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Hata: {ex.Message}");
                return new List<ToDoItem>();
            }
        }

        // 2. EKLEME METODU 
        public static async Task<bool> AddNewTodo(ToDoItem item)
        {
            try
            {
                await firebaseClient
                    .Child("todos")
                    .Child(item.Id)
                    .PutAsync(item); 
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Hata: {ex.Message}");
                return false;
            }
        }

        // 3. SİLME METODU 
        public static async Task<bool> DeleteTodo(string id)
        {
            try
            {
                await firebaseClient
                    .Child("todos")
                    .Child(id)
                    .DeleteAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}