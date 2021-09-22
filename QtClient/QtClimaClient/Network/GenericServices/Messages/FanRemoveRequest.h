#pragma once
#include <QObject>
#include <Services/QSerializer.h>
#include <Models/FanInfo.h>

class FanRemoveRequest:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    FanRemoveRequest(){}
    virtual ~FanRemoveRequest(){}
    QS_FIELD(QString, FanKey)
};
