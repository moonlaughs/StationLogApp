using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Automation.Peers;
using StationLogApp.Common;
using StationLogApp.Converters;
using StationLogApp.Interfaces;
using StationLogApp.Model;
using StationLogApp.Persistancy;
using StationLogApp.View;
using StationLogApp.ViewModel;
using StationLogApp.Singletons;

namespace StationLogApp.Handlers
{
    public class UpdateTaskHandler
    {
        private readonly UpdateTaskVm _updateVm;
        private readonly IUpdate<TaskClass> _update = new UpdateM<TaskClass>();

        public NavigationHelperVm Bvm { get; }
        public DateConverter Dc { get; }
        public TaskVm Tvm { get; set; }

        public RelayCommandClass DoGoTask { get; set; }
        public TaskEquipmentStationSingleton Singleton { get; set; }

        public UpdateTaskHandler(UpdateTaskVm updateVm)
        {
            _updateVm = updateVm;
            Bvm = new NavigationHelperVm();
            DoGoTask = new RelayCommandClass(GoTask);
            Dc = new DateConverter();
            Tvm = new TaskVm();
            Tvm.SelectedItem = new TaskEquipmentStation();
            Singleton = TaskEquipmentStationSingleton.GetInstance();
        }
        
        public async void UpdateTask()
        {
            if (Singleton.GetTaskId() != 0)
            {
                var updatedItem = new TaskClass(
                Singleton.GetTaskId(),
                Tvm.SelectedItem.TaskName = Singleton.GetTaskName(),
                Tvm.SelectedItem.TaskSchedule = Singleton.GetTaskSchedule(),
                null,
                Tvm.SelectedItem.TaskType = Singleton.GetTaskType(),
                Dc.ConvertToDate(_updateVm.DueDate),
                null,
                null,
                Tvm.SelectedItem.DoneVar = Singleton.GetDoneVar(),
                Tvm.SelectedItem.EquipmentId = Singleton.GetEquipmentId()
                );

                await _update.Update(updatedItem, "Tasks", Singleton.GetTaskId());

                GoTask();

                var msg = new MessageDialog("Task updated");
                await msg.ShowAsync();
            }
            else
            {
                var msg = new MessageDialog("Please select item");
                await msg.ShowAsync();
            }
        }

        public void GoTask()
        {
            Bvm.DoTask();
        }
    }
}
