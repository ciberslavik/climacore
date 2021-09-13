#pragma once
#include <QObject>
#include <Services/QSerializer.h>
#include <Models/HeaterState.h>

class HeaterStateListResponse:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    HeaterStateListResponse(){}
    virtual ~HeaterStateListResponse(){}

    QS_COLLECTION_OBJECTS(QList, HeaterState, States)
};
