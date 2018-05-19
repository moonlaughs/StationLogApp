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

        public TaskEquipmentStation(string equipmentName, string stationName)
        {
            EquipmentName = equipmentName;
            StationName = stationName;
        }

        public TaskEquipmentStation()
        {
            
        }
        
        public override string ToString()
        {
            return $"{EquipmentName}, {StationName}";
        }
    }
}