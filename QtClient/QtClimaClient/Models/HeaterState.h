#pragma once
#include <QObject>
#include <Services/QSerializer.h>
#include "HeaterInfo.h"

class HeaterState:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    HeaterState(){}
    virtual ~HeaterState(){}

    QS_OBJECT(HeaterInfo, Info)
    QS_FIELD(float, SetPoint)
    QS_FIELD(bool, IsRunning)
};
