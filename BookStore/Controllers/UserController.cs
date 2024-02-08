using BusinessLayer.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.requestModels;
using ModelLayer.responseModel;
using RepositoryLayer.Entities;

namespace BookStore.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IuserBusiness _business;

        public UserController(IuserBusiness business)
        {
            this._business = business;
        }

        [HttpPost]
        public IActionResult Register(UserModel request)
        {
            var result=_business.Register(request);
            if (result == null)
            {
                return BadRequest(new ResponseModel<UserModel>() { Success=false ,Message="register failed",Data=null});
            }
            else
            {
                return Ok(new ResponseModel<UserModel>() { Success = true, Message = "register Success..", Data = request });

            }

        }

        [HttpPost]
        public IActionResult Login(loginModel request)
        {
            var result = _business.Login(request.Email,request.Password);
            if (result == null)
            {
                return BadRequest(new ResponseModel<User>() { Success = false, Message = "login failed", Data = null });
            }
            else
            {
                return Ok(result);

            }

        }

    }
}
