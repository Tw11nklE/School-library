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
    public partial class InformPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        public InformPage()
        {
            InitializeComponent();
            _databaseService = new DatabaseService();
            LoadEventItems();
        }
        private void LoadEventItems()
        {
            EventList.ItemsSource = _databaseService.GetEventItems();
        }
    }
}