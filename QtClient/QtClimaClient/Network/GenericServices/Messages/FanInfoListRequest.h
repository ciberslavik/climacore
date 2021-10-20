#pragma once
#include <QObject>
#include <QMap>
#include <Services/QSerializer.h>
#include <Models/FanInfo.h>

class FanInfoListRequest: public QSerializer
{
    Q_GADGET;
    QS_SERIALIZABLE;
public:
    FanInfoListRequest(){}
    virtual ~FanInfoListRequest(){}

    QS_QT_DICT_OBJECTS(QMap, QString, FanInfo, Infos)
};
