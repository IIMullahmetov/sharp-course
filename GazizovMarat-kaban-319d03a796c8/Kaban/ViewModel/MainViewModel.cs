using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Documents;
using GongSolutions.Wpf.DragDrop;
using GongSolutions.Wpf.DragDrop.Utilities;
using Kaban.Annotations;
using Kaban.Command;
using Kaban.Context;
using Kaban.Entities;
using Kaban.Model;
using Kaban.Services;
using Kaban.View;

namespace Kaban.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged, IDropTarget
    {
        private readonly KabanDbContext _dbContext;
        readonly DataServices _dataServices;
        public ObservableCollection<CardViewModel> ToDoCards { get; set; }
            = new ObservableCollection<CardViewModel>();
        public ObservableCollection<CardViewModel> DoingCards { get; set; }
            = new ObservableCollection<CardViewModel>();

        public ObservableCollection<CardViewModel> DoneCards { get; set; }
            = new ObservableCollection<CardViewModel>();

        private CardViewModel _selectedCardViewModel;
        public CardViewModel SelectedCardViewModel
        {
            get { return _selectedCardViewModel; }
            set
            {
                _selectedCardViewModel = value;
                OnPropertyChanged(nameof(SelectedCardViewModel));
            }
        }
        private EditCardView _editCardView;
        private EditCardView EditCardView => _editCardView ?? (_editCardView = new EditCardView());

        public MainViewModel()
        {
            _dbContext = new KabanDbContext();
            _dataServices = new DataServices(_dbContext);

            GetCards();

            ToDoCards.CollectionChanged +=
                (sender, args) =>
                {

                    foreach (var toDoCard in ToDoCards)
                        if (toDoCard.Category != CardCategory.ToDo)
                        {
                            toDoCard.Category = CardCategory.ToDo;
                            toDoCard.WorkTime = default(TimeSpan);
                        }
                };
            DoingCards.CollectionChanged +=
                (sender, args) =>
                {

                    foreach (var doingCard in DoingCards)
                        if (doingCard.Category != CardCategory.Doing)
                        {
                            doingCard.Category = CardCategory.Doing;
                            doingCard.StartTime = DateTime.Now;
                            doingCard.WorkTime = default(TimeSpan);
                        }
                };
            DoneCards.CollectionChanged +=
                (sender, args) =>
                {
                    foreach (var doneCard in DoneCards)
                        if (doneCard.Category != CardCategory.Done)
                        {
                            doneCard.Category = CardCategory.Done;
                            doneCard.WorkTime = (doneCard.StartTime < DateTime.Now) ? DateTime.Now - doneCard.StartTime : default(TimeSpan);
                        }
                };
        }

        private void GetCards()
        {
            ToDoCards.Clear();
            DoingCards.Clear();
            DoneCards.Clear();
            var cards = _dataServices.GetCards(x => true);
            _dataServices
                .GetCards(x => x.Category == CardCategory.ToDo)
                .ToList()
                .ForEach(x => ToDoCards.Add(new CardViewModel(x)));
            _dataServices
                .GetCards(x => x.Category == CardCategory.Doing)
                .ToList()
                .ForEach(x => DoingCards.Add(new CardViewModel(x)));
            _dataServices
                .GetCards(x => x.Category == CardCategory.Done)
                .ToList()
                .ForEach(x => DoneCards.Add(new CardViewModel(x)));
        }
        private RelayCommand _addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return _addCommand ??
                  (_addCommand = new RelayCommand(obj =>
                  {
                      Card card = new Card();
                      EditCardView.SetDataContext(card);
                      if (EditCardView.ShowDialog() == true)
                      {
                          _dbContext.Cards.Add(card);
                          _dbContext.SaveChanges();
                          GetCards();
                      }

                  }));
            }
        }
        private RelayCommand _editCommand;

        public RelayCommand EditCommand
        {
            get
            {
                return _editCommand ??
                  (_editCommand = new RelayCommand(obj =>
                  {
                      if (SelectedCardViewModel != null)
                      {
                          EditCardView.SetDataContext(SelectedCardViewModel);
                          if (EditCardView.ShowDialog() == true)
                          {

                              //_dbContext.Cards.Add(SelectedCardViewModel.Card);
                              _dbContext.SaveChanges();
                              GetCards();
                          }


                      }
                  }));
            }
        }

        #region GongSolutions.Wpf.DragDrop
        public void DragOver(IDropInfo dropInfo)
        {

            dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
            dropInfo.Effects = DragDropEffects.Copy;
        }

        public void Drop(IDropInfo dropInfo)
        {
            CardViewModel sourceItem = dropInfo.Data as CardViewModel;
            CardViewModel targetItem = dropInfo.TargetItem as CardViewModel;
            var targetCollection = (ObservableCollection<CardViewModel>)dropInfo.TargetCollection;
            var sourceCollection = (ObservableCollection<CardViewModel>)dropInfo.DragInfo.SourceCollection;
            if (sourceItem != null)
            {
                int targetIndex = targetItem != null ? targetCollection.IndexOf(targetItem) : targetCollection.Count;
                targetCollection.Insert(targetIndex, sourceItem);
                sourceCollection.Remove(sourceItem);
            }
        }
        #endregion
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}