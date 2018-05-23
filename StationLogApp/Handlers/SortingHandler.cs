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
        private readonly TaskHandler _taskHandler;
        private readonly Collections _collectionsClass = new Collections();
        private ObservableCollection<TaskEquipmentStation> _loadedCollection;
        
        public ObservableCollection<TaskEquipmentStation> LoadedCollection
        {
            get => _loadedCollection;
            set => _loadedCollection = value;
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
            else
            {
                var msg = new MessageDialog("In order to sort please pick a filter.");
                await msg.ShowAsync();
            }

            _loadedCollection = newLoadedCollection;

            if (_loadedCollection.Count == 0)
            {
                var msg = new MessageDialog("No task found");
                await msg.ShowAsync();
            }
            else
            {
                _taskHandler.TaskVm.TaskCatalog = _loadedCollection;
            }
        }
    }
}