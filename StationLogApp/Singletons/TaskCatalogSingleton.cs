using System.Collections.ObjectModel;
using StationLogApp.Factories;

namespace StationLogApp.Singletons
{
    public class TaskCatalogSingleton
    {

        private static TaskCatalogSingleton _instance;


        // List of Task 
        public ObservableCollection<MainFactory> TaskList { get; set;}


        // Constructor of the Singleton

        private TaskCatalogSingleton()
        {
            TaskList = new ObservableCollection<MainFactory>();
            TaskList = new ObservableCollection<MainFactory>(new Facade.GetTaskList());
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
