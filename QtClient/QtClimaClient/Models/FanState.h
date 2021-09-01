#pragma once
#include <QObject>
#include <QTime>
#include <Services/QSerializer.h>

#include "FanInfo.h"

class FanState:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    FanState(){}
    virtual ~FanState(){}

    QS_FIELD(int, State)
    QS_FIELD(int, Mode)
    QS_OBJECT(FanInfo, Info)
    QS_FIELD(QTime, WorkingTime)
};
