#pragma once
#include <QObject>
#include "Services/QSerializer.h"
#include "Models/VentilationParams.h"

class VentilationParamsRequest: public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    VentilationParamsRequest(){}
    virtual ~VentilationParamsRequest(){}

    QS_OBJECT(VentilationParams, Parameters)
};
