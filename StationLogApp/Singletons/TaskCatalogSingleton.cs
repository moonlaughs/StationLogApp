using System.Collections.ObjectModel;
using System.Linq;
using StationLogApp.Factories;
using StationLogApp.Interfaces;
using StationLogApp.Persistancy;

namespace StationLogApp.Singletons
{
    public class TaskCatalogSingleton
    {

        private static TaskCatalogSingleton _instance;
        

        // List of Task

        public ObservableCollection<ITaskFactory> TaskCatalog { get; set; }


        // Constructor of the Singleton

        private TaskCatalogSingleton()
        {
            TaskCatalog = new ObservableCollection<ITaskFactory>();
            ILoad<ITaskFactory> retrievedCatalog = new LoadM<ITaskFactory>();
            TaskCatalog = new ObservableCollection<ITaskFactory>(retrievedCatalog.Load()); 
        }
        

        // Singleton Method

        public static TaskCatalogSingleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TaskCatalogSingleton();
                }
                return _instance;
            }
        }
    }
}
