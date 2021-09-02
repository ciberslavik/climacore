#pragma once
#include <QObject>
#include <QList>
#include <Services/QSerializer.h>
#include <Models/FanState.h>

class FanStateListResponse:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    FanStateListResponse(){}
    virtual ~FanStateListResponse(){}

    QS_COLLECTION_OBJECTS(QList, FanState, States)
};
