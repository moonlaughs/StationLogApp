using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StationLogApp.Interfaces;
using StationLogApp.Model;
using StationLogApp.Persistancy;

namespace StationLogApp.Common
{
    public class Collections
    {
        public ObservableCollection<TaskEquipmentStation> LoadToDo()
        {
            ObservableCollection<TaskEquipmentStation> ltes = new ObservableCollection<TaskEquipmentStation>();

            ILoad<TaskClass> retrivedTask = new LoadM<TaskClass>();
            ObservableCollection<TaskClass> taskCollection = retrivedTask.RetriveCollection("Tasks");

            ILoad<Station> retrivedStation = new LoadM<Station>();
            ObservableCollection<Station> stationCollection = retrivedStation.RetriveCollection("Stations");

            ILoad<Equipment> retrivedEquipmnet = new LoadM<Equipment>();
            ObservableCollection<Equipment> equipmentCollection = retrivedEquipmnet.RetriveCollection("Equipments");

            var query = (from t in taskCollection
                join e in equipmentCollection on t.EquipmentID equals e.EquipmentID
                join s in stationCollection on e.StationID equals s.StationID
                select new TaskEquipmentStation() { TaskId = t.TaskId, TaskName = t.TaskName, TaskSchedule = t.TaskSchedule, Registration = t.Registration, DueDate = t.DueDate, DoneDate = t.DoneDate, DoneVar = t.DoneVar, TaskType = t.TaskType, Comment = t.Comment, EquipmentID = t.EquipmentID, EquipmentName = e.EquipmentName, StationName = s.StationName }).ToList();

            foreach (var item in query)
            {
                if (item.DoneVar == "N")
                {
                    ltes.Add(item);
                }
            }

            return ltes;
        }

        public ObservableCollection<TaskEquipmentStation> LoadDone()
        {
            ObservableCollection<TaskEquipmentStation> done = new ObservableCollection<TaskEquipmentStation>();

            ILoad<TaskClass> retrivedTask = new LoadM<TaskClass>();
            ObservableCollection<TaskClass> taskCollection = retrivedTask.RetriveCollection("Tasks");

            ILoad<Station> retrivedStation = new LoadM<Station>();
            ObservableCollection<Station> stationCollection = retrivedStation.RetriveCollection("Stations");

            ILoad<Equipment> retrivedEquipmnet = new LoadM<Equipment>();
            ObservableCollection<Equipment> equipmentCollection = retrivedEquipmnet.RetriveCollection("Equipments");

            ILoad<User> retrivedUser = new LoadM<User>();
            ObservableCollection<User> userCollection = retrivedUser.RetriveCollection("Users");

            var query = (from t in taskCollection
                join e in equipmentCollection on t.EquipmentID equals e.EquipmentID
                join s in stationCollection on e.StationID equals s.StationID
                select new TaskEquipmentStation() { TaskId = t.TaskId, TaskName = t.TaskName, TaskSchedule = t.TaskSchedule, Registration = t.Registration, DueDate = t.DueDate, DoneDate = t.DoneDate, DoneVar = t.DoneVar, TaskType = t.TaskType, Comment = t.Comment, EquipmentID = t.EquipmentID, EquipmentName = e.EquipmentName, StationName = s.StationName }).ToList();

            foreach (var item in query)
            {
                if (item.DoneVar == "Y")
                {
                    done.Add(item);
                }
            }

            return done;
        }

        public ObservableCollection<TaskEquipmentStation> EquipmentStationsCollection()
        {
            ObservableCollection<TaskEquipmentStation> list = new ObservableCollection<TaskEquipmentStation>();

            ILoad<Equipment> retrievedEquipments = new LoadM<Equipment>();
            ObservableCollection<Equipment> equipmentsCollection = retrievedEquipments.RetriveCollection("Equipments");

            ILoad<Station> retrivedStations = new LoadM<Station>();
            ObservableCollection<Station> stationsCollection = retrivedStations.RetriveCollection("Stations");

            var query = (from e in equipmentsCollection
                join s in stationsCollection on e.StationID equals s.StationID
                select new TaskEquipmentStation() { EquipmentID = e.EquipmentID, EquipmentName = e.EquipmentName, StationName = s.StationName }).ToList();

            foreach (var item in query)
            {
                list.Add(item);
            }

            return list;
        }

        public string[] TypeArray = new string[] { "check", "register" };

        public string[] ScheduleArray = new string[] { "Every week", "Every two weeks", "Every three weeks", "Every month", "Every two months", "Every three months", "Every six months", "Every year" };


        #region
        //public ObservableCollection<TaskEquipmentStation> LoadCollection()
        //{
        //    _newLoadedCollection = new ObservableCollection<TaskEquipmentStation>();

        //    _newLoadedCollection.Clear();


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

        //    foreach (var item in query)
        //    {
        //        _newLoadedCollection.Add(item);
        //    }

        //    _loadedCollection = _newLoadedCollection;
        //    return _loadedCollection;
        //}

        public ObservableCollection<Station> LoadStation()
        {
            ILoad<Station> retrivedStation = new LoadM<Station>();
            ObservableCollection<Station> stationCollection = retrivedStation.RetriveCollection("Stations");

            var query = (from s in stationCollection
                         select new Station() { StationName = s.StationName, StationID = s.StationID }).ToList();

            foreach (var item in query)
            {
                stationCollection.Add(item);
            }
            return stationCollection;
        }

        public ObservableCollection<Notes> LoadNotes()
        {

            ObservableCollection<Notes> list = new ObservableCollection<Notes>();

            ILoad<Notes> loadedNotes = new LoadM<Notes>();
            ObservableCollection<Notes> notesCollection = loadedNotes.RetriveCollection("Notes");

            ILoad<Station> retrivedStation = new LoadM<Station>();
            ObservableCollection<Station> stationCollection = retrivedStation.RetriveCollection("Stations");

            var query = (from n in notesCollection
                join s in stationCollection on n.StationID equals s.StationID
                select new Notes()
                {
                    NotesID = n.NotesID,
                    Note1 = n.Note1,
                    UserID = n.UserID,
                    DueDate = n.DueDate,
                    StationName = s.StationName,
                    StationID = s.StationID
                }).ToList();


            foreach (var item in query)
            {
                list.Add(item);
            }

            return list;
        }
        #endregion

    }
}
