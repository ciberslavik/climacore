#pragma once
#include <QObject>
#include "Services/QSerializer.h"

class PinInfo:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    PinInfo(){}
    virtual ~PinInfo(){}

    QS_FIELD(QString, PinName)
    QS_FIELD(int, PinType)
    QS_FIELD(float, PinState)
};
