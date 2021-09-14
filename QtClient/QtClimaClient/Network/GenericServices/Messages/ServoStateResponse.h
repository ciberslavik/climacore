#pragma once
#include <QObject>
#include <Services/QSerializer.h>

class ServoStateResponse:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    ServoStateResponse(){}
    virtual ~ServoStateResponse(){}
    QS_FIELD(bool, IsManual)
    QS_FIELD(float, CurrentPosition)
    QS_FIELD(float, SetPoint)
};
