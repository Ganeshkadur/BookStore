using ModelLayer.requestModels;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.interfaces
{
    public interface ICartBusiness
    {
        public List<Book> AddToCart(AddToCartRequest cartRequest, int userId);
        public List<Book> GetCartBooks(int userId);
        public double GetPriceInCart(int userId);

        public QuantityUpdateRequest UpdateBookQuantity(int userId, QuantityUpdateRequest req);
    }
}
