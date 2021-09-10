#pragma once
#include <QObject>
#include <QDateTime>
#include <Services/QSerializer.h>
#include <Models/ProductionState.h>
class ProductionStateResponse:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    ProductionStateResponse(){}
    virtual ~ProductionStateResponse(){}

    QS_OBJECT(ProductionState, State)

};
