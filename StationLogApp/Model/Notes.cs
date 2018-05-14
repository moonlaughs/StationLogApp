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
        public string NoteText { get; set; }
        public int StationID { get; set; }
        public DateTimeOffset DueDate { get; set; }
        public int UserID { get; set; }
        #endregion

        #region // Constructors 

        public Notes(int notesID, string noteText, int stationID, DateTimeOffset dueDate, int userID)
        {
            NotesID = notesID;
            NoteText = noteText;
            StationID = stationID;
            DueDate = dueDate;
            UserID = userID;
        }

        public Notes(string noteText, int stationID, DateTimeOffset dueDate, int userID)
        {
            NoteText = noteText;
            StationID = stationID;
            DueDate = dueDate;
            UserID = userID;
        }

        public Notes()
        {
            
        }
        #endregion

        #region // ToString() method

        public override string ToString()
        {
            return $"{NotesID}, {NoteText}, {StationID}, {DueDate}, {UserID}";
        }
        #endregion
    }
}
