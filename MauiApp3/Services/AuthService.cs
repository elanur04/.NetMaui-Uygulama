using Firebase.Auth;
using Firebase.Auth.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp3.Services
{
    public class AuthService
    {
        private const string ApiKey = "AIzaSyDg5X75a9ixmyy6Bi8dBQuOqifVDC6It5M";

        private readonly FirebaseAuthClient _authClient;

        public AuthService()
        {
            var config = new FirebaseAuthConfig
            {
                ApiKey = ApiKey,
                AuthDomain = "odev-3-8cee9.firebaseapp.com", 
                Providers = new FirebaseAuthProvider[]
                {
                    new EmailProvider()
                }
            };
            _authClient = new FirebaseAuthClient(config);
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            try
            {
                var userCredential = await _authClient.SignInWithEmailAndPasswordAsync(email, password);
                return userCredential.User.Info.DisplayName; // Kullanıcı adını döndürelim
            }
            catch (Exception ex)
            {
              
                throw new Exception("Giriş başarısız: " + ex.Message);
            }
        }

        public async Task<string> RegisterAsync(string username, string email, string password)
        {
            try
            {
                var userCredential = await _authClient.CreateUserWithEmailAndPasswordAsync(email, password, username);
                return userCredential.User.Info.DisplayName;
            }
            catch (Exception ex)
            {
                throw new Exception("Kayıt başarısız: " + ex.Message);
            }
        }
    }
}