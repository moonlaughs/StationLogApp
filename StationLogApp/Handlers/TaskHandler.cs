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
    public class TaskHandler:NotifyPropertyChangedClass
    {
        #region instance fields
        private readonly ISave<TaskClass> _savedTaskClass = new SaveM<TaskClass>();
        private readonly IUpdate<TaskClass> _updateTaskClass = new UpdateM<TaskClass>();
        private  TaskVm _taskVm;
        private readonly FrameNavigateClass _frameNavigation;
        private readonly Collections _collectionsClass;
        private SortingHandler _sortingHandler;

        
        #endregion

        #region properties
        public TaskClass SelectedTask => _taskVm.SelectedItem;

        public string SelectedPeriodicityItem { get; set; }
        public Station SelectedStation { get; set; }


        // Load a Collection Of TaskEquipmentStation that has to be connected to the TaskVM for showing in the TaskListView

        public ObservableCollection<TaskEquipmentStation> LoadedCollection { get; set; }


        public TaskVm TaskVm
        {
            get { return _taskVm; }
            set { _taskVm = value; }
        }

        #endregion

        #region Constructor
        public TaskHandler(TaskVm taskVm)
        {
            _taskVm = taskVm;
            _frameNavigation = new FrameNavigateClass();
            _collectionsClass = new Collections();
            _sortingHandler = new SortingHandler(this);
        }
        #endregion
        
        #region SaveAsDoneTask
        // This method is activated by the button of the relayCommand  
        // and save the logged task and add a task to the next date that it has to be made

        public void OperateTask()
        {
            SaveTaskClass();
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
                    TaskVm.SelectedItem.TaskId,
                    TaskVm.SelectedItem.TaskName,
                    TaskVm.SelectedItem.TaskSchedule,
                    TaskVm.SelectedItem.Registration,
                    TaskVm.SelectedItem.TaskType,
                    TaskVm.SelectedItem.DueDate,
                    newDate,
                    TaskVm.SelectedItem.Comment,
                    doneVariable,
                    TaskVm.SelectedItem.EquipmentId
                );

                _savedTaskClass.Save(newReSchedule, "Tasks");

                ReScheduleTask();

                _frameNavigation.ActivateFrameNavigation(typeof(MenuPage));

                var msg = new MessageDialog("Task saved");
                msg.ShowAsync();
                
                return newReSchedule;
            }
            else
            {
                var msg = new MessageDialog("Please select the task!");
                msg.ShowAsync();
            }
            return null;
        }
        #endregion

        #region RescheduleTask
        public void ReScheduleTask()
        {
            if (TaskVm.SelectedItem.TaskSchedule != null)
            {
                if (TaskVm.SelectedItem.TaskSchedule == "Every week")
                {
                    DoRescheduleTask(7);
                }

                else if (TaskVm.SelectedItem.TaskSchedule == "Every two weeks")
                {
                    DoRescheduleTask(14);
                }

                else if (TaskVm.SelectedItem.TaskSchedule == "Every three weeks")
                {
                    DoRescheduleTask(21);
                }

                else if (TaskVm.SelectedItem.TaskSchedule == "Every month")
                {
                    DoRescheduleTask(28);
                }

                else if (TaskVm.SelectedItem.TaskSchedule == "Every two months")
                {
                    DoRescheduleTask(56);
                }

                else if (TaskVm.SelectedItem.TaskSchedule == "Every three months")
                {
                    DoRescheduleTask(84);
                }
                else if (TaskVm.SelectedItem.TaskSchedule == "Every six months")
                {
                    DoRescheduleTask(168);
                }
                else if (TaskVm.SelectedItem.TaskSchedule == "Every year")
                {
                    DoRescheduleTask(436);
                }
            }
        }

        private void DoRescheduleTask(int periodicity)
        {
            var nextTuesdayDate = GetNextDate(periodicity);
            var newReSchedule = CreateReScheduledTask(nextTuesdayDate, "N");
            _updateTaskClass.Update(newReSchedule, "Tasks", TaskVm.SelectedItem.TaskId);
        }
        
        private DateTime GetNextDate(int periodicity)
        {
            var today = DateTime.Today;
            int daysUntilNextTuesday = ((int)Windows.Globalization.DayOfWeek.Tuesday - (int)today.DayOfWeek + periodicity) % periodicity;
            var nextTuesdayDate = today.AddDays(daysUntilNextTuesday);
            return nextTuesdayDate;
        }

        private TaskClass CreateReScheduledTask(DateTime newDate, string doneVariable)
        {
            var newReSchedule = new TaskClass(
                TaskVm.SelectedItem.TaskId,
                TaskVm.SelectedItem.TaskName,
                TaskVm.SelectedItem.TaskSchedule,
                TaskVm.SelectedItem.Registration = null,
                TaskVm.SelectedItem.TaskType,
                newDate,
                null,
                TaskVm.SelectedItem.Comment = null,
                doneVariable,
                TaskVm.SelectedItem.EquipmentId
            );
            return newReSchedule;
        }
        #endregion

        #region SortingMethods

        public void SortCollection()
        {
            _sortingHandler.SortCollection();
        }        
        #endregion

        public ObservableCollection<TaskEquipmentStation> LoadCollection()
        {
            LoadedCollection = _collectionsClass.LoadToDo();
            return LoadedCollection;
        }
    }
}