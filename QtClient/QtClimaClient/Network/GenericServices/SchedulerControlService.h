#pragma once

#include <Network/INetworkService.h>
#include <Models/SchedulerInfo.h>
#include <QObject>

class SchedulerControlService : public INetworkService
{
    Q_OBJECT
public:
    explicit SchedulerControlService(QObject *parent = nullptr);

    void SetTemperatureProfile(const QString &profileKey);
    void SetVentilationProfile(const QString &profileKey);
    void SetValveProfile(const QString &profileKey);
    void SetMineProfile(const QString &profileKey);

    void GetSchedulerInfo();
signals:
    void SchedulerInfoReceived(SchedulerInfo info);
    void SchedulerUpdated();
    // INetworkService interface
public:
    QString ServiceName() override{return "SchedulerControlService";}
    QList<QString> Methods() override {return QList<QString>();}
    void ProcessReply(NetworkResponse *reply) override;
};

