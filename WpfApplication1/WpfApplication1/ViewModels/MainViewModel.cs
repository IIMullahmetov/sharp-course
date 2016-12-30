/*
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using WpfApplication1.Models;
//using GongSolutions.Wpf.DragDrop;
using System.Windows;
using System;

namespace WpfApplication1.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<NoteViewModel> TasksList { get; set; }
        public ObservableCollection<NoteViewModel> ActiveTasksList { get; set; }
        public ObservableCollection<NoteViewModel> DoneTasksList { get; set; }

        public MainViewModel()
        {
            //NotesList = new ObservableCollection<NoteViewModel>(notes.Select(n => new NoteViewModel(n)));
        }

        public MainViewModel(IEnumerable<Note> notes, DataBaseContext dbContext)
        {
            var enumarable = notes as IList<Note> ?? notes.ToList();

            var todoData = enumarable.Where(t => t.NoteCategory.Equals("Goals"));
            TasksList = new ObservableCollection<NoteViewModel>(todoData.Select(x => new NoteViewModel(x, dbContext)));

            var activeData = enumarable.Where(t => t.NoteCategory.Equals("Active"));
            ActiveTasksList = new ObservableCollection<NoteViewModel>(activeData.Select(x => new NoteViewModel(x, dbContext)));

            var doneData = enumarable.Where(t => t.NoteCategory.Equals("Done"));
            DoneTasksList = new ObservableCollection<NoteViewModel>(doneData.Select(x => new NoteViewModel(x, dbContext)));

            //TasksList = new ObservableCollection<TaskViewModel>(tasks.Select(b => new TaskViewModel(b, dbContext));
            //ActiveTasksList = new ObservableCollection<TaskViewModel>(tasks.Select(b => new TaskViewModel(b, dbContext)));

        }
    }
}
*/

using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using WpfApplication1.Models;
using System.Windows;
using System;
using WpfApplication1.ViewModels;

namespace WpfApplication1.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        public ObservableCollection<NoteViewModel> TasksList { get; set; }
        public ObservableCollection<NoteViewModel> ActiveTasksList { get; set; }
        public ObservableCollection<NoteViewModel> DoneTasksList { get; set; }

        public MainViewModel()
        {


        }
        #region Constructor

        public MainViewModel(IEnumerable<Note> notes, DataBaseContext dbContext)
        {
            var enumarable = notes as IList<Note> ?? notes.ToList();

            var todoData = enumarable.Where(t => t.NoteCategory.Equals("Goals"));
            TasksList = new ObservableCollection<NoteViewModel>(todoData.Select(x => new NoteViewModel(x, dbContext)));

            var activeData = enumarable.Where(t => t.NoteCategory.Equals("Active"));
            ActiveTasksList = new ObservableCollection<NoteViewModel>(activeData.Select(x => new NoteViewModel(x, dbContext)));

            var doneData = enumarable.Where(t => t.NoteCategory.Equals("Done"));
            DoneTasksList = new ObservableCollection<NoteViewModel>(doneData.Select(x => new NoteViewModel(x, dbContext)));

            //TasksList = new ObservableCollection<TaskViewModel>(tasks.Select(b => new TaskViewModel(b, dbContext));
            //ActiveTasksList = new ObservableCollection<TaskViewModel>(tasks.Select(b => new TaskViewModel(b, dbContext)));

        }


        #endregion

    }
}

