using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Clima.Basics
{
    public abstract class ObservableObject:INotifyPropertyChanged
    {
        private bool _isModified = false;
        protected virtual T Update<T>(ref T prop, T val, [CallerMemberName] string propertyName = "")
        {
            if (!Equals(prop, val))
            {
                prop = val;
                _isModified = true;
                OnPropertyChanged(propertyName);
            }

            return prop;
        }

        public bool IsModified => _isModified;
        public abstract void AcceptModification(); 

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}