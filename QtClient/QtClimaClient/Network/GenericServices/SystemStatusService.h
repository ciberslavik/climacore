#pragma once

#include <QObject>

#include <Network/INetworkService.h>

#include <Models/SensorsData.h>
#include <Models/SystemState.h>

class SystemStatusService : public INetworkService
{
    Q_OBJECT
public:
    explicit SystemStatusService(QObject *parent = nullptr);
    SystemState GetSystemState();
    SensorsData GetSensors();
signals:

public slots:


    // INetworkService interface
public:
    QString ServiceName(){return "SystemStatusService";}
    QList<QString> Methods();
    void ProcessReply(NetworkReply *reply);

};

