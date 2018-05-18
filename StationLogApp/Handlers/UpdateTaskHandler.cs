using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Automation.Peers;
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

        public RelayCommandClass DoGoTask { get; set; }
        private ButtonsVm Bvm { get; }

        public UpdateTaskHandler(UpdateTaskVm updateVm)
        {
            _updateVm = updateVm;
            _frameNavigateClass = new FrameNavigateClass();
            Bvm = new ButtonsVm();
            DoGoTask = new RelayCommandClass(GoTask);
        }
        
        public async void UpdateTask()
        {
            if (_updateVm.TaskId != 0)
            {
                TaskClass updatedItem = new TaskClass(
                _updateVm.TaskId,
                _updateVm.TaskName,
                _updateVm.TaskSchedule,
                _updateVm.Registration,
                _updateVm.TaskType,
                DateTimeConvertor.DateTimeOffsetAndTimeSetToDateTime(_updateVm.DueDate, TimeSpan.Zero),
                null,
                _updateVm.Comment,
                _updateVm.DoneVar,
                _updateVm.EquipmentId
                );

                await _update.Update(updatedItem, "Tasks", _updateVm.TaskId);

                GoTask();

                MessageDialog msg = new MessageDialog("Task updated");
                await msg.ShowAsync();
            }
            else
            {
                MessageDialog msg = new MessageDialog("Please select item");
                await msg.ShowAsync();
            }
        }

        public void GoTask()
        {
            Bvm.DoTask();
        }
    }
}
