using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationLogApp.Model
{
    public class TaskEquipmentStation : TaskClass
    {
        public string EquipmentName { get; set; }
        public string StationName { get; set; }
        public string UserName { get; set; }

        public TaskEquipmentStation(string equipmentName, string stationName, string userName)
        {
            EquipmentName = equipmentName;
            StationName = stationName;
            UserName = userName;
        }

        public TaskEquipmentStation()
        {
            
        }
        
        public override string ToString()
        {
            return $"{EquipmentName}, {StationName}, {UserName}";
        }
    }
}
