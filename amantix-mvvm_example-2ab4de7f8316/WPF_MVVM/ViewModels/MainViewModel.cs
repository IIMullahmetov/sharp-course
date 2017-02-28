using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WPF_MVVM.Annotations;
using WPF_MVVM.Models;
using System.Windows;
using System.Windows.Documents;
using GongSolutions.Wpf.DragDrop;
using GongSolutions.Wpf.DragDrop.Utilities;
using WPF_MVVM.Context;
using WPF_MVVM.Services;
using WPF_MVVM.Entities;
using System.Linq;
using System;

namespace WPF_MVVM.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged, IDropTarget
    {
        //private Project project;

        private readonly TaskDbContext _dbContext;
        readonly DataServices _dataServices;

        public ObservableCollection<NoteViewModel> Goals { get; set; }
            = new ObservableCollection<NoteViewModel>();
        public ObservableCollection<NoteViewModel> Active { get; set; }
            = new ObservableCollection<NoteViewModel>();
        public ObservableCollection<NoteViewModel> Done { get; set; }
            = new ObservableCollection<NoteViewModel>();
        public ObservableCollection<NoteViewModel> Canceled { get; set; }
            = new ObservableCollection<NoteViewModel>();

        private NoteViewModel _selectedNoteViewModel;

        public NoteViewModel SelectedNoteViewModel
        {
            get { return _selectedNoteViewModel; }
            set
            {
                _selectedNoteViewModel = value;
                OnPropertyChanged(nameof(SelectedNoteViewModel));
            }
        }

        //public MainViewModel(Project project)
        public MainViewModel()
        {
            //this.project = project;
            _dbContext = new TaskDbContext();
            //_dataServices = new DataServices(_dbContext);
            /*
            Goals = new ObservableCollection<NoteViewModel>();
            Active = new ObservableCollection<NoteViewModel>();
            Done = new ObservableCollection<NoteViewModel>();
            Canceled = new ObservableCollection<NoteViewModel>();
            */
            Goals.CollectionChanged +=
                (sender, args) =>
                {

                    foreach (var goals in Goals)
                        if (goals.Category != NoteCategory.Goals)
                        {
                            goals.Category = NoteCategory.Goals;
                            goals.WorkTime = default(TimeSpan);
                        }
                };
            Active.CollectionChanged +=
                (sender, args) =>
                {

                    foreach (var active in Active)
                        if (active.Category != NoteCategory.Active)
                        {
                            active.Category = NoteCategory.Active;
                            active.StartTime = DateTime.Now;
                            active.WorkTime = default(TimeSpan);
                        }
                };
            Done.CollectionChanged +=
                (sender, args) =>
                {
                    foreach (var done in Done)
                        if (done.Category != NoteCategory.Done)
                        {
                            done.Category = NoteCategory.Done;
                            done.WorkTime = (done.StartTime < DateTime.Now) ? DateTime.Now - done.StartTime : default(TimeSpan);
                        }
                };
            Canceled.CollectionChanged +=
                (sender, args) =>
                {
                    foreach (var canceled in Canceled)
                        if (canceled.Category != NoteCategory.Canceled)
                        {
                            canceled.Category = NoteCategory.Canceled;
                            canceled.WorkTime = (canceled.StartTime < DateTime.Now) ? DateTime.Now - canceled.StartTime : default(TimeSpan);
                        }
                };

            //GetCards();
        }

        private void GetCards()
        {
            Goals.Clear();
            Active.Clear();
            Done.Clear();
            Canceled.Clear();
            var cards = _dataServices.GetCards(x => true);
            _dataServices
                .GetCards(x => x.NoteCategory == NoteCategory.Goals)
                .ToList()
                .ForEach(x => Goals.Add(new NoteViewModel(x)));
            _dataServices
                .GetCards(x => x.NoteCategory == NoteCategory.Active)
                .ToList()
                .ForEach(x => Active.Add(new NoteViewModel(x)));
            _dataServices
                .GetCards(x => x.NoteCategory == NoteCategory.Done)
                .ToList()
                .ForEach(x => Done.Add(new NoteViewModel(x)));
        }

        public void DragOver(IDropInfo dropInfo)
        {
            dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
            dropInfo.Effects = DragDropEffects.Copy;
        }

        public void Drop(IDropInfo dropInfo)
        {
            NoteViewModel sourceItem = dropInfo.Data as NoteViewModel;
            NoteViewModel targetItem = dropInfo.TargetItem as NoteViewModel;
            var targetCollection = (ObservableCollection<NoteViewModel>)dropInfo.TargetCollection;
            var sourceCollection = (ObservableCollection<NoteViewModel>)dropInfo.DragInfo.SourceCollection;
            if (sourceItem != null)
            {
                int targetIndex = targetItem != null ? targetCollection.IndexOf(targetItem) : targetCollection.Count;
                targetCollection.Insert(targetIndex, sourceItem);
                sourceCollection.Remove(sourceItem);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "") //or null
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Note noteData;

        public Note NoteData
        {
            get { return noteData; }
            set { noteData = value; OnPropertyChanged(); }
        }
    }
}