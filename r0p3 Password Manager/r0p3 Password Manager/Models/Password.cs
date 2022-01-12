using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace r0p3_Password_Manager.Models
{
    public class Password
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Service { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Details { get; set; }
        public string PasswordEncrypted { get; set; }
        public bool Favorite { get; set; } = false;
        

        /*public Password(string service, string username, string email, string password)
        {
            Service = service;
            Username = username;
            Email = email;
            PasswordEncrypted = password;
        }*/
    }
}
