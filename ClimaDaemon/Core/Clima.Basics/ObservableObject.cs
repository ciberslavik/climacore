using System;
using System.Runtime.CompilerServices;

namespace Clima.Basics
{
    public class PropertyChangedEventArgs : EventArgs
    {
        public PropertyChangedEventArgs(string propertyName)
        {
            PropertyName = propertyName;
        }

        public string PropertyName { get; set; }
    }

    public delegate void PropertyChangedEventHandler(object sender, PropertyChangedEventArgs ea);

    public abstract class ObservableObject
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual bool Update<T>(ref T prop, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(prop, value))
                return false;

            prop = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}