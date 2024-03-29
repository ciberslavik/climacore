#pragma once
#include <QObject>
#include <QMap>
#include <Services/QSerializer.h>
#include <Models/FanInfo.h>

class FanInfoListResponse: public QSerializer
{
    Q_GADGET;
    QS_SERIALIZABLE;
public:
    FanInfoListResponse(){}
    virtual ~FanInfoListResponse(){}

    QS_QT_DICT_OBJECTS(QMap, QString, FanInfo, Infos)
};
