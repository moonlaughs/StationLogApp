using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StationLogApp.Interfaces;
using StationLogApp.Model;
using StationLogApp.ViewModel;

namespace StationLogApp.Handlers
{
    public class NoteHandler
    {
        private NoteVM _noteVM;
        private ISave<Notes> _savedNote = new SaveM<Notes>();
        

        public NoteHandler(NoteVM noteVM)
        {
            _noteVM = noteVM;
        }

        public void SaveNote()
        {
            Notes note1 = new Notes(
                _noteVM.Note, _noteVM.DueDate);

            _savedNote.Save(note1, "Notes");
        }
    }
}
