namespace Clima.AgavaModBusIO.Model
{
    public enum AgavaAnalogInType : ushort
    {
        Voltage_0_10V = 0,
        Current_4_20mA,
        Current_0_20mA,
        Current_0_5mA,
        Resistance,
        TR_Pt100,
        TR_Pt1000,
        TR_50M,
        TR_100M,
        TC_L,
        TC_J,
        TC_N,
        TC_K,
        TC_S,
        TC_R,
        TC_B,
        TC_A1,
        TC_A2,
        TC_A3,
        TR_TSP50,
        TR_TSP100,
        Millivolts
    }

    public enum AgavaAnalogOutType : ushort
    {
        Voltage_0_10V = 0,
        Current_4_20mA,
        Current_0_20mA,
        Current_0_5mA
    }
}