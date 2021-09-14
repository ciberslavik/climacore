#pragma once
#include <QObject>
#include <Services/QSerializer.h>
#include <Models/FanState.h>

class UpdateFanStateRequest:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    UpdateFanStateRequest(){}
    virtual ~UpdateFanStateRequest(){}

    QS_OBJECT(FanState, State)
};
