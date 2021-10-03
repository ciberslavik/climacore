#pragma once
#include <QObject>
#include <Services/QSerializer.h>

class SchedulerProfilesInfo:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    SchedulerProfilesInfo(){}
    virtual ~SchedulerProfilesInfo(){}

    QS_FIELD(QString, TemperatureProfileKey)
    QS_FIELD(QString, TemperatureProfileName)

    QS_FIELD(QString, VentilationProfileKey)
    QS_FIELD(QString, VentilationProfileName)

    QS_FIELD(QString, ValveProfileKey)
    QS_FIELD(QString, ValveProfileName)

    QS_FIELD(QString, MineProfileKey)
    QS_FIELD(QString, MineProfileName)
};
