using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using StationLogApp.Annotations;
using StationLogApp.Common;
using StationLogApp.Handlers;
using StationLogApp.Interfaces;
using StationLogApp.Model;
using StationLogApp.Persistancy;
using StationLogApp.Singletons;
using StationLogApp.View;

namespace StationLogApp.ViewModel
{
    public class TaskVm : NotifyPropertyChangedClass
    {
        #region instancefields

        private readonly TaskCatalogSingleton _catalogSingleton;

        private TaskEquipmentStation _selectedItem;
        #endregion 

        #region properties
        public ObservableCollection<TaskEquipmentStation> TaskCatalog
        {
            get
            {
                return _catalogSingleton.TaskCatalog;
            }
            set
            {
                _catalogSingleton.TaskCatalog = value;
                OnPropertyChanged(nameof(TaskCatalog));
            } 
        }

        public ObservableCollection<TaskEquipmentStation> DoneCatalog
        {
            get
            {
                return _catalogSingleton.DoneCatalog;
            }
            set
            {
                _catalogSingleton.DoneCatalog = value;
                OnPropertyChanged(nameof(DoneCatalog));
            }
        }

        public TaskEquipmentStation SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public RelayCommandClass SaveTaskClass { get; set; }
        public RelayCommandClass DoInfo { get; set; }

        public TaskHandler TaskHandler { get; set; }
        #endregion

        #region constructor
        public TaskVm()
        {
            _catalogSingleton = TaskCatalogSingleton.Instance;
            TaskHandler = new TaskHandler(this);
            SaveTaskClass = new RelayCommandClass(TaskHandler.OperateTask);
            
            DoInfo = new RelayCommandClass(Info);

            _selectedItem = new TaskEquipmentStation();
        }
        #endregion

        public async void Info()
        {
            if (SelectedItem.TaskId != 0)
            {
                string info =
                    $"Equipment name: {SelectedItem.EquipmentName} \nTaskId: {SelectedItem.TaskId} \nEquipmentId: {SelectedItem.EquipmentID} \nTask Schedule: {SelectedItem.TaskSchedule} \nStation name: {SelectedItem.StationName}";
                MessageDialog msg = new MessageDialog(info, "More Information");
                await msg.ShowAsync();
            }
            else
            {
                MessageDialog msg = new MessageDialog("Select item to see the details");
                await msg.ShowAsync();
            }
        }
    }
}
