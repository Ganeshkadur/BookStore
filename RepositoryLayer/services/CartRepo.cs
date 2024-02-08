using Microsoft.Data.SqlClient;
using ModelLayer.requestModels;
using RepositoryLayer.Entities;
using RepositoryLayer.interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.PortableExecutable;
using System.Text;
using static System.Reflection.Metadata.BlobBuilder;

namespace RepositoryLayer.services
{
    public class CartRepo : ICartRepo
    {
        string _connectionString = @"Data Source=NIMBUS2000\SQLEXPRESS;Initial Catalog=BookStore;Integrated Security=True;";


        public List<Book> AddToCart(AddToCartRequest cartRequest, int userId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spaddToCart", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@bookId", cartRequest.bookId);
                cmd.Parameters.AddWithValue("@quantity", cartRequest.quantity);

                cmd.ExecuteNonQuery();
                return GetCartBooks(userId);
            }
        }

        public List<Book> GetCartBooks(int userId)
        {

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("spGetAllbookByUserId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", userId);
                SqlDataReader reader = cmd.ExecuteReader();

                List<Book> list = new List<Book>();
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
                    list.Add(book);

                }

                return list;

            }
        }

        public double GetPriceInCart(int userId)
        {
            List<Book> bookList = GetCartBooks(userId);
            double totalPrice = 0;
            foreach (var book in bookList)
            {
                totalPrice += (book.quantity * book.price);
            }
            return totalPrice;
        }


        public QuantityUpdateRequest UpdateBookQuantity(int userId, QuantityUpdateRequest req)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spUpdateQuantityInCart", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@quantity", req.quantity);
                cmd.Parameters.AddWithValue("@bookId", req.bookId);

                int rowaffected=cmd.ExecuteNonQuery();

                if(rowaffected > 0)
                {
                    return req;
                }
                else
                {
                    return null;
                }

                
            }
        }

    }
}