using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StationLogApp.Interfaces;

namespace StationLogApp.Model
{
    public class User
    {
        #region Properties
        public int UserId { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string UserPassword { get; set; }
        public string UserType { get; set; }
        #endregion

        #region Constructors
        public User(int userId, string firstname, string surname, string username, string userPassword, string userType)
        {
            UserId = userId;
            Firstname = firstname;
            Surname = surname;
            Username = username;
            UserPassword = userPassword;
            UserType = userType;
        }

        public User()
        {
            
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"{UserId}, {Firstname}, {Surname}, {Username}, {UserPassword}, {UserType}";
        }
        #endregion
    }
}