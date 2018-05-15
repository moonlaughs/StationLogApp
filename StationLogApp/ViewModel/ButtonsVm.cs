using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using StationLogApp.Common;
using StationLogApp.Handlers;
using StationLogApp.Model;
using StationLogApp.Singletons;
using StationLogApp.View;

namespace StationLogApp.ViewModel
{
    public class ButtonsVm
    {
        private UserSingleton _currentUser;
        private readonly FrameNavigateClass _frameNavigation;

        public RelayCommandClass GoTask { get; set; }
        public RelayCommandClass GoDone { get; set; }

        public string UserName { get; set; }

        public UserSingleton CurrentUser
        {
            get => _currentUser;
            set => _currentUser = value;
        }

        public ButtonsVm()
        {
            _currentUser = UserSingleton.GetInstance();
            _frameNavigation = new FrameNavigateClass();
            GoTask = new RelayCommandClass(DoTask);
            GoDone = new RelayCommandClass(DoDone);
            UserName = _currentUser.GetUsername();
    }

        public void DoTask()
        {
            _frameNavigation.ActivateFrameNavigation(typeof(TaskPage), _currentUser);
        }

        public void DoDone()
        {
            _frameNavigation.ActivateFrameNavigation(typeof(TaskHistoryTechnicianPage), _currentUser);
        }
    }
}
