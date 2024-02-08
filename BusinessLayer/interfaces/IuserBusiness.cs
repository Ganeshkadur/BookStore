using ModelLayer.requestModels;
using ModelLayer.responseModel;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.interfaces
{
    public interface IuserBusiness
    {
        public UserModel Register(UserModel request);
        public ResponseModel<User> Login(string email, string password);
    }
}
