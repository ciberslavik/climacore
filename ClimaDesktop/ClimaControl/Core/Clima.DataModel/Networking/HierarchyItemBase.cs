using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using Clima.Basics;

namespace Clima.DataModel.Networking
{
    public abstract class HierarchyItemBase : ObservableObject
    {
        private ImmutableList<HierarchyItemBase> _child;
        private string _header;

        public virtual string Header
        {
            get => _header;
            set => Update(ref _header, value);
        }

        public virtual IList<HierarchyItemBase> ChildItems => _child;

        public virtual void AddChild(HierarchyItemBase item)
        {
            if (!_child.Contains(item))
            {
                _child = _child.Add(item);
                OnPropertyChanged();
            }
        }

        public virtual void RemoveChild(HierarchyItemBase item)
        {
            if (_child.Contains(item))
            {
                _child = _child.Remove(item);
                OnPropertyChanged();
            }
        }
    }
}