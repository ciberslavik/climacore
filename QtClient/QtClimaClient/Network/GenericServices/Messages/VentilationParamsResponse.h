#pragma once
#include <QObject>
#include "Services/QSerializer.h"
#include "Models/VentilationParams.h"

class VentilationParamsResponse: public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    VentilationParamsResponse(){}
    virtual ~VentilationParamsResponse(){}

    QS_OBJECT(VentilationParams, Parameters)
};
