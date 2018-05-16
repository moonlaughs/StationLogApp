using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using StationLogApp.Common;
using StationLogApp.Handlers;
using StationLogApp.Interfaces;
using StationLogApp.Model;
using StationLogApp.Persistancy;

namespace StationLogApp.Singletons
{
    public class TaskCatalogSingleton
    {
        
        private static TaskCatalogSingleton _instance;
       

        // Properties
        public ObservableCollection<TaskEquipmentStation> TaskCatalog {get; set; }
        public ObservableCollection<TaskEquipmentStation> DoneCatalog { get; set; }

        // Constructor of the Singleton
        private TaskCatalogSingleton()
        {
            //TaskCatalog = Collections.LoadToDo();
            //DoneCatalog = Collections.LoadDone();
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
