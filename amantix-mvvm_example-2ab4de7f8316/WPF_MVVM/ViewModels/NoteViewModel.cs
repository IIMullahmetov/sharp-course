using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WPF_MVVM.Annotations;
using WPF_MVVM.Entities;
using WPF_MVVM.Models;

namespace WPF_MVVM.ViewModels
{
    public class NoteViewModel : INotifyPropertyChanged
    {
        public Note Note { get; private set; }
        /*
        public Guid Id
        {
            get { return Note.Id; }
            set
            {
                Note.Id = value;
                OnPropertyChanged(nameof(Id));
            }
            
        }*/
        public string Name
        {
            get { return Note.Name; }
            set
            {
                Note.Name = value;
                OnPropertyChanged(nameof(Name));
            }

        }
        public string Description
        {
            get { return Note.Description; }
            set
            {
                Note.Description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
        public DateTime StartTime
        {
            get { return Note.StartTime; }
            set
            {
                Note.StartTime = value;
                OnPropertyChanged(nameof(StartTime));
            }
        }

        public TimeSpan WorkTime
        {
            get { return Note.WorkTime; }
            set
            {
                Note.WorkTime = value;
                OnPropertyChanged(nameof(WorkTime));
            }
        }

        public NoteCategory Category
        {
            get { return Note.NoteCategory; }
            set
            {
                Note.NoteCategory = value;
                OnPropertyChanged(nameof(Category));
            }
        }

        private NoteViewModel()
        {
        }
        public NoteViewModel([NotNull] Note note)
        {
            if (note == null) throw new ArgumentNullException(nameof(note));
            Note = note;
        }
        public NoteViewModel(string name, string desc, NoteCategory cat = NoteCategory.Goals)
        {
            Note = new Note(cat, name, desc, TimeSpan.Zero, DateTime.Now);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
