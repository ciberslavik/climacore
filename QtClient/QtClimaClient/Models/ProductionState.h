#pragma once
#include <QObject>
#include <QDateTime>

#include "Services/QSerializer.h"

class ProductionState: public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    ProductionState(){}
    virtual ~ProductionState(){}

    QS_FIELD(int, State)
    QS_FIELD(QDateTime, StartDate)
    QS_FIELD(int, CurrentDay)
    QS_FIELD(int, CurrentHeads)
};

