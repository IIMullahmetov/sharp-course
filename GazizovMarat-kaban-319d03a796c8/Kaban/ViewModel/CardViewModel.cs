using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Kaban.Annotations;
using Kaban.Entities;
using Kaban.Model;

namespace Kaban.ViewModel
{
    public class CardViewModel : INotifyPropertyChanged
    {
        public Card Card { get; private set; }

        public Guid Id
        {
            get { return Card.Id; }
            set
            {
                Card.Id = value;
                OnPropertyChanged(nameof(Id));
            }

        }
        public string Name
        {
            get { return Card.Name; }
            set
            {
                Card.Name = value;
                OnPropertyChanged(nameof(Name));
            }

        }
        public string Description
        {
            get { return Card.Description; }
            set
            {
                Card.Description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
        public DateTime StartTime
        {
            get { return Card.StartTime; }
            set
            {
                Card.StartTime = value;
                OnPropertyChanged(nameof(StartTime));
            }
        }

        public TimeSpan WorkTime
        {
            get { return Card.WorkTime; }
            set
            {
                Card.WorkTime = value;
                OnPropertyChanged(nameof(WorkTime));
            }
        }

        public CardCategory Category
        {
            get { return Card.Category; }
            set
            {
                Card.Category = value;
                OnPropertyChanged(nameof(Category));
            }
        }

        private CardViewModel()
        {
        }
        public CardViewModel([NotNull] Card card)
        {
            if (card == null) throw new ArgumentNullException(nameof(card));
            Card = card;
        }
        public CardViewModel(string name, string desc, CardCategory cat = CardCategory.ToDo)
        {
            Card = new Card(name, desc, cat);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}