#pragma once


#include <QObject>
#include <Services/QSerializer.h>

class ValueByValuePoint : public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE
public:
    ValueByValuePoint(float value_x = 0,float value_y = 0){
        ValueX = value_x;
        ValueY = value_y;
    }
    virtual ~ValueByValuePoint(){}
    QS_FIELD(int, PointIndex)
    QS_FIELD(float, ValueX)
    QS_FIELD(float, ValueY)
};

