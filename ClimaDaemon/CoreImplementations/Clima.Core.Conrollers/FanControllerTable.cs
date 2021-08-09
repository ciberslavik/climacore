using System.Collections.Generic;
using System.IO;

namespace Clima.Core.Conrollers.Ventilation
{
    internal class FanControllerTable
    {
        private List<FanControllerTableItem> _fanTable;

        internal FanControllerTable()
        {
            _fanTable = new List<FanControllerTableItem>();
        }

        internal FanControllerTableItem this[int index]
        {
            get => _fanTable[index];
            set => _fanTable[index] = value;
        }

        internal void Add(FanControllerTableItem item)
        {
            if (CheckContainsPriority(item.Priority))
                throw new InvalidDataException($"item with this priority:{item.Priority} already exsist");
            _fanTable.Add(item);
        }

        internal void Remove(FanControllerTableItem item)
        {
            _fanTable.Remove(item);
        }

        internal void Clear()
        {
            _fanTable.Clear();
        }

        private bool CheckContainsPriority(int priority)
        {
            foreach (var item in _fanTable)
                if (item.Priority == priority)
                    return true;
            return false;
        }
    }
}