using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationLogApp.Model
{
    public class Notes
    {
        #region // Properties
        public int NotesID { get; set; }
        public string Note1 { get; set; }
        public DateTime DueDate { get; set; }
        public int StationID { get; set; }
        public int UserID { get; set; }
        #endregion

        #region // Constructors 

        public Notes(int notesId, string note1, DateTime dueDate, int stationID, int userID)
        {
            NotesID = notesId;
            Note1 = note1;
            DueDate = dueDate;
            StationID = stationID;
            UserID = userID;
        }

        public Notes(string note, DateTime dueDate)
        {
            Note1 = note;
            DueDate = dueDate;
        }

        public Notes()
        {
            
        }
        #endregion

        #region // ToString() method

        public override string ToString()
        {
            return $"{NotesID}, {Note1}, {DueDate}, {StationID}, {UserID}";
        }
        #endregion
    }
}
