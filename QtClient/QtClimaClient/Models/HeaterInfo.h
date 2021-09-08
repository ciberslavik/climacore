#pragma once
#pragma once
#include <QObject>
#include <Services/QSerializer.h>

class HeaterInfo:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    HeaterInfo(){}
    virtual ~HeaterInfo(){}

    QS_FIELD(QString, Key)
    QS_FIELD(QString, RelayName)
    QS_FIELD(float, Hysteresis)
    QS_FIELD(float, ManualSetPoint)
    QS_FIELD(bool, IsManual)
    QS_FIELD(int, ControlZone)
};
