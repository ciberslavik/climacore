#pragma once

#include <QObject>

#include <Network/INetworkService.h>

class SystemStatusService : public QObject, public INetworkService
{
    Q_OBJECT
public:
    explicit SystemStatusService(QObject *parent = nullptr);

signals:


    // INetworkService interface
public:
    QString ServiceName(){return "SystemStatusService";}
    QList<QString> Methods();
};

