using AuthGoogleDemo.GoogleAuth;

namespace AuthGoogleDemo
{
    public partial class MainPage : ContentPage
    {
        private readonly IGoogleAuthService _googleAuthService = new GoogleAuthService();

        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void LoginBtn_Clicked(object sender, EventArgs e)
        {
            var loggedUser = await _googleAuthService.GetCurrentUserAsync();

            if (loggedUser == null)
            {
                loggedUser = await _googleAuthService.AuthenticateAsync();
            }

            // Проверяем null и приводим к строке
            string userName = loggedUser.FullName?.ToString() ?? "User";

            await Application.Current.MainPage.DisplayAlert("Login Message", "Welcome " + userName, "Ok");
        }


        private async void logoutBtn_Clicked(object sender, EventArgs e)
        {
            await _googleAuthService.LogoutAsync();

            await Application.Current.MainPage.DisplayAlert("Login Message", "Goodbay", "Ok");
        }

        //private async void OnCounterClicked(object sender, EventArgs e)
        //{
        //    count++;

        //    if (count == 1) CounterBtn.Text = $"Clicked {count} time";
        //    else CounterBtn.Text = $"Clicked {count} times";

        //    SemanticScreenReader.Announce(CounterBtn.Text);
        //}
    }
}
