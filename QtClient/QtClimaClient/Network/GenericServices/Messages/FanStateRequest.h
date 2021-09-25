#pragma once
#include <QObject>
#include <Services/QSerializer.h>

class FanStateRequest:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    FanStateRequest(){}
    virtual ~FanStateRequest(){}

    QS_FIELD(QString, Key)
    QS_FIELD(int, State)
};
