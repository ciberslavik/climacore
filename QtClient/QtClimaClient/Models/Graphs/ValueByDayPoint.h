#pragma once


#include <QObject>
#include <Services/QSerializer.h>

class ValueByDayPoint : public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE
public:
    ValueByDayPoint(int day =0,float value =0){
        Day = day;
        Value = value;
    }
    virtual ~ValueByDayPoint(){}
    QS_FIELD(int, PointIndex)
    QS_FIELD(int, Day)
    QS_FIELD(float, Value)
};

