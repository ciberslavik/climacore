#pragma once
#include <QObject>
#include <Services/QSerializer.h>
#include <Models/HeaterState.h>

class HeaterStateRequest:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    HeaterStateRequest(){}
    virtual ~HeaterStateRequest(){}

    QS_FIELD(QString, Key)
    QS_OBJECT(HeaterState, State)
};
