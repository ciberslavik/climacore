#pragma once

#include <Models/ProductionState.h>
#include <Network/INetworkService.h>
#include <QObject>

class ProductionService : public INetworkService
{
    Q_OBJECT
public:
    explicit ProductionService(QObject *parent = nullptr);
    void GetProductionState();
    void StartPreparing(float temperature, QDateTime startDate);
    void StartProduction(int placeHeads, QDateTime plandingDate, QDateTime startDate);
    void StopProduction();
signals:
    void PreparingStarted(int state);
    void ProductionStopped(int state);
    void ProductionStarted(int state);

    void ProductionStateChanged(ProductionState newState);
    // INetworkService interface
public:
    QString ServiceName(){return "ProductionService";}
    QList<QString> Methods();
    void ProcessReply(const NetworkResponse &reply);
};

