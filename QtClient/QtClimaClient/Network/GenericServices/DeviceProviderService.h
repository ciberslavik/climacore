#pragma once

#include <Models/RelayInfo.h>
#include <Network/INetworkService.h>
#include <QObject>

class DeviceProviderService : public INetworkService
{
    Q_OBJECT
public:
    explicit DeviceProviderService(QObject *parent = nullptr);
    void GetRelayList();
    // INetworkService interface
signals:
    void RelayListReceived(QList<RelayInfo> relayInfos);
public:
    QString ServiceName() override{return "DeviceProviderService";}
    QList<QString> Methods() override{return QList<QString>();}
    void ProcessReply(const NetworkResponse &reply) override;
};

