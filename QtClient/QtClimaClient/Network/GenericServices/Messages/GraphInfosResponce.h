#pragma once


#include <QObject>
#include <QDateTime>
#include <Network/NetworkResponse.h>
#include <Models/Graphs/GraphInfo.h>
class GraphInfosResponce : public NetworkResponse
{
    Q_GADGET
    QS_SERIALIZABLE
public:
    GraphInfosResponce();
    QS_FIELD(QString, GraphType)
    QS_COLLECTION_OBJECTS(QList, GraphInfo, Infos)
};

