using BusinessLayer.interfaces;
using ModelLayer.requestModels;
using ModelLayer.responseModel;
using RepositoryLayer.Entities;
using RepositoryLayer.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.services
{
    public class userBusiness: IuserBusiness
    {
        private readonly IuserRepo _repo;
        public userBusiness(IuserRepo repo)
        {
            _repo = repo;

        }

        public UserModel Register(UserModel request)
        {
            return _repo.Register(request);
        }

        public ResponseModel<User> Login(string email, string password)
        {
            return _repo.Login(email, password);
        }

    }
}
