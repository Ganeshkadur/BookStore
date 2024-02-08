using ModelLayer.requestModels;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.interfaces
{
    public  interface IBookRepo
    {
        public List<Book> getAllBooks();
        public Book GetBookById(int id);

        public Book bookDetailsByAuthor(FetchByAuthor request);
    }
}
