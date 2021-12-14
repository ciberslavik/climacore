#pragma once
#include <QObject>
#include <QDateTime>
#include "Services/QSerializer.h"


class LivestockOperation:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    LivestockOperation(){}
    LivestockOperation(int opType, int heads, QDateTime opDate)
    {
        OperationType = opType;
        OperationDate = opDate;
        HeadCount = heads;
    }
    virtual ~LivestockOperation(){}

    QS_FIELD(int, OperationType)
    QS_FIELD(int, HeadCount)
    QS_FIELD(QDateTime, OperationDate);
};
