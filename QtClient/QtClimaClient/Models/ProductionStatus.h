#pragma once
#include <QObject>
#include <QDateTime>

#include "Services/QSerializer.h"
typedef enum ProductionState
{
    Stopped,
    Growing,
    Preparing,
    Alarm,
    Fault
}ProductionState_t;

class ProductionStatus: public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    ProductionStatus(){}
    virtual ~ProductionStatus(){}

    QS_FIELD(int, CurrentState)
    QS_FIELD(int, TotalPlending)
    QS_FIELD(int, TotalKilled)
    QS_FIELD(int, TotalRefracting)
    QS_FIELD(QDateTime, StartProductionDate)
};

