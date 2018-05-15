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
        public string Note { get; set; }
        public DateTime DueDate { get; set; }
        public int StationID { get; set; }
        public int UserID { get; set; }
        #endregion

        #region // Constructors 

        public Notes(int notesId, string note, DateTime dueDate, int stationID, int userID)
        {
            NotesID = notesId;
            Note = note;
            DueDate = dueDate;
            StationID = stationID;
            UserID = userID;
        }

        public Notes(string note, DateTime dueDate)
        {
            Note = note;
            DueDate = dueDate;
        }

        public Notes()
        {
            
        }
        #endregion

        #region // ToString() method

        public override string ToString()
        {
            return $"{NotesID}, {Note}, {DueDate}, {StationID}, {UserID}";
        }
        #endregion
    }
}
