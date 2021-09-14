#pragma once
#include <QObject>
#include <Services/QSerializer.h>

class UpdateServoStateRequest:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    UpdateServoStateRequest(){}
    virtual ~UpdateServoStateRequest(){}
    QS_FIELD(bool, IsManual)
    QS_FIELD(float, SetPoint)
};
