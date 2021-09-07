#pragma once


#include <QObject>
#include <QDateTime>
#include <Network/NetworkResponse.h>
#include <Models/Graphs/ProfileInfo.h>
class GraphInfosResponce : public NetworkResponse
{
    Q_GADGET
    QS_SERIALIZABLE
public:
    GraphInfosResponce()
    {

    }
    QS_FIELD(int, GraphType)
    QS_COLLECTION_OBJECTS(QList, ProfileInfo, Infos)
};

