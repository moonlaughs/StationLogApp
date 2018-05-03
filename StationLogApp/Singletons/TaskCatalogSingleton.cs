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
       

        // Properties

        public ObservableCollection<TaskClass> TaskCatalog {get; set; }

        // Constructor of the Singleton

        private TaskCatalogSingleton()
        {
            TaskCatalog = LoadCatalog();

        }
       

        public static ObservableCollection<TaskClass> LoadCatalog()
        {
            LoadTask<TaskClass> retrievedCatalog = new LoadTask<TaskClass>();
            Task<ObservableCollection<TaskClass>> sth = retrievedCatalog.Load("Tasks");
            ObservableCollection<TaskClass> col = sth.Result;

            //LoadM<Station> retrivedStationCatalog = new LoadM<Station>();
            //Task<ObservableCollection<Station>> st = retrivedStationCatalog.Load("Stations");
            //ObservableCollection<Station> stations = st.Result;

            //LoadM<Equipment> retrivedEquipment = new LoadM<Equipment>();
            //Task<ObservableCollection<Equipment>> eq = retrivedEquipment.Load("Equipments");
            //ObservableCollection<Equipment> equipments = eq.Result;
            return col;
            //var query = from task in col
            //            join e in equipments
            //            on task.

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
