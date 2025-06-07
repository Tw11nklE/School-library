using System;
using System.Collections.Generic;
using MySqlConnector;


namespace Biblioteka
{
    public class DatabaseService
    {
        private readonly string connectionString = "Server=10.0.2.2;Port=3306;Database=lib;Uid=root;Pwd=root;";

        public void AddUser(User user)
        {
            using var connection = new MySqlConnection(connectionString);
            connection.Open();
            string query = "INSERT INTO users (login, password, role) VALUES (@login, @password, @role)";
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@login", user.Login);
            cmd.Parameters.AddWithValue("@password", user.Password);
            cmd.Parameters.AddWithValue("@role", user.Role);
            cmd.ExecuteNonQuery();
        }

        public List<User> GetUsers()
        {
            List<User> users = new List<User>();
            using var connection = new MySqlConnection(connectionString);
            connection.Open();
            string query = "SELECT * FROM users";
            using var cmd = new MySqlCommand(query, connection);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                users.Add(new User
                {
                    Id = reader.GetInt32("id"),
                    Login = reader.GetString("login"),
                    Password = reader.GetString("password"),
                    Role = reader.GetBoolean("role")
                });
            }
            return users;
        }

        public User GetUserByLogin(string login)
        {
            using var connection = new MySqlConnection(connectionString);
            connection.Open();
            string query = "SELECT * FROM users WHERE login = @login LIMIT 1";
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@login", login);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new User
                {
                    Id = reader.GetInt32("id"),
                    Login = reader.GetString("login"),
                    Password = reader.GetString("password"),
                    Role = reader.GetBoolean("role")
                };
            }
            return null;
        }

        public void AddBookItem(BookItem bookItem)
        {
            using var connection = new MySqlConnection(connectionString);
            connection.Open();
            string query = "INSERT INTO books (imageurl, title, author, description) VALUES (@image, @title, @author, @desc)";
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@image", bookItem.ImageUrl);
            cmd.Parameters.AddWithValue("@title", bookItem.Title);
            cmd.Parameters.AddWithValue("@author", bookItem.Author);
            cmd.Parameters.AddWithValue("@desc", bookItem.Description);
            cmd.ExecuteNonQuery();
        }
        public List<BookItem> GetBookItems()
        {
            List<BookItem> books = new List<BookItem>();
            using var connection = new MySqlConnection(connectionString);
            connection.Open();
            string query = "SELECT * FROM books";
            using var cmd = new MySqlCommand(query, connection);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                books.Add(new BookItem
                {
                    Id = reader.GetInt32("id"),
                    ImageUrl = reader.GetString("imageurl"),
                    Title = reader.GetString("title"),
                    Author = reader.GetString("author"),
                    Description = reader.GetString("description")
                });
            }
            return books;
        }

        public void DeleteBookItem(int bookId)
        {
            using var connection = new MySqlConnection(connectionString);
            connection.Open();
            string query = "DELETE FROM books WHERE id = @id";
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", bookId);
            cmd.ExecuteNonQuery();
        }

        public void UpdateBookItem(BookItem bookItem)
        {
            using var connection = new MySqlConnection(connectionString);
            connection.Open();
            string query = "UPDATE books SET imageurl = @image, title = @title, author = @author, description = @desc WHERE id = @id";
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", bookItem.Id);
            cmd.Parameters.AddWithValue("@image", bookItem.ImageUrl);
            cmd.Parameters.AddWithValue("@title", bookItem.Title);
            cmd.Parameters.AddWithValue("@author", bookItem.Author);
            cmd.Parameters.AddWithValue("@desc", bookItem.Description);
            cmd.ExecuteNonQuery();
        }

        public void AddEventItem(EventItem eventItem)
        {
            using var connection = new MySqlConnection(connectionString);
            connection.Open();
            string query = "INSERT INTO events (title, date, description) VALUES (@title, @date, @desc)";
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@title", eventItem.Title);
            cmd.Parameters.AddWithValue("@date", eventItem.Date);
            cmd.Parameters.AddWithValue("@desc", eventItem.Description);
            cmd.ExecuteNonQuery();
        }

        public List<EventItem> GetEventItems()
        {
            List<EventItem> events = new List<EventItem>();
            using var connection = new MySqlConnection(connectionString);
            connection.Open();
            string query = "SELECT * FROM events";
            using var cmd = new MySqlCommand(query, connection);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                events.Add(new EventItem
                {
                    Id = reader.GetInt32("id"),
                    Title = reader.GetString("title"),
                    Date = reader.GetString("date"),
                    Description = reader.GetString("description")
                });
            }
            return events;
        }

        public void DeleteEventItem(int eventId)
        {
            using var connection = new MySqlConnection(connectionString);
            connection.Open();
            string query = "DELETE FROM events WHERE id = @id";
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", eventId);
            cmd.ExecuteNonQuery();
        }

        public void AddLinkItem(LinkItem linkItem)
        {
            using var connection = new MySqlConnection(connectionString);
            connection.Open();
            string query = "INSERT INTO links (description, link) VALUES (@desc, @link)";
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@desc", linkItem.Description);
            cmd.Parameters.AddWithValue("@link", linkItem.Link);
            cmd.ExecuteNonQuery();
        }

        public List<LinkItem> GetLinkItems()
        {
            List<LinkItem> links = new List<LinkItem>();
            using var connection = new MySqlConnection(connectionString);
            connection.Open();
            string query = "SELECT * FROM links";
            using var cmd = new MySqlCommand(query, connection);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                links.Add(new LinkItem
                {
                    Id = reader.GetInt32("id"),
                    Description = reader.GetString("description"),
                    Link = reader.GetString("link")
                });
            }
            return links;
        }

        public void DeleteLinkItem(int linkId)
        {
            using var connection = new MySqlConnection(connectionString);
            connection.Open();
            string query = "DELETE FROM links WHERE id = @id";
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", linkId);
            cmd.ExecuteNonQuery();
        }
    }
}
