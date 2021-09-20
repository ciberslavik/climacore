#pragma once
#include <Network/NetworkResponse.h>
#include <QObject>
#include <Services/QSerializer.h>

class ClimatStatusResponse:public NetworkResponse
{
    Q_GADGET;
    QS_SERIALIZABLE;

public:
    ClimatStatusResponse()
    {
    }
    QS_FIELD(float, FrontTemperature)
    QS_FIELD(float, RearTemperature)
    QS_FIELD(float, OutdoorTemperature)
    QS_FIELD(float, Humidity)
    QS_FIELD(float, Pressure)
    QS_FIELD(float, TempSetPoint)

    QS_FIELD(float, AnalogFanPower)
    QS_FIELD(float, ValvePosition)
    QS_FIELD(float, MinePosition)
    QS_FIELD(float, TotalFanPerformance)
    QS_FIELD(float, VentilationSetPoint)
};
