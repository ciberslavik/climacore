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

    QS_FIELD(bool, IsAnalog)
    QS_FIELD(QString, Key)
    QS_FIELD(QString, FanName)
    QS_FIELD(QString, RelayName)
    QS_FIELD(int, Performance)
    QS_FIELD(int, FanCount)
    QS_FIELD(int, Priority)
    QS_FIELD(bool, Hermetise)
    QS_FIELD(bool, IsManual)
    QS_FIELD(float, StartValue)
    QS_FIELD(float, StopValue)
};
