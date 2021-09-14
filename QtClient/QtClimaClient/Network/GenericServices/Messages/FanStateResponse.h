#pragma once
#include <QObject>
#include <Services/QSerializer.h>
#include <Models/FanState.h>

class FanStateResponse:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    FanStateResponse(){}
    virtual ~FanStateResponse(){}
    QS_OBJECT(FanState, State)
};
