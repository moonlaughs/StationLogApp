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
        public int EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public string EquipmentType { get; set; }
        public int StationId { get; set; }
        #endregion

        #region // constructors
        public Equipment(int equipmentId, string equipmentName, string equipmentType, int stationId)
        {
            EquipmentId = equipmentId;
            EquipmentName = equipmentName;
            EquipmentType = equipmentType;
            StationId = stationId;
        }

        public Equipment()
        {
            
        }
        #endregion

        #region // ToStringMethod

        public override string ToString()
        {
            return $"{EquipmentId}, {EquipmentName}, {EquipmentType}, {StationId}";
        }
        #endregion
    }
}