
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication1.Models;

namespace WpfApplication1.ViewModels
{
    public class NoteViewModel : ViewModelBase
    {
        public Note Note;

        protected DataBaseContext DBContext;

        public NoteViewModel()
        {

        }

        public NoteViewModel(Note note, DataBaseContext dbContext)
        {
            Note = note;
            DBContext = dbContext;
        }

        public string Name
        {
            get { return Note.Name; }
            set
            {
                Note.Name = value;
                OnPropertyChanged("Name");
                DBContext.SaveChanges();
            }
        }

        public string Description
        {
            get { return Note.Description; }
            set
            {
                Note.Description = value;
                OnPropertyChanged("Description");
                DBContext.SaveChanges();
            }
        }

        public DateTime StartTime
        {
            get { return Note.StartTime; }
            set
            {
                Note.StartTime = value;
                OnPropertyChanged("StartTime");
                DBContext.SaveChanges();
            }
        }

        public DateTime? EndTime
        {
            get { return Note.EndTime; }
            set
            {
                Note.EndTime = value;
                OnPropertyChanged("EndTime");
            }
        }
    }
}
