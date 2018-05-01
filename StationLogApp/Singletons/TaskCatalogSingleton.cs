using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using StationLogApp.Factories;
using StationLogApp.Interfaces;
using StationLogApp.Model;
using StationLogApp.Persistancy;

namespace StationLogApp.Singletons
{
    public class TaskCatalogSingleton
    {


        private static TaskCatalogSingleton _instance;
        private static ObservableCollection<ITaskFactory> _taskCatalog;

        // List of Task
        

        // Constructor of the Singleton

        private TaskCatalogSingleton()
        {
           _taskCatalog = new ObservableCollection<TaskClass>();
        }


        public static async void LoadCatalog()
        {
            ILoad<TaskClass> retrievedCatalog = new LoadTask<TaskClass>();
            Task<ObservableCollection<TaskClass>> sth = retrievedCatalog.Load();
            await sth;
            ObservableCollection<TaskClass> col = sth.Result;
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
