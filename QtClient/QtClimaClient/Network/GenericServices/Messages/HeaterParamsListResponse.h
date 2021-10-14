#pragma once
#include <QObject>
#include "Services/QSerializer.h"
#include "Models/HeaterParams.h"

class HeaterParamsListResponse:public QSerializer
{
    Q_GADGET;
    QS_SERIALIZABLE;
public:
    HeaterParamsListResponse(){}
    virtual ~HeaterParamsListResponse(){}

    QS_COLLECTION_OBJECTS(QList, HeaterParams, ParamsList)
};
