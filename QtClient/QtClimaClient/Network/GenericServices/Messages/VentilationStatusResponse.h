#pragma once
#include <QObject>
#include "Services/QSerializer.h"

class VentilationStatusResponse:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    VentilationStatusResponse(){}
    virtual ~VentilationStatusResponse(){}

    QS_FIELD(float, MineCurrentPos)
    QS_FIELD( float, MineSetPoint)
    QS_FIELD( float, ValveCurrentPos)
    QS_FIELD( float, ValveSetPoint)
    QS_FIELD( float, VentSetPoint)
};
