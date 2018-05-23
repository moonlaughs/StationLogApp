using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationLogApp.Model
{
    public class Notes : Station
    {
        #region // Properties
        public int NotesId { get; set; }
        public string Note1 { get; set; }
        public DateTime DueDate { get; set; }
        public int StationId { get; set; }
        public int UserId { get; set; }
        #endregion

        #region // Constructors 

        public Notes(int notesId, string note1, DateTime dueDate, int stationId, int userId)
        {
            NotesId = notesId;
            Note1 = note1;
            DueDate = dueDate;
            StationId = stationId;
            UserId = userId;
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
            return $"{NotesId}, {Note1}, {DueDate}, {StationId}, {UserId}";
        }
        #endregion
    }
}