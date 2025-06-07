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
    public partial class LinksPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        public LinksPage()
        {
            InitializeComponent();
            _databaseService = new DatabaseService();
            LoadLinkItems();
        }
        private void LoadLinkItems()
        {
            LinkList.ItemsSource = _databaseService.GetLinkItems();
        }
    }
}