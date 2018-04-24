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
        public int StationID { get; set; }
        public string StationName { get; set; }
        public string StationAddress { get; set; }
        #endregion
        #region // Constructors 
        public Station(int stationID, string stationName, string stationAddress)
        {
            StationID = stationID;
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
            return $"{StationID}, {StationName}, {StationAddress}";
        }
        #endregion
    }
}
