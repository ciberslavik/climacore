#pragma once

#include <QObject>

#include <Network/INetworkService.h>

#include <Models/Graphs/ProfileInfo.h>

class GraphService : public INetworkService
{
    Q_OBJECT
public:
    explicit GraphService(QObject *parent = nullptr);
    void TempInfosRequest();
    void CreateTemperatureProfile(ProfileInfo *info);
signals:
    void TempInfosResponse(QList<ProfileInfo> *infos);

    // INetworkService interface
public:
    QString ServiceName() override;
    QList<QString> Methods() override;
    void ProcessReply(NetworkResponse *reply) override;
};

