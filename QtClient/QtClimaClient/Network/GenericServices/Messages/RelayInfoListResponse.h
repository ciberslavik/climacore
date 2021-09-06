#pragma once
#include <QObject>
#include <Services/QSerializer.h>
#include "Models/RelayInfo.h"



class RelayInfoListResponse:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    RelayInfoListResponse(){}
    virtual ~RelayInfoListResponse(){}

    QS_COLLECTION_OBJECTS(QList, RelayInfo, Infos)
};
