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
        public static User _user;
        #endregion

        #region Properties
        private static UserSingleton Instance { get; set; }
        #endregion

        #region Constructor
        public UserSingleton()
        {
            _user = new User();
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
            _user = user;
        }

        public int GetUserID()
        {
            return _user.UserID;
        }

        public string GetFirstname()
        {
            return _user.Firstname;
        }

        public string GetSurname()
        {
            return _user.Surname;
        }

        public string GetUsername()
        {
            return _user.Username;
        }

        public int GetUserPassword()
        {
            return _user.UserID;
        }

        public string GetUserType()
        {
            return _user.UserType;
        }
        #endregion
    }
}
 