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
        private Collections Col { get; set; }

        public RelayCommandClass DoGoTask { get; set; }
        private ButtonsVm Bvm { get; }

        public UpdateTaskHandler(UpdateTaskVm updateVm)
        {
            _updateVm = updateVm;
            _frameNavigateClass = new FrameNavigateClass();
            Bvm = new ButtonsVm();
            DoGoTask = new RelayCommandClass(GoTask);
            Col = new Collections();
        }
        
        public async void UpdateTask()
        {
            if (_updateVm.SelectedItem != null)
            {
                TaskClass updatedItem = null;
                foreach (var item in Col.LoadToDo())
                {
                    if (item.TaskId == _updateVm.TaskId)
                    {
                        updatedItem = new TaskClass(
                            item.TaskId,
                            item.TaskName = _updateVm.TaskName,
                            item.TaskSchedule = _updateVm.TaskSchedule,
                            item.Registration = null,
                            item.TaskType = _updateVm.TaskType,
                            item.DueDate =
                                DateTimeConvertor.DateTimeOffsetAndTimeSetToDateTime(_updateVm.DueDate, TimeSpan.Zero),
                            null,
                            item.Comment = null,
                            item.DoneVar = "N",
                            item.EquipmentID = _updateVm.EquipmentID
                        );

                    }
                }

                await _update.Update(updatedItem, "Tasks", _updateVm.SelectedItem.TaskId);

                GoTask();

                MessageDialog msg = new MessageDialog("Task updated");
                await msg.ShowAsync();
            }
            else
            {
                MessageDialog msg = new MessageDialog("Please select item");
                await msg.ShowAsync();
            }
            //if (_updateVm.SelectedItem != null)
            //{
            //    //TaskClass updatedItem = new TaskClass(
            //    //    );
            //    //    ///*_updateVm.SelectedItem.TaskId = */_updateVm.TaskId,
            //    //    ///*_updateVm.SelectedItem.TaskName = */_updateVm.TaskName,
            //    //    ///*_updateVm.SelectedItem.TaskSchedule =*/_updateVm.TaskSchedule,
            //    //    ///*_updateVm.SelectedItem.Registration = */null,
            //    //    ///*_updateVm.SelectedItem.TaskType = */_updateVm.TaskType,
            //    //    //DateTimeConvertor.DateTimeOffsetAndTimeSetToDateTime(_updateVm.DueDate, TimeSpan.Zero),
            //    //    //null,
            //    //    ///*_updateVm.SelectedItem.Comment = */null,
            //    //    ///*_updateVm.SelectedItem.DoneVar = */"N",
            //    //    ///*_updateVm.SelectedItem.EquipmentID = */_updateVm.EquipmentID
            //    //    //);




            //}
        }

        public void GoTask()
        {
            Bvm.DoTask();
        }
    }
}
