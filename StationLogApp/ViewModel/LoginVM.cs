using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using StationLogApp.Common;
using StationLogApp.Interfaces;
using StationLogApp.Methods;
using StationLogApp.Model;
using StationLogApp.Persistancy;
using StationLogApp.Singletons;
using StationLogApp.View;

namespace StationLogApp.ViewModel
{
    public class LoginVM : NotifyPropertyChangedClass
    {
        private IUserFactory _currentUser = new User();
        public RelayCommandClass CheckCommand { get; set; }
        private readonly FrameNavigateClass _frame;
        private readonly UserSingleton _userSingleton;
        private bool LoginStatus { get; set; }
        

        public LoginVM()
        {

            _frame = new FrameNavigateClass();
            _userSingleton = UserSingleton.GetInstance();
            LoginCheck loginCheck = new LoginCheck();
            CheckCommand = new RelayCommandClass(loginCheck.Check);
        }


    }

}
