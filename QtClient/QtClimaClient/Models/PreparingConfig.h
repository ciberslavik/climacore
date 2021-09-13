#pragma once
#include <QObject>
#include <QDateTime>
#include <Services/QSerializer.h>

class PreparingConfig:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    PreparingConfig(){}
    virtual ~PreparingConfig(){}

    QS_FIELD(float, TemperatureSetPoint)
    QS_FIELD(float, VentilationSetPint)
    QS_FIELD(float, ValveSetPoint)
    QS_FIELD(float, MineSetPoint)
    QS_FIELD(QDateTime, StartDate)
};
