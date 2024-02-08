using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public double price { get; set; }
        public long quantity { get; set; }
        public string image { get; set; }
    }
}
