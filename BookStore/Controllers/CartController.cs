using BusinessLayer.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.requestModels;
using ModelLayer.responseModel;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookStore.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartBusiness _business;
        public CartController(ICartBusiness business)
        {
            _business = business;
        }

        [HttpPost]
        public IActionResult AddToCart(AddToCartRequest cartRequest)
        {

            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);

            List<Book> books=_business.AddToCart(cartRequest, userId);
            if (books != null)
            {
                
                return Ok(new ResponseModel<List<Book>>() {Success=true,Message="all the books in cart ",Data=books });
            }
            else
            {
                return BadRequest(new ResponseModel<List<Book>>() { Success = false, Message = "cant add  books into cart ", Data = books });

            }
        }

        [HttpGet]
        public IActionResult GetCartBooksByUserId()
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);

            List<Book> books=_business.GetCartBooks(userId);
            if (books != null)
            {
                return Ok(new ResponseModel<List<Book>>() { Success = true, Message = "all the books in cart based on user id ", Data = books });
            }
            return BadRequest(new ResponseModel<List<Book>>() { Success = false, Message = "cant find the books based on the userid", Data = books });

        }

        [HttpGet]
        public IActionResult GetPriceInCart()
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);



            double price = _business.GetPriceInCart(userId);

            if (price >=0)
            {
                return Ok(new ResponseModel<double>() { Success = true, Message = "the total price is ", Data = price });
            }
            return BadRequest(new ResponseModel<double>() { Success = false, Message = "the total price cant be get", Data = price });



        }

        [HttpPut]
        public IActionResult UpdateBookQuantity( QuantityUpdateRequest req)
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);

            var result =_business.UpdateBookQuantity(userId, req);
            if(result != null)
            {
                return Ok(new ResponseModel<QuantityUpdateRequest>() { Success = true, Message = "quantity of book updated ", Data = result });
            }

            return BadRequest(new ResponseModel<QuantityUpdateRequest>() { Success = true, Message = "quantity of book failed updated ", Data = result });


        }
    }
}
