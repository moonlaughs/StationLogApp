using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationLogApp.Model
{
    public class Equipment
    {
        #region // properties
        public int EquipmentID { get; set; }
        public string EquipmentName { get; set; }
        public string EquipmentType { get; set; }
        public int StationID { get; set; }
        #endregion

        #region // constructors
        public Equipment(int equipmentID, string equipmentName, string equipmentType, int stationID)
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

        #region // ToStringMethod

        public override string ToString()
        {
            return $"{EquipmentID}, {EquipmentName}, {EquipmentType}, {StationID}";
        }

        #endregion
    }
}
