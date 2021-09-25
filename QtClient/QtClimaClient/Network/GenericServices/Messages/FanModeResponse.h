#pragma once
#include <QObject>
#include <Services/QSerializer.h>

class FanModeRsponse:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    FanModeRsponse(){}
    virtual ~FanModeRsponse(){}

    QS_FIELD(QString, Key)
    QS_FIELD(int, Mode)
};
