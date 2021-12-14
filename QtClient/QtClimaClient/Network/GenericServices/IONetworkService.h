#pragma once

#include <Models/PinInfo.h>
#include <Network/INetworkService.h>
#include <QObject>

class IONetworkService : public INetworkService
{
    Q_OBJECT
public:
    explicit IONetworkService(QObject *parent = nullptr);
    void getPinInfos();
signals:
    void onPinInfosReceived(const QList<PinInfo> &infos);

    // INetworkService interface
public:
    virtual QString ServiceName() override;
    virtual QList<QString> Methods() override;
    virtual void ProcessReply(const NetworkResponse &reply) override;
};

