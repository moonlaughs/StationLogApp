using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using StationLogApp.Common;
using StationLogApp.Interfaces;
using StationLogApp.Model;
using StationLogApp.Persistancy;
using StationLogApp.View;
using StationLogApp.ViewModel;

namespace StationLogApp.Handlers
{
    public class DeleteTaskHandler
    {
        private readonly TaskVm _deleteVm;
        private readonly FrameNavigateClass _frame;
        private readonly IDelete<TaskClass> _delete = new DeleteM<TaskClass>();

        public async void DeleteTask(int key)
        {
            if (_deleteVm.SelectedItem != null)
            {
                await _delete.Delete("Tasks", key);

                _frame.ActivateFrameNavigation(typeof(TaskPage));

                var msg = new MessageDialog("Task deleted");
                await msg.ShowAsync();
            }
            else
            {
                var msg = new MessageDialog("Please select the task");
                await msg.ShowAsync();
            }
        }

        public DeleteTaskHandler(TaskVm deleteVm)
        {
            _deleteVm = deleteVm;
            _frame = new FrameNavigateClass();
        }
    }
}
