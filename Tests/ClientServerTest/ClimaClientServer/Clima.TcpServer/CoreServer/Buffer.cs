using System;
using System.Diagnostics;

namespace Clima.TcpServer.CoreServer
{
    public class Buffer
    {
        private byte[] _data;
        private long _size;
        private long _offset;

        public bool IsEmpty => (_data == null) || (_size == 0);
        public byte[] Data => _data;
        public long Capacity => _data.Length;
        public long Size => _size;
        public long Offset => _offset;
        public byte this[int index] => _data[index];

        public Buffer() { _data = new byte[0]; _size = 0; _offset = 0; }
        public Buffer(long capacity) { _data = new byte[capacity]; _size = 0; _offset = 0; }
        public Buffer(byte[] data) { _data = data; _size = data.Length; _offset = 0; }
        public void Reserve(long capacity)
        {
            Debug.Assert((capacity >= 0), "Invalid reserve capacity!");
            if (capacity < 0)
                throw new ArgumentException("Invalid reserve capacity!", nameof(capacity));

            if (capacity > Capacity)
            {
                byte[] data = new byte[Math.Max(capacity, 2 * Capacity)];
                Array.Copy(_data, 0, data, 0, _size);
                _data = data;
            }
        }
        public void Resize(long size)
        {
            Reserve(size);
            _size = size;
            if (_offset > _size)
                _offset = _size;
        }
    }
}