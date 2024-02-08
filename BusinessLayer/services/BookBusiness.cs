using BusinessLayer.interfaces;
using ModelLayer.requestModels;
using RepositoryLayer.Entities;
using RepositoryLayer.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.services
{
    public class BookBusiness:IBookBusiness
    {
        private readonly IBookRepo _repo;

        public BookBusiness(IBookRepo repo)
        {
            _repo = repo;
        }
        public List<Book> getAllBooks()
        {
            return _repo.getAllBooks();

        }

        public Book GetBookById(int id)
        {
            return _repo.GetBookById(id);
        }


        public Book bookDetailsByAuthor(FetchByAuthor request)
        {
            return _repo.bookDetailsByAuthor(request);
        }
    }


}
