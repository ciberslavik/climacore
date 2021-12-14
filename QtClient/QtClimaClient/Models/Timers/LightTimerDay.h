#pragma once
#include <QObject>
#include "Services/QSerializer.h"
#include "Models/Timers/LightTimerItem.h"

class LightTimerDay:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    LightTimerDay(){}
    virtual ~LightTimerDay(){}

    QS_FIELD(int, DayNumber);
    QS_COLLECTION_OBJECTS(QList, LightTimerItem, Timers);
};
