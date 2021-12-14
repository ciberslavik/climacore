#pragma once

#include <QObject>

#include <Network/INetworkService.h>

#include <Models/LivestockOperation.h>
#include <Models/LivestockState.h>

class LivestockService : public INetworkService
{
    Q_OBJECT
public:
    explicit LivestockService(QObject *parent = nullptr);

    void GetLivestockState();
    void Plant(const int &heads, const QDateTime &date);
    void Kill(const int &heads, const QDateTime &date);
    void Refraction(const int &heads, const QDateTime &date);
    void Death(const int &heads, const QDateTime &date);
    void GetOpList();
signals:
    void LivestockStateReceived(LivestockState state);

    void PlantedComplete();
    void KillComlete();
    void DeathComplete();
    void RefractionComplete();
    void OpListReceived(const QList<LivestockOperation> &operations);
    void LivestockUpdated(LivestockState state);
    // INetworkService interface
public:
    QString ServiceName() override{return "LivestockService";}
    QList<QString> Methods() override{return QList<QString>();}
    void ProcessReply(const NetworkResponse &reply) override;
};

