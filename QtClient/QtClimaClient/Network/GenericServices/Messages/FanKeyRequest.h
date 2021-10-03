#pragma once
#include <QObject>
#include <Services/QSerializer.h>

class FanKeyRequest:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    FanKeyRequest(){}
    virtual ~FanKeyRequest(){}

    QS_FIELD(QString, FanKey);
};
