using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Biblioteka
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Books : ContentPage
    {
        private readonly DatabaseService _databaseService;
        public Books()
        {
            InitializeComponent();
            _databaseService = new DatabaseService();
            LoadBookItems();
        }
        private void LoadBookItems()
        {
            BooksList.ItemsSource = _databaseService.GetBookItems();
        }
    }
}