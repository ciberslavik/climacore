#pragma once
#include <QObject>
#include <Models/LivestockOperation.h>
#include "Services/QSerializer.h"

class LivestockOpListResponse:public QSerializer
{
    Q_GADGET;
    QS_SERIALIZABLE;
public:
    LivestockOpListResponse(){}
    virtual ~LivestockOpListResponse(){}

    QS_COLLECTION_OBJECTS(QList, LivestockOperation, Operations);
};
