using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationLogApp.Interfaces
{
    public interface IUserFactory
    {
        string UserID { get; set; }
        string Firstname { get; set; }
        string Surname { get; set; }
        string Username { get; set; }
        string UserPassword { get; set; }
        string UserType { get; set; }
    }
}
