#pragma once
#include <QObject>
#include <Services/QSerializer.h>

class FanModeRequest:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    FanModeRequest(){}
    virtual ~FanModeRequest(){}

    QS_FIELD(QString, Key)
    QS_FIELD(int, Mode)
};
