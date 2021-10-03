#pragma once
#include <QObject>
#include <Services/QSerializer.h>

class SchedulerProcessInfo:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    SchedulerProcessInfo(){}
    virtual ~SchedulerProcessInfo(){}

    QS_FIELD(float, TemperatureSetPoint)
    QS_FIELD(float, TemperatureFront)
    QS_FIELD(float, TemperatureRear)

    QS_FIELD(float, VentilationMaxPoint)
    QS_FIELD(float, VentilationMinPoint)
    QS_FIELD(float, VentilationSetPoint)

    QS_FIELD(float, AnalogPower)

    QS_FIELD(float, ValveSetPoint)
    QS_FIELD(float, MineSetPoint)

    QS_FIELD(int, CurrentDay)
    QS_FIELD(int, CurrentHeads)


};
