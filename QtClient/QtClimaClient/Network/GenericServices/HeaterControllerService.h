#pragma once

#include <Models/HeaterState.h>
#include <Network/INetworkService.h>
#include <QObject>

class HeaterControllerService : public INetworkService
{
    Q_OBJECT
public:
    explicit HeaterControllerService(QObject *parent = nullptr);
    void GetHeaterStates();
    void UpdateHeaterState(HeaterState state);
signals:
    void HeaterStateListReceived(QList<HeaterState> states);
    void HeaterStateUpdated(HeaterState state);
    // INetworkService interface
public:
    QString ServiceName() override{return "HeaterControllerService";};
    QList<QString> Methods() override{return QList<QString>();};
    void ProcessReply(NetworkResponse *reply) override;
};

