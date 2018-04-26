using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationLogApp.Interfaces
{
    public class IUserFactory
    {
        #region 
        public string UserID { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string UserPassword { get; set; }
        public string UserType { get; set; }
        #endregion
    }
}
