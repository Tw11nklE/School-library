using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteka;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Biblioteka
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        public RegistrationPage()
        {
            InitializeComponent();
            _databaseService = new DatabaseService();
        }
        private void Reg_user_btn(object sender, EventArgs e)
        {
            string login = reg_user_login.Text;
            string password = reg_user_password.Text;
            string adminPassword = user_role.Text;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                DisplayAlert("Ошибка", "Пожалуйста, заполните все поля.", "OK");
                return;
            }

            bool role = false;
            if (!string.IsNullOrEmpty(adminPassword))
            {
                bool isAdmin = adminPassword == "root"; 
                role = isAdmin; 
            }

            var newUser = new User
            {
                Login = login,
                Password = password,
                Role = role
            };

            _databaseService.AddUser(newUser);
            DisplayAlert("Поздравляю", "Вы успешно зарегистрированы!", "OK");
            Navigation.PopAsync();
        }
    }
}