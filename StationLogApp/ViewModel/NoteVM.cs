using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StationLogApp.Common;
using StationLogApp.Handlers;

namespace StationLogApp.ViewModel
{
    public class NoteVM :NotifyPropertyChangedClass
    {
        private string _note;
        private string _description;
        private DateTime _dueDate;

        public string Note
        {
            get { return _note; }
            set
            {
                _note = value; 
                OnPropertyChanged(nameof(Note));
            }
        }

        public string Descrption
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Descrption));
            }
        }

        public DateTime DueDate
        {
            get { return _dueDate; }
            set
            {
                _dueDate = value;
                OnPropertyChanged(nameof(DueDate));
            }
        }

        public RelayCommandClass SaveNote { get; set; }
        public NoteHandler NoteHandler { get; set; }

        public NoteVM()
        {
            NoteHandler = new NoteHandler(this);
            SaveNote = new RelayCommandClass(NoteHandler.SaveNote);
        }

    }
}
