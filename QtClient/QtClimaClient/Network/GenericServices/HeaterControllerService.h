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
    void GetHeaterParams();
    void UpdateHeaterParams(QList<HeaterParams> heaterParams);
    void UpdateHeaterState(const QString &key, HeaterState state);
signals:
    void HeaterStateListReceived(float setpoint, QList<HeaterState> states);
    void HeaterParamsListReceived(QList<HeaterParams> heaterParams);
    void HeaterParamsUpdated(QList<HeaterParams> heaterParams);
    void HeaterStateUpdated(HeaterState state);
    // INetworkService interface
public:
    QString ServiceName() override{return "HeaterControllerService";};
    QList<QString> Methods() override{return QList<QString>();};
    void ProcessReply(NetworkResponse *reply) override;
};

