#pragma once
#include <QObject>
#include <Services/QSerializer.h>

class SchedulerInfo:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    SchedulerInfo(){}
    virtual ~SchedulerInfo(){}

    QS_FIELD(QString, TemperatureProfileKey)
    QS_FIELD(QString, TemperatureProfileName)

    QS_FIELD(QString, VentilationProfileKey)
    QS_FIELD(QString, VentilationProfileName)

    QS_FIELD(QString, ValveProfileKey)
    QS_FIELD(QString, ValveProfileName)

    QS_FIELD(QString, MineProfileKey)
    QS_FIELD(QString, MineProfileName)

    QS_FIELD(float, TemperatureSetPoint)
    QS_FIELD(float, VentilationMaxPoint)
    QS_FIELD(float, VentilationMinPoint)
    QS_FIELD(float, VentilationSetPoint)

    QS_FIELD(float, ValveSetPoint)
    QS_FIELD(float, MineSetPoint)
    QS_FIELD(int, CurrentDay)
    QS_FIELD(int, CurrentHeads)
};
