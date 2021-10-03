#pragma once
#include <QObject>
#include "Services/QSerializer.h"

class VentilationParams: public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    VentilationParams(){}
    virtual ~VentilationParams(){}

    QS_FIELD(float, Proportional)
    QS_FIELD(float, Integral)
    QS_FIELD(float, Differential)

    QS_FIELD(float, CorrectionMax)
    QS_FIELD(float, CorrectionMin)
};
