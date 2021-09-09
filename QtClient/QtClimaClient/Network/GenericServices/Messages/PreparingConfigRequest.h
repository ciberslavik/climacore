#pragma once
#include <QObject>
#include <Services/QSerializer.h>
#include "Models/PreparingConfig.h"

class PreparingConfigRequest:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    PreparingConfigRequest(){}
    virtual ~PreparingConfigRequest(){}

    QS_OBJECT(PreparingConfig, Config)
};
