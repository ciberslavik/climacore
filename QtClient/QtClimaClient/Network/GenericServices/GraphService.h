#pragma once

#include <QObject>

#include <Network/INetworkService.h>

#include <Network/GenericServices/Messages/GetProfileResponse.h>
#include <Network/GenericServices/Messages/GraphInfosResponce.h>

class GraphService : public INetworkService
{
    Q_OBJECT
public:
    explicit GraphService(QObject *parent = nullptr);

    void GetTempInfos();
    void CreateTemperatureProfile(ProfileInfo *info);
    void GetTemperatureProfile(const QString &key);
    void UpdateTemperatureProfile(ValueByDayProfile *profile);

    void GetVentInfos();
    void CreateVentilationProfile(ProfileInfo *info);
    void GetVentilationProfile(const QString &key);
    //void UpdateVentilationProfile()

signals:
    void TempInfosResponse(QList<ProfileInfo> *infos);
    void TempProfileResponse(ValueByDayProfile *profile);

    void VentInfosResponse(QList<ProfileInfo> *infos);
    // INetworkService interface
public:
    QString ServiceName() override;
    QList<QString> Methods() override;
    void ProcessReply(NetworkResponse *reply) override;
};

