#pragma once
#include <QObject>
#include <Services/QSerializer.h>
#include <Models/ProductionConfig.h>

class ProductionConfigRequest:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    ProductionConfigRequest(){}
    virtual ~ProductionConfigRequest(){}

    QS_OBJECT(ProductionConfig, Config)
};
