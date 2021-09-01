#pragma once
#include <QObject>
#include <Services/QSerializer.h>
#include "Models/FanState.h"

class VentilationStateResponse:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    VentilationStateResponse(){}
    virtual ~VentilationStateResponse(){}
    QS_COLLECTION_OBJECTS(QList, FanState, States)
};
