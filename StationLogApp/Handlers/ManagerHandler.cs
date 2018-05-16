﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using StationLogApp.Interfaces;
using StationLogApp.Model;
using StationLogApp.Persistancy;
using StationLogApp.ViewModel;

namespace StationLogApp.Handlers
{
    public class ManagerHandler
    {
        private CrudVM _crudVm;
        private ISave<TaskClass> _savedTaskClass = new SaveM<TaskClass>();
        private IUpdate<TaskClass> _updateTaskClass = new UpdateM<TaskClass>();

        public ManagerHandler(CrudVM crudVm)
        {
            _crudVm = crudVm;
        }

        public async void CreateTask()
        {
            if (_crudVm.NewItem.TaskName != null)
            {
                await _savedTaskClass.Save(new TaskClass(
                    _crudVm.NewItem.TaskId,
                    _crudVm.NewItem.TaskName,
                    _crudVm.NewItem.TaskSchedule,
                    _crudVm.NewItem.Registration,
                    _crudVm.NewItem.TaskType,
                    _crudVm.NewItem.DueDate,
                    _crudVm.NewItem.DueDate,
                    _crudVm.NewItem.Comment,
                    _crudVm.NewItem.DoneVar = "N",
                    _crudVm.NewItem.EquipmentID), "Tasks");

                MessageDialog msg = new MessageDialog("Task created");
                await msg.ShowAsync();
            }
            else
            {
                MessageDialog msg = new MessageDialog("Please fill in the missing information");
                await msg.ShowAsync();
            }
        }

        public ObservableCollection<Station> StationCollection()
        {
            ObservableCollection<Station> list = new ObservableCollection<Station>();

            ILoad<Station> retrievedStations = new LoadM<Station>();
            ObservableCollection<Station> stationCollection = retrievedStations.RetriveCollection("Stations");

            var query = (from s in stationCollection
                select new Station(){StationAddress = s.StationAddress, StationID = s.StationID, StationName = s.StationName}).ToList();

            foreach (var item in query)
            {
                list.Add(item);
            }

            return list;
        }

        public ObservableCollection<Equipment> EquipmentsCollection()
        {
            ObservableCollection<Equipment> list = new ObservableCollection<Equipment>();

            ILoad<Equipment> retrievedEquipments = new LoadM<Equipment>();
            ObservableCollection<Equipment> equipmentsCollection = retrievedEquipments.RetriveCollection("Equipments");

            var query = (from e in equipmentsCollection
                select new Equipment() { EquipmentName = e.EquipmentName, EquipmentID = e.EquipmentID}).ToList();

            foreach (var item in query)
            {
                list.Add(item);
            }

            return list;
        }

        public string[] typeArray = new string[] { "check", "register" };

        public string[] scheduleArray = new string[]{"Every week", "Every two weeks", "Every three weeks", "Every month", "Every two months", "Every three months", "Every six months", "Every year"};
    }
}
