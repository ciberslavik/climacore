#pragma once

#include <QObject>

#include <Network/INetworkService.h>

#include <Models/Graphs/GraphInfo.h>

class GraphService : public INetworkService
{
    Q_OBJECT
public:
    explicit GraphService(QObject *parent = nullptr);
    void TempInfosRequest();
signals:
    void TempInfosResponse(QList<GraphInfo> *infos);

    // INetworkService interface
public:
    QString ServiceName() override;
    QList<QString> Methods() override;
    void ProcessReply(NetworkReply *reply) override;
};

