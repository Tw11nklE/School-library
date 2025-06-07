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
    public partial class BasePage : FlyoutPage
    {
        public BasePage()
        {
            InitializeComponent();
            flyout.listview.ItemSelected += OnSelectedItem;
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void OnSelectedItem(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as FlyoutItemPage;
            if (item != null)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetPage));
                flyout.listview.SelectedItem = null;
                IsPresented = false;
            }
        }
    }
}