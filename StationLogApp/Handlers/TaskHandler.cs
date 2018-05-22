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

        private readonly TaskVm _taskVm;
        private readonly FrameNavigateClass _frameNavigation;
        private readonly Collections _collectionsClass;

        private ObservableCollection<TaskEquipmentStation> _loadedCollection;
        #endregion

        #region properties
        public TaskClass SelectedTask => _taskVm.SelectedItem;

        public string SelectedPeriodicityItem { get; set; }
        public Station SelectedStation { get; set; }

        public ObservableCollection<TaskEquipmentStation> LoadedCollection
        {
            get => _loadedCollection;
            set
            {
                _loadedCollection = value;
                OnPropertyChanged(nameof(LoadedCollection));
            }
        }
        #endregion

        #region Constructor
        public TaskHandler(TaskVm taskVm)
        {
            _taskVm = taskVm;
            _frameNavigation = new FrameNavigateClass();
            _collectionsClass = new Collections();
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
                    _taskVm.SelectedItem.TaskId,
                    _taskVm.SelectedItem.TaskName,
                    _taskVm.SelectedItem.TaskSchedule,
                    _taskVm.SelectedItem.Registration,
                    _taskVm.SelectedItem.TaskType,
                    _taskVm.SelectedItem.DueDate,
                    newDate,
                    _taskVm.SelectedItem.Comment,
                    doneVariable,
                    _taskVm.SelectedItem.EquipmentId
                );

                _savedTaskClass.Save(newReSchedule, "Tasks");

                ReScheduleTask();

                _frameNavigation.ActivateFrameNavigation(typeof(TaskPage));

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
            var nextTuesdayDate = GetNextDate(periodicity);
            var newReSchedule = CreateReScheduledTask(nextTuesdayDate, "N");
            _updateTaskClass.Update(newReSchedule, "Tasks", _taskVm.SelectedItem.TaskId);
        }
        
        private DateTime GetNextDate(int periodicity)
        {
            var today = DateTime.Today;
            var nextTuesdayDate = today.AddDays(periodicity + 1);
            return nextTuesdayDate;
        }

        private TaskClass CreateReScheduledTask(DateTime newDate, string doneVariable)
        {
            var newReSchedule = new TaskClass(
                _taskVm.SelectedItem.TaskId,
                _taskVm.SelectedItem.TaskName,
                _taskVm.SelectedItem.TaskSchedule,
                _taskVm.SelectedItem.Registration = null,
                _taskVm.SelectedItem.TaskType,
                newDate,
                null,
                _taskVm.SelectedItem.Comment = null,
                doneVariable,
                _taskVm.SelectedItem.EquipmentId
            );
            return newReSchedule;
        }
        #endregion

        #region SortingMethods
        public async void SortCollection()
        {
            if (_taskVm.SelectedItemStation != null || _taskVm.SelectedItemPeriodicity != null)
            {
                var newLoadedCollection = new ObservableCollection<TaskEquipmentStation>();
                newLoadedCollection.Clear();

                if (_taskVm.SelectedItemStation != null)
                {
                    foreach (var item in _collectionsClass.LoadToDo())
                    {
                        if (item.StationName == _taskVm.SelectedItemStation.StationName)
                        {
                            newLoadedCollection.Add(item);
                        }
                    }
                }
                if (_taskVm.SelectedItemPeriodicity == null)
                {
                    _loadedCollection = newLoadedCollection;
                    _taskVm.TaskCatalog = _loadedCollection;
                }
                if (_taskVm.SelectedItemPeriodicity == null) return;
                {
                    var newList = new ObservableCollection<TaskEquipmentStation>();

                    if (newLoadedCollection.Count == 0)
                    {
                        foreach (var item in _collectionsClass.LoadToDo())
                        {
                            if (item.TaskSchedule == _taskVm.SelectedItemPeriodicity)
                            {
                                newLoadedCollection.Add(item);
                            }
                        }
                        _loadedCollection = newLoadedCollection;
                        _taskVm.TaskCatalog = _loadedCollection;
                    }
                    else
                    {
                        foreach (var item in newLoadedCollection)
                        {
                            if (item.TaskSchedule == _taskVm.SelectedItemPeriodicity)
                            {
                                newList.Add(item);
                            }
                        }
                        _loadedCollection = newList;
                        _taskVm.TaskCatalog = _loadedCollection;
                    }
                }
                if (_taskVm.TaskCatalog.Count == 0)
                {
                    var msg = new MessageDialog("No such object.");
                    await msg.ShowAsync();
                }
            }
            else
            {
                var msg = new MessageDialog("In order to sort please pick a filter.");
                await msg.ShowAsync();
            }
        }
        #endregion

        public ObservableCollection<TaskEquipmentStation> LoadCollection()
        {
            _loadedCollection = _collectionsClass.LoadToDo();
            return _loadedCollection;
        }
    }
}