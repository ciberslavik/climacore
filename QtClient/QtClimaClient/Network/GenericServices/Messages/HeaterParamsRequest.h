#pragma once
#include <QObject>
#include "Services/QSerializer.h"
#include "Models/HeaterParams.h"

class HeaterParamsRequest:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    HeaterParamsRequest(){}
    virtual ~HeaterParamsRequest(){}
    QS_COLLECTION_OBJECTS(QList, HeaterParams, ParamsList)
};
