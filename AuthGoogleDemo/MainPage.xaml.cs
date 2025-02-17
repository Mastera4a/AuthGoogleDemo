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
            try
            {
                var loggedUser = await _googleAuthService.GetCurrentUserAsync();
                Console.WriteLine($"[DEBUG] GetCurrentUserAsync: {loggedUser?.FullName}");

                if (loggedUser == null)
                {
                    loggedUser = await _googleAuthService.AuthenticateAsync();
                    Console.WriteLine($"[DEBUG] AuthenticateAsync: {loggedUser?.FullName}");
                }

                if (loggedUser == null)
                {
                    Console.WriteLine("[ERROR] loggedUser всё ещё null");
                    return;
                }

                await DisplayAlert("Login Message", "Welcome " + loggedUser.FullName, "Ok");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] {ex.Message}");
                await DisplayAlert("Error", ex.Message, "Ok");
            }
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
