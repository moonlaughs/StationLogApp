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
        private ICreate<Notes> _createdNote = new CreateM<Notes>();
        private DateConverter _dateConverter = new DateConverter();
        private Notes _note1;

        public NoteHandler(NoteVM noteVM)
        {
            _noteVM = noteVM;
        }

        public void CreateNote()
        {
            DateTime convertedDate = _dateConverter.ConvertToDate(_noteVM.DueDate);
            int convertedStationName = _noteVM.SelectedNote.StationID;
            _note1 = new Notes(_noteVM.NotesID, _noteVM.NoteText, convertedDate, convertedStationName, _noteVM.UserID);
        }

        public void SaveNote()
        {
            _savedNote.Save(_note1, "Notes");
        }

        public void CreateAndSaveNote()
        {
            CreateNote();
            SaveNote();
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
