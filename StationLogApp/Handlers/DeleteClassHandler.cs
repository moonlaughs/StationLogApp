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
    public class DeleteClassHandler : IDeleteTask
    {
        private readonly DeleteTaskVm _deleteVm;
        private readonly IDelete<TaskClass> _delete = new DeleteM<TaskClass>();

        public async void DeleteTask()
        {
            if (_deleteVm.SelectedItem != null)
            {
                await _delete.Delete("Taks", _deleteVm.SelectedItem.TaskId);

                MessageDialog msg = new MessageDialog("Task deleted");
                await msg.ShowAsync();
            }
            else
            {
                MessageDialog msg = new MessageDialog("Please select the task");
                await msg.ShowAsync();
            }
        }

        public DeleteClassHandler(DeleteTaskVm deleteVm)
        {
            _deleteVm = deleteVm;
        }
    }
}
