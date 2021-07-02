using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Clima.DataModel
{
    public abstract class ObservableObject:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual bool Update<T>(ref T prop, T value, [CallerMemberName]string propertyName = "")
        {
            bool result = false;
            if (!prop.Equals(value))
            {
                prop = value;
                OnPropertyChanged(propertyName);
            }

            return result;
        }
    }
}