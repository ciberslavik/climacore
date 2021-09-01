#pragma once

#include <Models/ProductionStatus.h>
#include <Network/INetworkService.h>
#include <QObject>

class ProductionService : public INetworkService
{
    Q_OBJECT
public:
    explicit ProductionService(QObject *parent = nullptr);
    void GetProductionState();
    void StartPreparing();
    void StartProduction();
    void StopProduction();
signals:
    void ProductionStateReceived(ProductionStatus status);
    // INetworkService interface
public:
    QString ServiceName(){return "ProductionService";}
    QList<QString> Methods();
    void ProcessReply(NetworkResponse *reply);
};

