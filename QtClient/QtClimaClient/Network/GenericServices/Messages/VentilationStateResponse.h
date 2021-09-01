#pragma once
#include <QObject>
#include <Services/QSerializer.h>

class VentilationStateResponse:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    VentilationStateResponse(){}
    virtual ~VentilationStateResponse(){}

};
