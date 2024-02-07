using Microsoft.AspNetCore.DataProtection;
using Microsoft.Data.SqlClient;
using ModelLayer.requestModels;
using ModelLayer.responseModel;
using RepositoryLayer.Entities;
using RepositoryLayer.interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace RepositoryLayer.services
{
    public class userRepo:IuserRepo
    {
        string _connectionString = @"Data Source=NIMBUS2000\SQLEXPRESS;Initial Catalog=BookStore;Integrated Security=True;";
        private readonly IDataProtector _protector;


        public userRepo(IDataProtectionProvider provider)
        {
            _protector = provider.CreateProtector("Encryption_key");

        }

        public UserModel Register(UserModel request)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();

                SqlCommand sqlCommand = new SqlCommand("spRegisterUser", con);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@FullName", request.fullName);
                sqlCommand.Parameters.AddWithValue("@Email", request.Email);

                string encPassWord= _protector.Protect(request.Password);//encripted

                sqlCommand.Parameters.AddWithValue("@Password", encPassWord);
                sqlCommand.Parameters.AddWithValue("@MobileNum", request.MobileNumber);

                int result = sqlCommand.ExecuteNonQuery();
                con.Close();
                if (result == 0)
                {

                    return null;
                }
                else
                {
                    return request;
                }


                
            }

        }


    }
}
