using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using StationLogApp.Common;
using StationLogApp.Convertor;
using StationLogApp.Interfaces;
using StationLogApp.Model;
using StationLogApp.Persistancy;
using StationLogApp.View;
using StationLogApp.ViewModel;

namespace StationLogApp.Handlers
{
    public class CreateTaskHandler : ICreateTask
    {
        #region instancefields
        private readonly CreateTaskVm _createVm;
        private readonly ISave<TaskClass> _savedTaskClass = new SaveM<TaskClass>();
        private readonly FrameNavigateClass _frameNavigateClass;
        #endregion

        #region Constructor
        public CreateTaskHandler(CreateTaskVm createVm)
        {
            _createVm = createVm;
            _frameNavigateClass = new FrameNavigateClass();
        }
        #endregion

        #region Methods
        public async void CreateTask()
        {
            if (_createVm.NewItem.TaskName != null && _createVm.NewItem.TaskSchedule != null && _createVm.NewItem.TaskType != null && _createVm.NewItem.EquipmentID != 0)
            {
                await _savedTaskClass.Save(new TaskClass(
                    _createVm.NewItem.TaskId,
                    _createVm.NewItem.TaskName,
                    _createVm.NewItem.TaskSchedule,
                    null,
                    _createVm.NewItem.TaskType,
                    DateTimeConvertor.DateTimeOffsetAndTimeSetToDateTime(_createVm.DueDate, TimeSpan.Zero),
                    null,
                    null,
                    _createVm.NewItem.DoneVar = "N",
                    _createVm.NewItem.EquipmentID), "Tasks");

                MessageDialog msg = new MessageDialog("Task created");
                await msg.ShowAsync();

                _frameNavigateClass.ActivateFrameNavigation(typeof(LogInPage));
            }
            else
            {
                MessageDialog msg = new MessageDialog("Please fill in the missing information");
                await msg.ShowAsync();
            }
        }
        #endregion
    }
}
