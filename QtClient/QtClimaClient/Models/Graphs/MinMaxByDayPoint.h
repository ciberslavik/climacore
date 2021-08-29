#pragma once
#include <QObject>
#include <Services/QSerializer.h>

class MinMaxByDayPoint:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE
public:
        MinMaxByDayPoint(){}
    virtual ~MinMaxByDayPoint(){}

    QS_FIELD(int, Day)
    QS_FIELD(float, MaxValue)
    QS_FIELD(float, MinValue)
};
