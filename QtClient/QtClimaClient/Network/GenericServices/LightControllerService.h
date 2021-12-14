#pragma once
#include <QObject>
#include <Network/INetworkService.h>
#include <Network/GenericServices/Messages/CreateLightProfileResponse.h>
#include <Models/Timers/LightTimerProfile.h>
#include <Models/Timers/LightTimerProfileInfo.h>

class LightControllerService : public INetworkService
{
    Q_OBJECT
public:
    explicit LightControllerService(QObject *parent = nullptr);
    void CreateTimerProfile(const QString &profileName, const QString &description);
    void UpdateProfile(const LightTimerProfile &profile);
    void RemoveProfile(const QString &key);

    void GetProfileInfoList();
    void GetProfile(const QString &key);
    void GetCurrentProfileInfo();
    QString ServiceName() override{return "LightControllerService";}
    QList<QString> Methods() override;
    void ProcessReply(const NetworkResponse &reply) override;

signals:
    void ProfileCreated(const LightTimerProfile &profile);
    void PresetListReceived(const QList<LightTimerProfileInfo> &profiles);
    void ProfileReceived(const LightTimerProfile &profile);
};

