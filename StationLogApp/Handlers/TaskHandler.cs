using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class TaskHandler
    {
        #region instance fields
        private readonly ISave<TaskClass> _savedTaskClass = new SaveM<TaskClass>();
        private readonly IUpdate<TaskClass> _updateTaskClass = new UpdateM<TaskClass>();

        private readonly TaskVm _taskVm;
        private readonly FrameNavigateClass _frameNavigation;
        #endregion

        #region properties
        public TaskClass SelectedTask
        {
            get { return _taskVm.SelectedItem; }
        }

        public TaskHandler(TaskVm taskVm)
        {
            _taskVm = taskVm;
            _frameNavigation = new FrameNavigateClass();
        }
        #endregion
        
        #region SaveAsDoneTask
        // This method is activated by the button of the relayCommand  
        // and save the logged task and add a task to the next date that it has to be made
        public void OperateTask()
        {
            SaveTaskClass();
            _frameNavigation.ActivateFrameNavigation(typeof(TaskPage));
        }
        
        public void SaveTaskClass()
        {
            DateTime loggedDate = DateTime.Now;
            TaskClass loggedTask = PostDoneTask(loggedDate, "Y");
        }
        
        private TaskClass PostDoneTask(DateTime newDate, string doneVariable)
        {
            if (SelectedTask.TaskId != 0)
            {
                TaskClass newReSchedule = new TaskClass(
                    _taskVm.SelectedItem.TaskId,
                    _taskVm.SelectedItem.TaskName,
                    _taskVm.SelectedItem.TaskSchedule,
                    _taskVm.SelectedItem.Registration,
                    _taskVm.SelectedItem.TaskType,
                    _taskVm.SelectedItem.DueDate,
                    newDate,
                    _taskVm.SelectedItem.Comment,
                    doneVariable,
                    _taskVm.SelectedItem.EquipmentID
                );

                _savedTaskClass.Save(newReSchedule, "Tasks");

                ReScheduleTask();

                MessageDialog msg = new MessageDialog("Task saved");
                msg.ShowAsync();

                return newReSchedule;
            }
            else
            {
                MessageDialog msg = new MessageDialog("Please select the task!");
                msg.ShowAsync();
            }
            return null;
        }
        #endregion

        #region RescheduleTask
        public void ReScheduleTask()
        {
            if (_taskVm.SelectedItem.TaskSchedule != null)
            {
                if (_taskVm.SelectedItem.TaskSchedule == "Every week")
                {
                    DoRescheduleTask(7);
                }

                else if (_taskVm.SelectedItem.TaskSchedule == "Every two weeks")
                {
                    DoRescheduleTask(14);
                }

                else if (_taskVm.SelectedItem.TaskSchedule == "Every three weeks")
                {
                    DoRescheduleTask(21);
                }

                else if (_taskVm.SelectedItem.TaskSchedule == "Every month")
                {
                    DoRescheduleTask(28);
                }

                else if (_taskVm.SelectedItem.TaskSchedule == "Every two months")
                {
                    DoRescheduleTask(56);
                }

                else if (_taskVm.SelectedItem.TaskSchedule == "Every three months")
                {
                    DoRescheduleTask(84);
                }
                else if (_taskVm.SelectedItem.TaskSchedule == "Every six months")
                {
                    DoRescheduleTask(168);
                }
                else if (_taskVm.SelectedItem.TaskSchedule == "Every year")
                {
                    DoRescheduleTask(436);
                }
            }
        }

        private void DoRescheduleTask(int periodicity)
        {
            DateTime nextTuesdayDate = GetNextDate(periodicity);
            TaskClass newReSchedule = CreateReScheduledTask(nextTuesdayDate, "N");
            _updateTaskClass.Update(newReSchedule, "Tasks", _taskVm.SelectedItem.TaskId);
        }
        
        private DateTime GetNextDate(int periodicity)
        {
            DateTime today = DateTime.Today;
            DateTime nextTuesdayDate = today.AddDays(periodicity + 1);
            return nextTuesdayDate;
        }

        private TaskClass CreateReScheduledTask(DateTime newDate, string doneVariable)
        {
            TaskClass newReSchedule = new TaskClass(
                _taskVm.SelectedItem.TaskId,
                _taskVm.SelectedItem.TaskName,
                _taskVm.SelectedItem.TaskSchedule,
                _taskVm.SelectedItem.Registration = null,
                _taskVm.SelectedItem.TaskType,
                newDate,
                null,
                _taskVm.SelectedItem.Comment = null,
                doneVariable,
                _taskVm.SelectedItem.EquipmentID
            );
            return newReSchedule;
        }
        #endregion
    }
}
