using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Entities
{
    public class User
    {

        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public long MobileNumber {  get; set; }  
    }
}
