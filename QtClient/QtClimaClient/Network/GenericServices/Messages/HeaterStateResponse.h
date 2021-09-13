#pragma once
#include <QObject>
#include <Services/QSerializer.h>
#include <Models/HeaterState.h>

class HeaterStateResponse:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    HeaterStateResponse(){}
    virtual ~HeaterStateResponse(){}

    QS_OBJECT(HeaterState, State)
};
