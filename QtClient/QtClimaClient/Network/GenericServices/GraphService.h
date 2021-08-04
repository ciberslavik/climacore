#pragma once

#include <QObject>

#include <Network/INetworkService.h>

class GraphService : public INetworkService
{
    Q_OBJECT
public:
    explicit GraphService(QObject *parent = nullptr);
    void TempInfosRequest();
signals:
    void TempInfosResponse();

    // INetworkService interface
public:
    QString ServiceName() override;
    QList<QString> Methods() override;
    void ProcessReply(NetworkReply *reply) override;
};

