#pragma once
#include <QObject>
#include <Services/QSerializer.h>

class FanInfo:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    FanInfo(){}
    virtual ~FanInfo(){}

    bool Hermetised;

    QS_FIELD(bool, IsAnalog)
    QS_FIELD(QString, Key)
    QS_FIELD(QString, FanName)
    QS_FIELD(QString, RelayName)
    QS_FIELD(int, Performance)
    QS_FIELD(int, FanCount)
    QS_FIELD(int, Priority)
    QS_FIELD(float, StartValue)
    QS_FIELD(float, StopValue)
    QS_FIELD(int, State)
    QS_FIELD(int, Mode)
    QS_FIELD(float, AnalogPower)
    QS_FIELD(bool, IsAlarm)

};
