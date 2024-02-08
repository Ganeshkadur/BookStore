using BusinessLayer.interfaces;
using ModelLayer.requestModels;
using RepositoryLayer.Entities;
using RepositoryLayer.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.services
{
    public class CartBusiness:ICartBusiness
    {
       private readonly ICartRepo _repo;

        public CartBusiness(ICartRepo repo)
        {
            _repo = repo;
        }

        public List<Book> AddToCart(AddToCartRequest cartRequest, int userId)
        {
           return _repo.AddToCart(cartRequest, userId);
        }

        public List<Book> GetCartBooks(int userId)
        {
           return   _repo.GetCartBooks(userId);
        }

        public double GetPriceInCart(int userId)
        {
          return  _repo.GetPriceInCart(userId);
        }

        public QuantityUpdateRequest UpdateBookQuantity(int userId, QuantityUpdateRequest req)
        {
            return _repo.UpdateBookQuantity(userId, req);
        }

    }
}
