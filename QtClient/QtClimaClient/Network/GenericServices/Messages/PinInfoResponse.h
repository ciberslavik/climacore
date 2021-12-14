#pragma once
#include <QObject>
#include "Services/QSerializer.h"
#include "Models/PinInfo.h"

class PinInfoResponse:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    PinInfoResponse(){}
    virtual ~PinInfoResponse(){}

    QS_COLLECTION_OBJECTS(QList, PinInfo, Infos)
};
