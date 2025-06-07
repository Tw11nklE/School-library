using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Biblioteka
{
    public partial class MainPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            _databaseService = new DatabaseService();
        }
        private async void Register_btn(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistrationPage());
        }
        private async void Vhod_btn(object sender, EventArgs e)
        {
            string login = user_login.Text;
            string password = user_password.Text;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Ошибка", "Пожалуйста, заполните все поля.", "OK");
                return;
            }

            var user = _databaseService.GetUserByLogin(login);

            if (user != null && user.Password == password)
            {
                if (user.Role)
                {
                    await Navigation.PushAsync(new AdminPage());
                }
                else
                    await Navigation.PushAsync(new BasePage());
            }
            else
            {
                await DisplayAlert("Ошибка", "Неверный логин или пароль.", "OK");
            }
        }
    }
}
