using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationLogApp.Model
{
    public class Equipment
    {
        #region properties
        public string EquipmentID { get; set; }
        public string EquipmentName { get; set; }
        public string EquipmentType { get; set; }
        public string StationID { get; set; }
        #endregion

        #region constructors
        public Equipment(string equipmentID, string equipmentName, string equipmentType, string stationID)
        {
            EquipmentID = equipmentID;
            EquipmentName = equipmentName;
            EquipmentType = equipmentType;
            StationID = stationID;
        }

        public Equipment()
        {
            
        }
        #endregion

        #region ToStringMethod

        public override string ToString()
        {
            return $"{EquipmentID}, {EquipmentName}, {EquipmentType}, {StationID}";
        }

        #endregion
    }
}
