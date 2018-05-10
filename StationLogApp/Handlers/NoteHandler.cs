using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StationLogApp.Converters;
using StationLogApp.Interfaces;
using StationLogApp.Model;
using StationLogApp.Persistancy;
using StationLogApp.ViewModel;

namespace StationLogApp.Handlers
{
    public class NoteHandler
    {
        private NoteVM _noteVM;
        private ISave<Notes> _savedNote = new SaveM<Notes>();
        private DateConverter _dateConverter = new DateConverter();
        

        public NoteHandler(NoteVM noteVM)
        {
            _noteVM = noteVM;
        }

        public void CreateNote()
        {
            DateTime convertedDate = _dateConverter.ConvertToDate(_noteVM.DueDate);
            Notes note1 = new Notes(_noteVM.Note, _noteVM.StationID, convertedDate, _noteVM.UserID);
            
        }

        public void SaveNote(Notes note1)
        {
            _savedNote.Save(note1, "Notes");
        }

        public static ObservableCollection<Station> LoadStations()
        {
            LoadM<Station> retrievedCatalog = new LoadM<Station>();
            Task<ObservableCollection<Station>> retrievedStationsTask = retrievedCatalog.Load("Stations");
            ObservableCollection<Station> collectionOfStations = retrievedStationsTask.Result;
            return collectionOfStations;
        }
    }
}
