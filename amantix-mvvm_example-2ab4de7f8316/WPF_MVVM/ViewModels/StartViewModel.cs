using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WPF_MVVM.Annotations;
using WPF_MVVM.Models;

namespace WPF_MVVM.ViewModels
{
    public class StartViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Project> Projects { get; set; }

        private List _list;

        public StartViewModel(List list)
        {
            _list = list;
            Projects = new ObservableCollection<Project>(list.Projects);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
