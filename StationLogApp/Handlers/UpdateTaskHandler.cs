using System;
using System.Collections.Generic;
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
    public class UpdateTaskHandler : IUpdateTask
    {
        private readonly UpdateTaskVm _updateVm;
        private readonly IUpdate<TaskClass> _update = new UpdateM<TaskClass>();
        private readonly FrameNavigateClass _frameNavigateClass;

        public UpdateTaskHandler(UpdateTaskVm updateVm)
        {
            _updateVm = updateVm;
            _frameNavigateClass = new FrameNavigateClass();
        }
        
        public async void UpdateTask()
        {
            if (_updateVm.SelectedItem != null)
            {
                TaskClass updatedItem = new TaskClass(
                    _updateVm.SelectedItem.TaskId,
                    _updateVm.SelectedItem.TaskName,
                    _updateVm.SelectedItem.TaskSchedule,
                    _updateVm.SelectedItem.Registration = null,
                    _updateVm.SelectedItem.TaskType,
                    DateTimeConvertor.DateTimeOffsetAndTimeSetToDateTime(_updateVm.DueDate, TimeSpan.Zero),
                    null,
                    _updateVm.SelectedItem.Comment = null,
                    _updateVm.SelectedItem.DoneVar = "N",
                    _updateVm.SelectedItem.EquipmentID
                    );

                await _update.Update(updatedItem, "Tasks", _updateVm.SelectedItem.TaskId);

                MessageDialog msg = new MessageDialog("Task updated");
                await msg.ShowAsync();

                _frameNavigateClass.ActivateFrameNavigation(typeof(TaskPage));
            }
            else
            {
                MessageDialog msg = new MessageDialog("Please select item");
                await msg.ShowAsync();
            }
        }
    }
}
