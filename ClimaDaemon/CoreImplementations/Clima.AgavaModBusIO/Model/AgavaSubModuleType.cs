using System;

namespace Clima.AgavaModBusIO.Model
{
    internal enum AgavaSubModuleType:ushort
    {
        None = 0x00,
        DO = 0x01,
        SYM = 0x02,
        R = 0x03,
        AI = 0x04,
        AIO = 0x05,
        DI = 0x06,
        TMP = 0x07,
        DO6 = 0x08,
        ENI = 0x09
    }
}
