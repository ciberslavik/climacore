#pragma once

#include <QObject>

#include <Network/INetworkService.h>

#include <Network/GenericServices/Messages/SensorsServiceResponse.h>

class SensorsService : public INetworkService
{
    Q_OBJECT
public:
    explicit SensorsService(QObject *parent = nullptr);

    void GetSensors();
signals:
    void SensorsReceived(SensorsServiceResponse *data);
    // INetworkService interface
public:
    QString ServiceName() override;
    QList<QString> Methods() override;
    void ProcessReply(NetworkReply *reply) override;
};

