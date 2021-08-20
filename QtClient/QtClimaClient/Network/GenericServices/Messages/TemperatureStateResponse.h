#pragma once
#include <QObject>
#include <Network/NetworkResponse.h>

class TemperatureStateResponse:public NetworkResponse
{
    Q_GADGET;
    QS_SERIALIZABLE;
public:
    TemperatureStateResponse()
    {

    }
public:
    QS_FIELD(float, FrontTemperature)
    QS_FIELD(float, RearTemperature)
    QS_FIELD(float, OutdoorTemperature)
    QS_FIELD(float, AverageTemperature)
    QS_FIELD(float, TemperatureCorrection)
    QS_FIELD(QString, CurrentGraphName)
    QS_COLLECTION(QList, float, AvgGraphPoints)
};
