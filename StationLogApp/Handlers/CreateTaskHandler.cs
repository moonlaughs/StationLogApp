using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using StationLogApp.Common;
using StationLogApp.Converters;
using StationLogApp.Interfaces;
using StationLogApp.Model;
using StationLogApp.Persistancy;
using StationLogApp.View;
using StationLogApp.ViewModel;

namespace StationLogApp.Handlers
{
    public class CreateTaskHandler
    {
        #region instancefields
        private readonly CreateTaskVm _createVm;
        private readonly ISave<TaskClass> _savedTaskClass = new SaveM<TaskClass>();
        #endregion

        public RelayCommandClass DoGoTask { get; set; }
        private NavigationHelperVm Bvm { get; }
        public DateConverter Dc { get; }

        #region Constructor
        public CreateTaskHandler(CreateTaskVm createVm)
        {
            _createVm = createVm;
            Bvm = new NavigationHelperVm();
            DoGoTask = new RelayCommandClass(GoTask);
            Dc = new DateConverter();
        }
        #endregion

        #region Methods
        public async void CreateTask()
        {
            if (_createVm.NewItem.TaskName != null && _createVm.NewItem.TaskSchedule != null && _createVm.NewItem.TaskType != null && _createVm.NewItem.EquipmentId != 0)
            {
                await _savedTaskClass.Save(new TaskClass(
                    _createVm.NewItem.TaskId,
                    _createVm.NewItem.TaskName,
                    _createVm.NewItem.TaskSchedule,
                    null,
                    _createVm.NewItem.TaskType,
                    Dc.ConvertToDate(_createVm.DueDate),
                    null,
                    null,
                    _createVm.NewItem.DoneVar = "N",
                    _createVm.NewItem.EquipmentId), "Tasks");
                
                GoTask();

                MessageDialog msg = new MessageDialog("Task created");
                await msg.ShowAsync();
            }
            else
            {
                MessageDialog msg = new MessageDialog("Please fill in the missing information");
                await msg.ShowAsync();
            }
        }
        #endregion

        public void GoTask()
        {
            Bvm.DoTask();
        }
    }
}
