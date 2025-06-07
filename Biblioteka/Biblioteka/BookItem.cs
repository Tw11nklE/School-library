using System;
using System.Collections.Generic;
using System.Text;

namespace Biblioteka
{
    public class BookItem
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
    }
}