#pragma once
#include <QObject>
#include <Services/QSerializer.h>
#include <Models/FanInfo.h>

class FanInfoRequest:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    FanInfoRequest(){}
    virtual ~FanInfoRequest(){}
    QS_FIELD(QString, FanKey)
    QS_OBJECT(FanInfo, Info)
};
