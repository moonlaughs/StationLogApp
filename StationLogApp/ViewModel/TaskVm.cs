using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StationLogApp.Common;
using StationLogApp.Factories;
using StationLogApp.Interfaces;
using StationLogApp.Persistancy;
using StationLogApp.View;

namespace StationLogApp.ViewModel
{
    public class TaskVm : NotifyPropertyChangedClass
    {
        #region instancefields
        private IMainFactory _newTask;
        private IMainFactory _selectedItem;
        private ObservableCollection<IMainFactory> _collection;
        private FrameNavigateClass _frameNavigation;
        #endregion

        #region properties

        public IMainFactory taskobj = new MainFactory();

        public IMainFactory NewTask
        {
            get { return _newTask; }
            set { _newTask = value;
                OnPropertyChanged(nameof(NewTask));
            }
        }
        
        public IMainFactory SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public ObservableCollection<IMainFactory> Catalog
        {
            get { return _collection; }
            set { _collection = value; OnPropertyChanged(nameof(Catalog)); }
        }

        ILoad<Collection<IMainFactory>> load = new LoadM<Collection<IMainFactory>>();

        public RelayCommandClass LoadCommand { get; set; }
        #endregion

        #region constructor
        public TaskVm()
        {
            LoadCommand = new RelayCommandClass(LoadMethod);
            _frameNavigation = new FrameNavigateClass();
        }
        #endregion

        #region Methods
        public void LoadMethod()
        {
            _frameNavigation.ActivateFrameNavigation(typeof(TaskPage));
            load.Load("Tasks");
        }
        #endregion
    }
}
