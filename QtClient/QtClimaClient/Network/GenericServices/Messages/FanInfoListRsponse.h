#pragma once
#include <QObject>
#include <QList>
#include <Services/QSerializer.h>
#include <Models/FanInfo.h>

class FanInfoListResponse: public QSerializer
{
    Q_GADGET;
    QS_SERIALIZABLE;
public:
    FanInfoListResponse(){}
    virtual ~FanInfoListResponse(){}

    QS_COLLECTION_OBJECTS(QList, FanInfo, Infos)
};
