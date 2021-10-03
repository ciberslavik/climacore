#pragma once

#include <Network/INetworkService.h>
#include <Models/SchedulerInfo.h>
#include <Models/SchedulerProcessInfo.h>
#include <Models/SchedulerProfilesInfo.h>
#include <Models/VentilationParams.h>
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

    void GetProfilesInfo();
    void GetProcessInfo();

    void GetVentilationParams();
    void UpdateVentilationParams(VentilationParams parameters);
signals:
    void SchedulerProfilesInfoReceived(SchedulerProfilesInfo profilesInfo);
    void SchedulerProcessInfoReceived(SchedulerProcessInfo processInfo);
    void SchedulerUpdated();
    void VentilationParamsReceived(VentilationParams params);
    void VentilationParamsUpdated(VentilationParams params);
    // INetworkService interface
public:
    QString ServiceName() override{return "SchedulerControlService";}
    QList<QString> Methods() override {return QList<QString>();}
    void ProcessReply(NetworkResponse *reply) override;
};

