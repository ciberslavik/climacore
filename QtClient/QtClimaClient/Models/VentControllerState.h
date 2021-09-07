#pragma once
#include <QObject>
#include <Services/QSerializer.h>

class VentControllerState:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    VentControllerState(){}
    virtual ~VentControllerState(){}

    QS_FIELD(QString, CurrentProfileName)
    QS_FIELD(QString, CurrentProfileKey)
    QS_FIELD(float, SetPoint)
    QS_FIELD(bool, IsManual)
};
