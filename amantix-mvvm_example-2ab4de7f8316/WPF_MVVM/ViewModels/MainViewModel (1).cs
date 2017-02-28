using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WPF_MVVM.Annotations;
using WPF_MVVM.Models;

namespace WPF_MVVM.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Note> Goals { get; set; }
        public ObservableCollection<Note> Active { get; set; }
        public ObservableCollection<Note> Done { get; set; }
        public ObservableCollection<Note> Canceled { get; set; }

        private Project _project;

        public MainViewModel(Project project)
        {
            _project = project;
            Goals = new ObservableCollection<Note>(project.Goals);
            Active = new ObservableCollection<Note>(project.Active);
            Done = new ObservableCollection<Note>(project.Done);
            Canceled = new ObservableCollection<Note>(project.Canceled);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}