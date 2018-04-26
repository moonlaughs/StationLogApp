using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StationLogApp.Common;
using StationLogApp.Factories;
using StationLogApp.Interfaces;
using StationLogApp.Singletons;

namespace StationLogApp.ViewModel
{
    public class TaskVm : NotifyPropertyChangedClass
    {
        #region instancefields
        private MainFactory _newTask;
        private MainFactory _selectedItem;
        private ObservableCollection<MainFactory> _collection;
        private TaskCatalogSingleton _taskCatalogSingleton;
        private ObservableCollection<ITaskFactory> _taskCatalog;
        #endregion

        #region properties
        // ILoad<Collection<>>

        public ObservableCollection<ITaskFactory> TaskCatalog { get; set; }

        public MainFactory NewTask
        {
            get { return _newTask; }
            set { _newTask = value;
                OnPropertyChanged(nameof(NewTask));
            }
        }
        
        public MainFactory SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public ObservableCollection<MainFactory> Collection
        {
            get { return _collection; }
            set { _collection = value; OnPropertyChanged(nameof(Collection)); }
        }
        #endregion

        #region constructor
        public TaskVm()
        {
            _taskCatalogSingleton = TaskCatalogSingleton.Instance; 
        }
        #endregion
    }
}
