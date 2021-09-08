#pragma once
#include <QObject>
#include <QDateTime>
#include <Services/QSerializer.h>

class LivestockOperationRequest:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    LivestockOperationRequest(){}
    virtual ~LivestockOperationRequest(){}

    QS_FIELD(int, HeadsCount)
    QS_FIELD(QDateTime, OperationDate)
};
