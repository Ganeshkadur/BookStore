using Microsoft.Data.SqlClient;
using ModelLayer.requestModels;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace RepositoryLayer.services
{
    public class OrderRepo
    {
        string _connectionString = @"Data Source=NIMBUS2000\SQLEXPRESS;Initial Catalog=BookStore;Integrated Security=True;";


        public List<Book> Addorder(AddOrderRequest request, int userId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("addOrder", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@bookId", request.bookId);
                cmd.Parameters.AddWithValue("@quantity", request.quantity);
                cmd.ExecuteNonQuery();
                return GetAllOrders(userId);
            }
        }

        public List<Book> GetAllOrders(long userId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("getAllOrder", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", userId);
                SqlDataReader reader = cmd.ExecuteReader();
                List<Book> orders = new List<Book>();
                while (reader.Read())
                {
                    Book book = new Book();
                    book.Id = Convert.ToInt32(reader["bookId"]);
                    book.Author = (reader["author"]).ToString();
                    book.Title = reader.GetString("Title");
                    book.Description = reader.GetString("detail");
                    book.quantity = reader.GetInt32("order_quantity");
                    book.price = Convert.ToDouble(reader["price"]);
                    orders.Add(book);
                }
                return orders;
            }
        }

        public double GetPriceInOrder(long userId)
        {
            List<Book> bookList = GetAllOrders(userId);
            double totalPrice = 0;
            foreach (var book in bookList)
            {
                totalPrice += (book.quantity * book.price);
            }
            return totalPrice;
        }


        public List<Book> OrderCart(List<AddOrderRequest> orders, int userId, string email)
        {
            foreach (var order in orders)
            {
                Addorder(order, userId);
            }
            double TotalPrice = GetPriceInOrder(userId);
            List<Book> bookList = GetAllOrders(userId);
            string stringList = bookList.ToString();

           // SendTokKen(email, stringList, TotalPrice);
            return bookList;
        }

    }
}
