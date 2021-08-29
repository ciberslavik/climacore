#pragma once
#include <QObject>
#include <Services/QSerializer.h>
#include <Models/Graphs/ProfileInfo.h>
#include <Models/Graphs/MinMaxByDayPoint.h>

class MinMaxByDayProfile:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE
public:
        MinMaxByDayProfile(){}
    virtual ~MinMaxByDayProfile(){}

    QS_OBJECT(ProfileInfo, Info)
    QS_COLLECTION_OBJECTS(QList, MinMaxByDayPoint, Points)
};
