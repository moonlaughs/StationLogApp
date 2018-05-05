using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StationLogApp.Interfaces;
using StationLogApp.Model;
using StationLogApp.Persistancy;
using StationLogApp.ViewModel;

namespace StationLogApp.Handlers
{
    public class TaskHandler
    {
        
        private ISave<TaskClass> _savedTaskClass = new SaveM<TaskClass>();
        private TaskVm _taskVm;

        public TaskClass SelectedTask
        {
            get { return _taskVm.SelectedTaskClass;}
        }

        public TaskHandler(TaskVm taskVm)
        {
            _taskVm = taskVm;
        }

        public static ObservableCollection<TaskClass> LoadCatalog()
        {
            LoadM<TaskClass> retrievedCatalog = new LoadM<TaskClass>();
            Task<ObservableCollection<TaskClass>> sth = retrievedCatalog.Load("Tasks");
            ObservableCollection<TaskClass> col = sth.Result;
            return col;
        }

        public void SaveTaskClass()
        {
            DateTime loggedDate = DateTime.Now;

            TaskClass becomeDoneTask = new TaskClass(
                _taskVm.SelectedTaskClass.TaskId, 
                _taskVm.SelectedTaskClass.TaskName, 
                _taskVm.SelectedTaskClass.TaskSchedule, 
                _taskVm.SelectedTaskClass.Registration, 
                _taskVm.SelectedTaskClass.TaskType, 
                loggedDate, 
                _taskVm.SelectedTaskClass.Comment, 
                _taskVm.SelectedTaskClass.DoneVar, 
                _taskVm.SelectedTaskClass.EquipmentID
                );
            
            _savedTaskClass.Save(becomeDoneTask, "Tasks");
        }

        public void ReScheduleTask()
        {
            if (_taskVm.SelectedTaskClass.TaskSchedule != null)
            {
                if (_taskVm.SelectedTaskClass.TaskSchedule == "Every week")
                {
                    DateTime today = DateTime.Today;
                    int daysUntilTuesday = ((int) DayOfWeek.Tuesday - (int) today.DayOfWeek + 7) % 7;
                    DateTime nextTuesdayDate = today.AddDays(daysUntilTuesday);


                    TaskClass newReSchedule = new TaskClass(
                        _taskVm.SelectedTaskClass.TaskId,
                        _taskVm.SelectedTaskClass.TaskName,
                        _taskVm.SelectedTaskClass.TaskSchedule,
                        _taskVm.SelectedTaskClass.Registration,
                        _taskVm.SelectedTaskClass.TaskType,
                        nextTuesdayDate,
                        _taskVm.SelectedTaskClass.Comment,
                        _taskVm.SelectedTaskClass.DoneVar,
                        _taskVm.SelectedTaskClass.EquipmentID
                    );
                }

                else if (_taskVm.SelectedTaskClass.TaskSchedule == "Every two weeks")
                {
                    DateTime today = DateTime.Today;
                    int daysUntilThreeWeeksTuesday = ((int) DayOfWeek.Tuesday - (int) today.DayOfWeek + 14) % 14;
                    DateTime nextTuesdayDate = today.AddDays(daysUntilThreeWeeksTuesday);

                    TaskClass twoWeeksReSchedule = new TaskClass(
                        _taskVm.SelectedTaskClass.TaskId,
                        _taskVm.SelectedTaskClass.TaskName,
                        _taskVm.SelectedTaskClass.TaskSchedule,
                        _taskVm.SelectedTaskClass.Registration,
                        _taskVm.SelectedTaskClass.TaskType,
                        nextTuesdayDate,
                        _taskVm.SelectedTaskClass.Comment,
                        _taskVm.SelectedTaskClass.DoneVar,
                        _taskVm.SelectedTaskClass.EquipmentID
                    );
                }

                else if (_taskVm.SelectedTaskClass.TaskSchedule == "Every three weeks")
                {
                    DateTime today = DateTime.Today;
                    int daysUntilNextMonthlyTuesday = ((int)DayOfWeek.Tuesday - (int)today.DayOfWeek + 21) % 21;
                    DateTime nextTuesdayDate = today.AddDays(daysUntilNextMonthlyTuesday);

                    TaskClass monthReSchedule = new TaskClass(
                        _taskVm.SelectedTaskClass.TaskId,
                        _taskVm.SelectedTaskClass.TaskName,
                        _taskVm.SelectedTaskClass.TaskSchedule,
                        _taskVm.SelectedTaskClass.Registration,
                        _taskVm.SelectedTaskClass.TaskType,
                        nextTuesdayDate,
                        _taskVm.SelectedTaskClass.Comment,
                        _taskVm.SelectedTaskClass.DoneVar,
                        _taskVm.SelectedTaskClass.EquipmentID
                    );
                }

                else if (_taskVm.SelectedTaskClass.TaskSchedule == "Every month")
                {
                    DateTime today = DateTime.Today;
                    int daysUntilNextMonthlyTuesday = ((int)DayOfWeek.Tuesday - (int)today.DayOfWeek + 28) % 28;
                    DateTime nextTuesdayDate = today.AddDays(daysUntilNextMonthlyTuesday);

                    TaskClass monthReSchedule = new TaskClass(
                        _taskVm.SelectedTaskClass.TaskId,
                        _taskVm.SelectedTaskClass.TaskName,
                        _taskVm.SelectedTaskClass.TaskSchedule,
                        _taskVm.SelectedTaskClass.Registration,
                        _taskVm.SelectedTaskClass.TaskType,
                        nextTuesdayDate,
                        _taskVm.SelectedTaskClass.Comment,
                        _taskVm.SelectedTaskClass.DoneVar,
                        _taskVm.SelectedTaskClass.EquipmentID
                    );
                }

                else if (_taskVm.SelectedTaskClass.TaskSchedule == "Every two month")
                {
                    DateTime today = DateTime.Today;
                    int daysUntilNextTwoMonthlyTuesday = ((int)DayOfWeek.Tuesday - (int)today.DayOfWeek +56) % 56;
                    DateTime nextTuesdayDate = today.AddDays(daysUntilNextTwoMonthlyTuesday);

                    TaskClass monthReSchedule = new TaskClass(
                        _taskVm.SelectedTaskClass.TaskId,
                        _taskVm.SelectedTaskClass.TaskName,
                        _taskVm.SelectedTaskClass.TaskSchedule,
                        _taskVm.SelectedTaskClass.Registration,
                        _taskVm.SelectedTaskClass.TaskType,
                        nextTuesdayDate,
                        _taskVm.SelectedTaskClass.Comment,
                        _taskVm.SelectedTaskClass.DoneVar,
                        _taskVm.SelectedTaskClass.EquipmentID
                    );
                }

                else if (_taskVm.SelectedTaskClass.TaskSchedule == "Every two month")
                {
                    DateTime today = DateTime.Today;
                    int daysUntilNextTwoMonthlyTuesday = ((int)DayOfWeek.Tuesday - (int)today.DayOfWeek + 84) % 84;
                    DateTime nextTuesdayDate = today.AddDays(daysUntilNextTwoMonthlyTuesday);

                    TaskClass monthReSchedule = new TaskClass(
                        _taskVm.SelectedTaskClass.TaskId,
                        _taskVm.SelectedTaskClass.TaskName,
                        _taskVm.SelectedTaskClass.TaskSchedule,
                        _taskVm.SelectedTaskClass.Registration,
                        _taskVm.SelectedTaskClass.TaskType,
                        nextTuesdayDate,
                        _taskVm.SelectedTaskClass.Comment,
                        _taskVm.SelectedTaskClass.DoneVar,
                        _taskVm.SelectedTaskClass.EquipmentID
                    );
                }
            }

        }
    }
}
