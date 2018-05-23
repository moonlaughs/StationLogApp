using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationLogApp.Model
{
    public class Station
    {
        #region // Properties
        public int StationId { get; set; }
        public string StationName { get; set; }
        public string StationAddress { get; set; }
        #endregion

        #region // Constructors 
        public Station(int stationId, string stationName, string stationAddress)
        {
            StationId = stationId;
            StationName = stationName;
            StationAddress = stationAddress;
        }

        public Station()
        {
            
        }
        #endregion

        #region // ToString() method
        public override string ToString()
        {
            return $"{StationId}, {StationName}, {StationAddress}";
        }
        #endregion
    }
}