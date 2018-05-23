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
    public class NavigationHelperVm
    {
        private readonly FrameNavigateClass _frameNavigation;

        public RelayCommandClass GoTask { get; set; }
        public RelayCommandClass GoDone { get; set; }
        public RelayCommandClass DoGoCreate { get; set; }

        public string UserName { get; set; }
        public int UserId { get; set; }

        public string TaskName { get; set; }

        public UserSingleton CurrentUser { get; set; }

        public TaskEquipmentStationSingleton TaskSingleton { get; set; }

        public NavigationHelperVm()
        {
            CurrentUser = UserSingleton.GetInstance();
            TaskSingleton = TaskEquipmentStationSingleton.GetInstance();
            _frameNavigation = new FrameNavigateClass();
            GoTask = new RelayCommandClass(DoTask);
            GoDone = new RelayCommandClass(DoDone);
            DoGoCreate = new RelayCommandClass(GoCreate);
            UserName = CurrentUser.GetUsername();
            UserId = CurrentUser.GetUserID();
        }

        public void DoTask()
        {
            _frameNavigation.ActivateFrameNavigation(typeof(TaskPage), CurrentUser);
        }

        public void DoDone()
        {
            _frameNavigation.ActivateFrameNavigation(typeof(TaskHistoryTechnicianPage), CurrentUser);
        }

        public void GoCreate()
        {
            _frameNavigation.ActivateFrameNavigation(typeof(CreateTaskPage), CurrentUser);
        }
    }
}