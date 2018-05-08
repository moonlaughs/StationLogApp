using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StationLogApp.Model;

namespace StationLogApp.Persistancy
{
    public class LoadJoinedColections
    {
        public static ObservableCollection<string> LoadCatalog()
        {
            LoadM<TaskClass> retrivedTask = new LoadM<TaskClass>();
            Task<ObservableCollection<TaskClass>> api = retrivedTask.Load("Tasks");
            ObservableCollection<TaskClass> taskCollection = api.Result;
            
            LoadM<Station> retrivedStation = new LoadM<Station>();
            Task<ObservableCollection<Station>> api2 = retrivedStation.Load("Stations");
            ObservableCollection<Station> stationCollection = api2.Result;

            LoadM<Equipment> retrivedEquipment = new LoadM<Equipment>();
            Task<ObservableCollection<Equipment>> api3 = retrivedEquipment.Load("Equipments");
            ObservableCollection<Equipment> equipmentCollection = api3.Result;

            ObservableCollection<string> query = (ObservableCollection<string>) (from t in taskCollection
                join e in equipmentCollection on t.EquipmentID equals e.EquipmentID
                join s in stationCollection on e.StationID equals s.StationID
                select new {t, e, s});

            return query;
        }
    }
}
