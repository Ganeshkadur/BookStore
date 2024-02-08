using Microsoft.AspNetCore.DataProtection;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ModelLayer.requestModels;
using ModelLayer.responseModel;
using RepositoryLayer.Entities;
using RepositoryLayer.interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.services
{
    public class userRepo:IuserRepo
    {
        string _connectionString = @"Data Source=NIMBUS2000\SQLEXPRESS;Initial Catalog=BookStore;Integrated Security=True;";
        private readonly IDataProtector _protector;
        public readonly IConfiguration _config;

        public userRepo(IDataProtectionProvider provider, IConfiguration config)
        {
            _protector = provider.CreateProtector("Encryption_key");
            _config = config;

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

               // string encPassWord= _protector.Protect(request.Password);//encripted

                sqlCommand.Parameters.AddWithValue("@Password", request.Password);
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

        public ResponseModel<User> Login(string email,string password)
        {
            using(SqlConnection con = new SqlConnection(_connectionString))
            {  
                User user=new User();
                con.Open();
               
                string query = $"select * from userTable where email = '{email}'";
                SqlCommand sqlCommand = new SqlCommand(query,con);
                SqlDataReader rdr =sqlCommand.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        user.UserId = Convert.ToInt32(rdr["userId"]);
                        user.FullName = rdr["name"].ToString();
                        user.Email = rdr["email"].ToString();
                        user.Password = rdr["password"].ToString();
                        user.MobileNumber =Convert.ToInt64(rdr["mobnum"]);

                        
                        if(user.Password == password)
                        {
                            var token = GenerateToken(user.UserId, email);

                            return new ResponseModel<User>() { Success = true,Message=token,Data=user };

                        }


                    }
                    
                }
                return null;
            }
        }


      

        private string GenerateToken(int userId, string email)
        {
            
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
        new Claim("Email",email),
        new Claim("UserId", userId.ToString())
             };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



    }
}
