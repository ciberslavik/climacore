#pragma once
#include <QObject>
#include <QTime>

#include "Services/QSerializer.h"

class LightTimerItem:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    LightTimerItem(){}
    virtual ~LightTimerItem(){}

    QS_FIELD(QDateTime, OnTime);
    QS_FIELD(QDateTime, OffTime);
};
