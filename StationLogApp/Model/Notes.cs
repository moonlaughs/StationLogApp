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
        public int StationID { get; set; }
        public DateTime DueDate { get; set; }
        public int UserID { get; set; }
        #endregion

        #region // Constructors 

        public Notes(int notesID, string note, int stationID, DateTime dueDate, int userID)
        {
            NotesID = notesID;
            Note = note;
            StationID = stationID;
            DueDate = dueDate;
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
            return $"{NotesID}, {Note}, {StationID}, {DueDate}, {UserID}";
        }
        #endregion
    }
}
