using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using StationLogApp.Interfaces;
using StationLogApp.Model;
using StationLogApp.Persistancy;
using StationLogApp.ViewModel;

namespace StationLogApp.Handlers
{
    public class ManagerHandler
    {
        private CrudVM _crudVm;
        private ISave<TaskClass> _savedTaskClass = new SaveM<TaskClass>();
        private IUpdate<TaskClass> _updateTaskClass = new UpdateM<TaskClass>();

        public ManagerHandler(CrudVM crudVm)
        {
            _crudVm = crudVm;
        }
        public async void CreateTask()
        {
            TaskClass newTask = new TaskClass(
                _crudVm.NewItem.TaskId,
                _crudVm.NewItem.TaskName,
                _crudVm.NewItem.TaskSchedule,
                _crudVm.NewItem.Registration,
                _crudVm.NewItem.TaskType = "register",
                _crudVm.NewItem.DueDate,
                _crudVm.NewItem.DueDate,
                _crudVm.NewItem.Comment,
                _crudVm.NewItem.DoneVar = "N",
                _crudVm.NewItem.EquipmentID);

            await _savedTaskClass.Save(newTask, "Tasks");

            MessageDialog msg = new MessageDialog("Task created");
            await msg.ShowAsync();
        }

    }
}
