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
    public class LoginVM : NotifyPropertyChangedClass
    {
        private User _currentUser = new User();

        private readonly FrameNavigateClass _frame;

        private UserSingleton _userSingleton;

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

        public LoginVM()
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
            Task<ObservableCollection<User>> sth = loaded.Load("UserTables");
            await sth;
            ObservableCollection<User> col = sth.Result;
            if (col != null)
            {
                foreach (User user in col)
                {
                    if ((user.Username == CurrentUser.Username) && (user.UserPassword == CurrentUser.UserPassword))
                    {
                        _userSingleton.SetPerson(user);
                        LoginStatus = true;
                        if (user.UserType == "admin" || user.UserType == "manager")
                        {
                            _frame.ActivateFrameNavigation(typeof(AddNotesPage), user);
                        }
                        else
                        {
                            _frame.ActivateFrameNavigation(typeof(MenuTreePage), user);
                        }
                        CurrentUser = user;
                        break;
                    }
                }
            }
            else
            {
                MessageDialog msg = new MessageDialog("No user found with that username and password!");
                await msg.ShowAsync();
            }
        }
    }
}
