#pragma once

#include <QObject>

#include <Network/INetworkService.h>

#include <Models/SensorsData.h>
#include <Models/SystemState.h>

#include <Network/GenericServices/Messages/ClimatStatusResponse.h>
#include <Network/GenericServices/Messages/TemperatureStateResponse.h>

class SystemStatusService : public INetworkService
{
    Q_OBJECT
public:
    explicit SystemStatusService(QObject *parent = nullptr);

signals:
    void onClimatStatusRecv(ClimatStatusResponse *response);
    void onTemperatureStatusRecv(TemperatureStateResponse *response);
public slots:
    void getClimatStatus();
    void getTemperatureStatus();
    // INetworkService interface
public:
    QString ServiceName()override{return "SystemStatusService";}
    QList<QString> Methods() override;
    void ProcessReply(const NetworkResponse &reply) override;

};

