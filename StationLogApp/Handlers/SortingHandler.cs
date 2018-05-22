using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using StationLogApp.Common;
using StationLogApp.Model;

namespace StationLogApp.Handlers
{
    public class SortingHandler
    {
        private TaskHandler _taskHandler;
        private Collections _collectionsClass = new Collections();
        private ObservableCollection<TaskEquipmentStation> _loadedCollection ;


        public ObservableCollection<TaskEquipmentStation> LoadedCollection
        {
            get { return _loadedCollection; }
            set { _loadedCollection = value; }
        }
        

        public SortingHandler(TaskHandler taskHandler)
        {
            _taskHandler = taskHandler;
        }


        public async void SortCollection()
        {
            var newLoadedCollection = new ObservableCollection<TaskEquipmentStation>();

            if (_taskHandler.SelectedStation != null && _taskHandler.SelectedPeriodicityItem != null)
            {
                foreach (var item in _collectionsClass.LoadToDo())
                {
                    if (item.StationName == _taskHandler.SelectedStation.StationName && item.TaskSchedule == _taskHandler.SelectedPeriodicityItem)
                    {
                        newLoadedCollection.Add(item);
                    }
                }
            }

            else if (_taskHandler.SelectedStation != null && _taskHandler.SelectedPeriodicityItem == null)
            {
                foreach (var item in _collectionsClass.LoadToDo())
                {
                    if (item.StationName == _taskHandler.SelectedStation.StationName)
                    {
                        newLoadedCollection.Add(item);
                    }
                }
            }

            else if (_taskHandler.SelectedStation == null && _taskHandler.SelectedPeriodicityItem != null)
            {
                foreach (var item in _collectionsClass.LoadToDo())
                {
                    if (item.TaskSchedule == _taskHandler.SelectedPeriodicityItem)
                    {
                        newLoadedCollection.Add(item);
                    }
                }
            }

            //else if (_taskHandler.LoadedCollection.Count == 0)
            //{
            //        var msg = new MessageDialog("Not such Object");
            //        await msg.ShowAsync();
            //}

            else
            {
                var msg = new MessageDialog("In order to sort please pick a filter.");
                await msg.ShowAsync();
            }

            _loadedCollection = newLoadedCollection;

            if (_loadedCollection.Count == 0)
            {
                var msg = new MessageDialog("Not task found");
                await msg.ShowAsync();
            }

            else
            {
                _taskHandler.TaskVm.TaskCatalog = _loadedCollection;
            }
            
        }


        //public async void SortCollection()
        //{
        //    if (_taskHandler.SelectedStation != null || _taskHandler.SelectedPeriodicityItem != null)
        //    {
        //        var newLoadedCollection = new ObservableCollection<TaskEquipmentStation>();
        //        newLoadedCollection.Clear();

        //        if (_taskHandler.SelectedStation != null)
        //        {
        //            foreach (var item in _collectionsClass.LoadToDo())
        //            {
        //                if (item.StationName == _taskHandler.SelectedStation.StationName)
        //                {
        //                    newLoadedCollection.Add(item);
        //                }
        //            }
        //        }

        //        if (_taskHandler.SelectedPeriodicityItem == null)
        //        {
        //            _loadedCollection = newLoadedCollection;
        //            _taskHandler.LoadedCollection = _loadedCollection;
        //        }

        //        if (_taskHandler.SelectedPeriodicityItem == null) return;
        //        {
        //            var newList = new ObservableCollection<TaskEquipmentStation>();

        //            if (newLoadedCollection.Count == 0)
        //            {
        //                foreach (var item in _collectionsClass.LoadToDo())
        //                {
        //                    if (item.TaskSchedule == _taskHandler.SelectedPeriodicityItem)
        //                    {
        //                        newLoadedCollection.Add(item);
        //                    }
        //                }
        //                _loadedCollection = newLoadedCollection;
        //                _taskHandler.LoadedCollection = _loadedCollection;
        //            }
        //            else
        //            {
        //                foreach (var item in newLoadedCollection)
        //                {
        //                    if (item.TaskSchedule == _taskHandler.SelectedPeriodicityItem)
        //                    {
        //                        newList.Add(item);
        //                    }
        //                }
        //                _loadedCollection = newList;
        //                _taskHandler.LoadedCollection = _loadedCollection;
        //            }
        //        }
        //        if (_taskHandler.LoadedCollection.Count == 0)
        //        {
        //            var msg = new MessageDialog("No such object.");
        //            await msg.ShowAsync();
        //        }
        //    }
        //    else
        //    {
        //        var msg = new MessageDialog("In order to sort please pick a filter.");
        //        await msg.ShowAsync();
        //    }
        //}
    }
}
