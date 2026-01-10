using MauiApp3.Services;
namespace MauiApp3.Views;

    public partial class LoginPage : ContentPage
    {
        private readonly AuthService _authService;
        private bool _isLoginMode = true; // Baþlangýçta giriþ modu

        public LoginPage()
        {
            InitializeComponent();
            _authService = new AuthService();
        }

        // Mod Deðiþtirme (Giriþ <-> Kayýt)
        private void OnSwitchModeTapped(object sender, EventArgs e)
        {
            _isLoginMode = !_isLoginMode;

            if (_isLoginMode)
            {
                lblTitle.Text = "Oturum Aç";
                btnAction.Text = "Oturum Aç";
                lblSwitch.Text = "Hesabým Yok";
                txtUsername.IsVisible = false;
                lineUsername.IsVisible = false;
            }
            else
            {
                lblTitle.Text = "Kaydol";
                btnAction.Text = "Kaydol";
                lblSwitch.Text = "Zaten bir hesabým var";
                txtUsername.IsVisible = true;
                lineUsername.IsVisible = true;
            }
        }

        // Butona Týklama Ýþlemi
        private async void OnActionClicked(object sender, EventArgs e)
        {
          
            if (string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                await DisplayAlert("Hata", "Lütfen tüm alanlarý doldurunuz.", "Tamam");
                return;
            }

            if (!_isLoginMode && string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                await DisplayAlert("Hata", "Kullanýcý adý boþ býrakýlamaz.", "Tamam");
                return;
            }

            loadingIndicator.IsRunning = true;
            btnAction.IsEnabled = false;

            try
            {
                string displayName = "";

                if (_isLoginMode)
                {
                    // Giriþ Yap
                    displayName = await _authService.LoginAsync(txtEmail.Text, txtPassword.Text);
                }
                else
                {
                    // Kayýt Ol
                    displayName = await _authService.RegisterAsync(txtUsername.Text, txtEmail.Text, txtPassword.Text);
                    await DisplayAlert("Baþarýlý", "Kayýt oluþturuldu, giriþ yapýlýyor...", "Tamam");
                }

                // Baþarýlý giriþ sonrasý Ana Uygulamaya Yönlendirme
                Application.Current.MainPage = new AppShell();
            }
        catch (Exception ex)
        {
            string hataMesaji = "Beklenmedik bir hata oluþtu."; // Varsayýlan mesaj

            // Gelen hatanýn içindeki teknik metne bakarak Türkçe karþýlýk
            if (ex.Message.Contains("INVALID_LOGIN_CREDENTIALS") || ex.Message.Contains("INVALID_PASSWORD"))
            {
                hataMesaji = "E-posta veya þifre hatalý.";
            }
            else if (ex.Message.Contains("EMAIL_NOT_FOUND"))
            {
                hataMesaji = "Bu e-posta adresiyle kayýtlý kullanýcý bulunamadý.";
            }
            else if (ex.Message.Contains("EMAIL_EXISTS"))
            {
                hataMesaji = "Bu e-posta adresi zaten kullanýmda.";
            }
            else if (ex.Message.Contains("INVALID_EMAIL"))
            {
                hataMesaji = "Lütfen geçerli bir e-posta adresi giriniz.";
            }
            else if (ex.Message.Contains("WEAK_PASSWORD"))
            {
                hataMesaji = "Þifreniz çok zayýf (en az 6 karakter olmalý).";
            }
            else if (ex.Message.Contains("API key not valid"))
            {
                // Ekran görüntüsündeki hatanýn asýl sebebi bu:
                hataMesaji = "Sistem hatasý: API Anahtarý tanýmlanmamýþ.";
            }

            // Kullanýcýya sadece temiz mesajý göster
            await DisplayAlert("Ýþlem Baþarýsýz", hataMesaji, "Tamam");
        }
        finally
            {
                loadingIndicator.IsRunning = false;
                btnAction.IsEnabled = true;
            }
        }
    }
