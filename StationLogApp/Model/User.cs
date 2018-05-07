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
        public string UserID { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string UserPassword { get; set; }
        public string UserType { get; set; }
        #endregion

        #region Constructors
        public User(string userID, string firstname, string surname, string username, string userPassword, string userType)
        {
            UserID = userID;
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
            return $"{UserID}, {Firstname}, {Surname}, {Username}, {UserPassword}, {UserType}";
        }

        #endregion
    }
}
