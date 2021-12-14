#pragma once
#include <QObject>
#include "Services/QSerializer.h"
#include "Models/Timers/LightTimerDay.h"

class LightTimerProfile:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    LightTimerProfile(){}
    virtual ~LightTimerProfile(){}
    int getRowCount() const
    {
        int max_row = 0;
        foreach(const LightTimerDay &day, Days)
        {
            if(day.Timers.count() > max_row)
                max_row = day.Timers.count();
        }
        return max_row;
    }

    QS_FIELD(QString, Key)
    QS_FIELD(QString, Name)
    QS_FIELD(QString, Description)
    QS_COLLECTION_OBJECTS(QList, LightTimerDay, Days)
};
