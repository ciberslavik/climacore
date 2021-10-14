#pragma once
#pragma once
#include <QObject>
#include <Services/QSerializer.h>

class HeaterParams:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    HeaterParams(){}
    virtual ~HeaterParams(){}

    QS_FIELD(QString, Key);
    QS_FIELD(QString, PinName);
    QS_FIELD(float, DeltaOn);
    QS_FIELD(float, DeltaOff);
    QS_FIELD(float, ManualSetPoint);
    QS_FIELD(bool, IsManual);
    QS_FIELD(int, ControlZone);
    QS_FIELD(float, Correction);
};
