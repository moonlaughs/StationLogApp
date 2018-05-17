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
        private Collections _collectionsClass = new Collections();                            //to change?


        // Collections for Sorting Purposes

        //private ObservableCollection<TaskEquipmentStation> _newLoadedCollection;            //think

        //private ObservableCollection<TaskEquipmentStation> _loadedCollection;
        #endregion

        #region properties
        public TaskClass SelectedTask
        {
            get { return _taskVm.SelectedItem; }
        }

        //public string[] PeriodicityItems             //????????????????
        //{
        //    get { return _collectionsClass.ScheduleArray; }
        //    set => _collectionsClass.ScheduleArray = value;
        //}

        public string SelectedPeriodicityItem { get; set; }
        public Station SelectedStation { get; set; }

        //public ObservableCollection<TaskEquipmentStation> LoadedCollection                 //?????????????
        //{
        //    get { return _loadedCollection; }
        //    set
        //    {
        //        _loadedCollection = value;
        //        OnPropertyChanged(nameof(LoadedCollection));
        //    }
        //}
        #endregion

        #region Constructor

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

        #region SortingMethods
        public void SortCollection()
        {

            ObservableCollection<TaskEquipmentStation> newLoadedCollection = new ObservableCollection<TaskEquipmentStation>();
            //newLoadedCollection.Clear();

            if (_taskVm.SelectedItemStation != null)
            {
            //    ILoad<TaskClass> retrivedTask = new LoadM<TaskClass>();
            //    ObservableCollection<TaskClass> taskCollection = retrivedTask.RetriveCollection("Tasks");

            //    ILoad<Station> retrivedStation = new LoadM<Station>();
            //    ObservableCollection<Station> stationCollection = retrivedStation.RetriveCollection("Stations");

            //    ILoad<Equipment> retrivedEquipmnet = new LoadM<Equipment>();
            //    ObservableCollection<Equipment> equipmentCollection = retrivedEquipmnet.RetriveCollection("Equipments");

            //    var query = (from t in taskCollection
            //                 join e in equipmentCollection on t.EquipmentID equals e.EquipmentID
            //                 join s in stationCollection on e.StationID equals s.StationID
            //                 select new TaskEquipmentStation() { TaskName = t.TaskName, TaskType = t.TaskType, EquipmentName = e.EquipmentName, StationName = s.StationName, TaskSchedule = t.TaskSchedule, EquipmentID = e.EquipmentID }).ToList();


                foreach (var item in _collectionsClass.LoadToDo())
                {
                    if (item.StationName == _taskVm.SelectedItemStation.StationName)
                    {
                        newLoadedCollection.Add(item);
                    }
                }

                if (_taskVm.SelectedItemPeriodicity == null)
                {
                    _taskVm.TaskCatalog = newLoadedCollection;
                }
            }
            else if (_taskVm.SelectedItemPeriodicity != null)
            {
                ObservableCollection<TaskEquipmentStation> newList = new ObservableCollection<TaskEquipmentStation>();
                //ILoad<TaskClass> retrivedTask = new LoadM<TaskClass>();
                //ObservableCollection<TaskClass> taskCollection = retrivedTask.RetriveCollection("Tasks");

                //ILoad<Station> retrivedStation = new LoadM<Station>();
                //ObservableCollection<Station> stationCollection = retrivedStation.RetriveCollection("Stations");

                //ILoad<Equipment> retrivedEquipmnet = new LoadM<Equipment>();
                //ObservableCollection<Equipment> equipmentCollection = retrivedEquipmnet.RetriveCollection("Equipments");

                //var query = (from t in taskCollection
                //             join e in equipmentCollection on t.EquipmentID equals e.EquipmentID
                //             join s in stationCollection on e.StationID equals s.StationID
                //             select new TaskEquipmentStation() { TaskName = t.TaskName, TaskType = t.TaskType, EquipmentName = e.EquipmentName, StationName = s.StationName, TaskSchedule = t.TaskSchedule, EquipmentID = e.EquipmentID }).ToList();



                if (newLoadedCollection.Count == 0)
                {
                    foreach (var item in _collectionsClass.LoadToDo())
                    {
                        if (item.TaskSchedule == _taskVm.SelectedItemPeriodicity)
                        {
                            newLoadedCollection.Add(item);
                        }
                    }
                    _taskVm.TaskCatalog = newLoadedCollection;
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
                    _taskVm.TaskCatalog = newList;
                }
            }
            else if(_taskVm.SelectedItemStation == null && _taskVm.SelectedItemPeriodicity == null)
            {
                _taskVm.TaskCatalog = _collectionsClass.LoadToDo();
            }
            
        }

        #endregion
    }
}
