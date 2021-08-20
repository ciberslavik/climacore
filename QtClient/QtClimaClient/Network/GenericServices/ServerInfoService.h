#pragma once

#include <QObject>

#include <Network/INetworkService.h>

class ServerInfoService : public INetworkService
{
    Q_OBJECT
public:
    explicit ServerInfoService(QObject *parent = nullptr);

    void GetServerInfo();
signals:


    // INetworkService interface
public:
    QString ServiceName() override;
    QList<QString> Methods() override;
    void ProcessReply(NetworkResponse *reply) override;

};

