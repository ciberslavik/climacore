#pragma once
#include <QObject>
#include <Services/QSerializer.h>

class ProductionStateResponse:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    ProductionStateResponse(){}
    virtual ~ProductionStateResponse(){}

    QS_FIELD(int, State)
};
