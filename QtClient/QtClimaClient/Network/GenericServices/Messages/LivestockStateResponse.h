#pragma once
#include <QObject>
#include <Services/QSerializer.h>
#include <Models/LivestockState.h>

class LivestockStateResponse:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    LivestockStateResponse(){}
    virtual ~LivestockStateResponse(){}

    QS_OBJECT(LivestockState, State)
};
