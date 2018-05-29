using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using StationLogApp.Common;
using StationLogApp.Interfaces;
using StationLogApp.Model;
using StationLogApp.Persistancy;
using StationLogApp.Singletons;
using StationLogApp.View;

namespace StationLogApp.ViewModel
{
    public class LoginVm : NotifyPropertyChangedClass
    {
        private User _currentUser = new User();

        private readonly FrameNavigateClass _frame;

        private readonly UserSingleton _userSingleton;

        private bool LoginStatus { get; set; }

        public RelayCommandClass CheckCommand { get; set; }

        public User CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
        }

        public LoginVm()
        {
            _frame = new FrameNavigateClass();
            _userSingleton = UserSingleton.GetInstance();
            CheckCommand = new RelayCommandClass(Check);
        }

        // Check checks the Currrent User information against tha information of the user in the database
        public async void Check()
        {
            LoginStatus = false;
            ILoad<User> loaded = new LoadM<User>();
            var sth = loaded.Load("UserTables");
            await sth;
            var col = sth.Result;
            if (col != null)
            {
                foreach (User user in col)
                {
                    if ((user.Username != CurrentUser.Username) ||
                        (user.UserPassword != CurrentUser.UserPassword)) continue;
                    _userSingleton.SetPerson(user);
                    LoginStatus = true;
                    if (user.UserType == "admin" || user.UserType == "manager")
                    {
                        _frame.ActivateFrameNavigation(typeof(MenuPage), user);
                        CurrentUser = user;
                        break;
                    }
                    else
                    {
                        _frame.ActivateFrameNavigation(typeof(MenuPage), user);
                        CurrentUser = user;
                        break;
                    }
                }
                if (LoginStatus != false) return;
                var msg = new MessageDialog("Incorrect username or password.");
                await msg.ShowAsync();
            }
            else
            {
                var msg = new MessageDialog("Incorrect username or password.");
                await msg.ShowAsync();
            }
        }
    }
}