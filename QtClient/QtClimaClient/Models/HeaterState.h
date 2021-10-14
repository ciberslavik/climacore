#pragma once
#include <QObject>
#include <Services/QSerializer.h>
#include "HeaterParams.h"

class HeaterState:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    HeaterState(){}
    virtual ~HeaterState(){}

    QS_FIELD(float, SetPoint)
    QS_FIELD(float, CorrectedSetPoint)
    QS_FIELD(bool, IsRunning)
};
