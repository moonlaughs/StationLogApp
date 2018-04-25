using System.Collections.ObjectModel;
using StationLogApp.Factories;
using StationLogApp.Interfaces;
using StationLogApp.Persistancy;

namespace StationLogApp.Singletons
{
    public class TaskCatalogSingleton
    {

        private static TaskCatalogSingleton _instance;


        // List of Task 
        public ObservableCollection<MainFactory> TaskList { get; set;}
        public ICreate<MainFactory> TaskObject = new CreateM<MainFactory>();


        // Constructor of the Singleton

        private TaskCatalogSingleton()
        {
            TaskList = new ObservableCollection<MainFactory>();
            // TaskList = new ObservableCollection<MainFactory>(TaskObject.Create());
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
