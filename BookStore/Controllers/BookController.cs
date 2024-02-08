using BusinessLayer.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.requestModels;
using ModelLayer.responseModel;
using RepositoryLayer.Entities;
using System.Collections.Generic;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookStore.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class BookController : ControllerBase
    {

        private readonly IBookBusiness _business;

        public BookController(IBookBusiness business)
        {
            _business = business;
        }

        [HttpGet]
        public IActionResult getAllBooks()
        {
            List<Book> books=_business.getAllBooks();
            if (books != null)
            {
                return Ok(new ResponseModel<List<Book>>(){Success=true,Message="fetched all the books",Data=books });
            }

            return BadRequest(new ResponseModel<List<Book>>() { Success = false, Message = " can not fetch all the books", Data = books });


        }

        [HttpGet]
        public IActionResult GetBookById(int id)
        {
            Book book = _business.GetBookById(id);
            if(book != null)
            {
                return Ok(new ResponseModel<Book>() { Success = true, Message = "fetched book by id", Data = book });
            }

            return Ok(new ResponseModel<Book>() { Success = false, Message = "fetched book by id failed", Data = book });
        }

        [HttpGet]
        public IActionResult bookDetailsByAuthor(FetchByAuthor request)
        {
             Book book=_business.bookDetailsByAuthor(request);
            if(book != null)
            {
                return Ok(new ResponseModel<Book>() { Success = true, Message = "book found sucess", Data = book });
            }

            return BadRequest(new ResponseModel<Book>() { Success = false, Message = "book not found", Data = book });
        }
    }
}
