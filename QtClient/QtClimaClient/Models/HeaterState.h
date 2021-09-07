#pragma once
#include <QObject>
#include <Services/QSerializer.h>

class HeaterState:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    HeaterState(){}
    virtual ~HeaterState(){}

    QS_FIELD(QString, Key)
    QS_FIELD(QString, GraphName)
    QS_FIELD(float, SetPoint)
    QS_FIELD(float, Hysteresis)
    QS_FIELD(bool, IsManual)
    QS_FIELD(bool, IsRunning)

};
