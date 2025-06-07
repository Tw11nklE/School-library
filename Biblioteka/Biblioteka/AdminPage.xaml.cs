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
    public partial class AdminPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        public AdminPage()
        {
            InitializeComponent();
            _databaseService = new DatabaseService();
            LoadBookItems();
            LoadEventItems();
            LoadLinkItems();
        }
        private void LoadBookItems()
        {
            BooksListView.ItemsSource = _databaseService.GetBookItems();
        }
        private void LoadEventItems()
        {
            EventListView.ItemsSource = _databaseService.GetEventItems();
        }
        private void LoadLinkItems()
        {
            LinkListView.ItemsSource = _databaseService.GetLinkItems();
        }
        private void Posmotret(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BasePage());
        }
        private void AddBookItem_Clicked(object sender, EventArgs e) // Добавление книги
        {
            string imageUrl = BookImageUrl.Text;
            string title = BookTitle.Text;
            string author = BookAuthor.Text;
            string description = BookDescription.Text;

            if (string.IsNullOrEmpty(imageUrl) || string.IsNullOrEmpty(title) || string.IsNullOrEmpty(author))
            {
                DisplayAlert("Ошибка", "Пожалуйста, заполните все поля.", "OK");
                return;
            }
            var bookItem = new BookItem
            {
                ImageUrl = imageUrl,
                Title = title,
                Author = author,
                Description = description
            };
            _databaseService.AddBookItem(bookItem);

            BookImageUrl.Text = string.Empty;
            BookTitle.Text = string.Empty;
            BookAuthor.Text = string.Empty;
            BookDescription.Text = string.Empty;
            BooksListView.ItemsSource = _databaseService.GetBookItems();

            DisplayAlert("Успех", "Книга успешно добавлена.", "OK");
        }
        private async void BooksListView_ItemTapped(object sender, ItemTappedEventArgs e) // Удаление книги
        {
            if (e.Item == null) return; 
            var selectedBook = e.Item as BookItem;

            bool confirmDelete = await DisplayAlert(
                "Удаление книги",
                $"Вы уверены, что хотите удалить книгу '{selectedBook.Title}'?",
                "Да",
                "Отмена"
            );

            if (confirmDelete)
            {
                _databaseService.DeleteBookItem(selectedBook.Id);
                BooksListView.ItemsSource = _databaseService.GetBookItems();
                await DisplayAlert("Удаление", "Книга успешно удалена", "OK");
            }
            ((ListView)sender).SelectedItem = null;
        }
        private void AddEventItem_Clicked(object sender, EventArgs e)
        {
            // Проверяем, что поля заполнены
            if (!string.IsNullOrEmpty(EventTitle.Text) && !string.IsNullOrEmpty(EventDescription.Text))
            {
                // Создаем экземпляр EventItem
                var eventItem = new EventItem
                {
                    Title = EventTitle.Text,
                    Date = EventDate.Text,
                    Description = EventDescription.Text
                };

                // Сохраняем мероприятие в базе данных
                var dbService = new DatabaseService();
                dbService.AddEventItem(eventItem);

                // Очищаем поля ввода
                EventTitle.Text = string.Empty;
                EventDate.Text = string.Empty;
                EventDescription.Text = string.Empty;
                EventListView.ItemsSource = _databaseService.GetEventItems();

                // Показываем сообщение об успехе
                DisplayAlert("Успех", "Мероприятие добавлено", "OK");
            }
            else
            {
                DisplayAlert("Ошибка", "Пожалуйста, заполните все поля", "OK");
            }
        }
        private async void EventListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null) return;
            var selectedEvent = e.Item as EventItem;

            bool confirmDelete = await DisplayAlert(
                "Удаление книги",
                $"Вы уверены, что хотите удалить мероприятие '{selectedEvent.Title}'?",
                "Да",
                "Отмена"
            );

            if (confirmDelete)
            {
                _databaseService.DeleteEventItem(selectedEvent.Id);
                EventListView.ItemsSource = _databaseService.GetEventItems();
                await DisplayAlert("Удаление", "Мероприятие успешно удалено", "OK");
            }
            ((ListView)sender).SelectedItem = null;
        }

        private async void LinkListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null) return;

            var selectedLink = e.Item as LinkItem;

            bool confirmDelete = await DisplayAlert(
                "Удаление ссылки",
                $"Вы уверены, что хотите удалить ссылку '{selectedLink.Description}'?",
                "Да",
                "Отмена"
            );

            if (confirmDelete)
            {
                _databaseService.DeleteLinkItem(selectedLink.Id);
                LinkListView.ItemsSource = _databaseService.GetLinkItems();
                await DisplayAlert("Удаление", "Ссылка успешно удалена", "OK");
            }

    ((ListView)sender).SelectedItem = null;
        }

        private void AddLinkItem_Clicked(object sender, EventArgs e)
        {
            string description = LinkDescription.Text;
            string link = LinkLink.Text;

            if (string.IsNullOrEmpty(description) || string.IsNullOrEmpty(link))
            {
                DisplayAlert("Ошибка", "Пожалуйста, заполните все поля.", "OK");
                return;
            }

            var linkItem = new LinkItem
            {
                Description = description,
                Link = link
            };

            _databaseService.AddLinkItem(linkItem);

            // Очищаем поля ввода
            LinkDescription.Text = string.Empty;
            LinkLink.Text = string.Empty;

            // Обновляем список ссылок
            LinkListView.ItemsSource = _databaseService.GetLinkItems();

            DisplayAlert("Успех", "Ссылка успешно добавлена.", "OK");
        }
    }
}