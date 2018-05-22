using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StationLogApp.Interfaces;
using StationLogApp.Model;

namespace StationLogApp.Singletons
{
    public class UserSingleton
    {
        #region Instance fields
        public static User User;
        #endregion

        #region Properties
        private static UserSingleton Instance { get; set; }
        #endregion

        #region Constructor
        public UserSingleton()
        {
            User = new User();
        }
        #endregion

        #region Methods
        public static UserSingleton GetInstance()
        {
            if (Instance == null)
            {
                Instance = new UserSingleton();
            }
            return Instance;
        }

        public void SetPerson(User user)
        {
            User = user;
        }

        public int GetUserID()
        {
            return User.UserId;
        }

        public string GetFirstname()
        {
            return User.Firstname;
        }

        public string GetSurname()
        {
            return User.Surname;
        }

        public string GetUsername()
        {
            return User.Username;
        }

        public int GetUserPassword()
        {
            return User.UserId;
        }

        public string GetUserType()
        {
            return User.UserType;
        }
        #endregion
    }
}