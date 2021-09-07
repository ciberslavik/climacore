#pragma once

#include <QObject>

#include <Network/INetworkService.h>

#include <Network/GenericServices/Messages/GraphInfosRequest.h>
#include <Network/GenericServices/Messages/GraphInfosResponce.h>

#include <Network/GenericServices/Messages/CreateGraphRequest.h>
#include <Network/GenericServices/Messages/GetVentProfileResponse.h>
#include <Network/GenericServices/Messages/GetValveProfileResponse.h>
#include <Network/GenericServices/Messages/GetProfileRequest.h>
#include <Network/GenericServices/Messages/UpdateValueByDayProfileRequest.h>
#include <Network/GenericServices/Messages/UpdateMinMaxByDayProfileRequest.h>
#include <Network/GenericServices/Messages/UpdateValueByValueProfileRequest.h>
#include <Frames/Graphs/ProfileType.h>

class GraphService : public INetworkService
{
    Q_OBJECT
public:
    explicit GraphService(QObject *parent = nullptr);

    void GetTempInfos();
    void CreateTemperatureProfile(ProfileInfo info);
    void GetTemperatureProfile(const QString &key);
    void UpdateTemperatureProfile(ValueByDayProfile profile);

    void GetVentInfos();
    void CreateVentilationProfile(ProfileInfo info);
    void GetVentilationProfile(const QString &key);
    void UpdateVentilationProfile(MinMaxByDayProfile profile);

    void GetValveInfos();
    void CreateValveProfile(ProfileInfo info);
    void GetValveProfile(const QString &key);
    void UpdateValveProfile(ValueByValueProfile profile);


signals:
    void TempInfosResponse(QList<ProfileInfo> infos);
    void TempProfileResponse(ValueByDayProfile profile);

    void VentInfosResponse(QList<ProfileInfo> infos);
    void VentProfileResponse(MinMaxByDayProfile profile);

    void ValveInfosResponse(QList<ProfileInfo> infos);
    void ValveProfileResponse(ValueByValueProfile profile);
    // INetworkService interface
public:
    QString ServiceName() override;
    QList<QString> Methods() override;
    void ProcessReply(NetworkResponse *reply) override;

private:

};

