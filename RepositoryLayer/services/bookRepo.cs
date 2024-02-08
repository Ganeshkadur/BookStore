using Microsoft.Data.SqlClient;
using ModelLayer.requestModels;
using RepositoryLayer.Entities;
using RepositoryLayer.interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace RepositoryLayer.services
{
    public class bookRepo:IBookRepo
    {
        string _connectionString = @"Data Source=NIMBUS2000\SQLEXPRESS;Initial Catalog=BookStore;Integrated Security=True;";

        
        public List<Book> getAllBooks()
        {

            using (SqlConnection conn = new SqlConnection(this._connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spGetAllBooks", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                List<Book> books = new List<Book>();
                while (reader.Read())
                {
                    Book book = new Book();
                    book.Id = reader.GetInt32("bookId");
                    book.Title = reader.GetString("title");
                    book.price = Convert.ToDouble(reader["price"]);
                    book.Author = reader.GetString("author");
                    book.Description = reader.GetString("detail");
                    book.quantity = reader.GetInt32("quantity");
                    book.image = reader.GetString("image");
                    books.Add(book);
                }
                return books;
            }
        }

        public Book GetBookById(int id)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                sql.Open();
                Book book = new Book();
                SqlCommand cmd = new SqlCommand("getBookById", sql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@bookId", id);
                SqlDataReader reader = cmd.ExecuteReader();
              if(reader.HasRows)
              {
                while (reader.Read())
                {
                    book.Id = reader.GetInt32("bookId");
                    book.Title = reader.GetString("title");
                    book.price = Convert.ToDouble(reader["price"]);
                    book.Author = reader.GetString("author");
                    book.Description = reader.GetString("detail");
                    book.quantity = reader.GetInt32("quantity");
                    book.image = reader.GetString("image");
                 }
                return book;

              }

              return null;
            }
        }

        public Book bookDetailsByAuthor(FetchByAuthor request)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                Book book = new Book();
                SqlCommand cmd = new SqlCommand("FetchBookByAuthor", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@author", request.Author);
                cmd.Parameters.AddWithValue("@title", request.Title);
                SqlDataReader reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                {
                    while (reader.Read())
                    {
                        book.Id = reader.GetInt32("bookId");
                        book.Title = reader.GetString("title");
                        book.price = Convert.ToDouble(reader["price"]);
                        book.Author = reader.GetString("author");
                        book.Description = reader.GetString("detail");
                        book.quantity = reader.GetInt32("quantity");
                        book.image = reader.GetString("image");
                    }
                    return book;

                }
                return null;
            }
        }

    }
}
