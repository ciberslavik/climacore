#pragma once
#include <QObject>
#include <Services/QSerializer.h>


class FanStateResponse:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    FanStateResponse(){}
    virtual ~FanStateResponse(){}
    QS_FIELD(QString, Key)
    QS_FIELD(int, State)
};
