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
    void StartPreparing(float temperature);
    void StartProduction();
    void StopProduction();
signals:
    void ProductionStateReceived(int state);
    void PreparingStarted(int state);
    void ProductionStopped(int state);
    void ProductionStarted(int state);

    void ProductionStateChanged(int newState);
    // INetworkService interface
public:
    QString ServiceName(){return "ProductionService";}
    QList<QString> Methods();
    void ProcessReply(NetworkResponse *reply);
};

