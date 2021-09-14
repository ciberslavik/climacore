#pragma once

#include "Network/INetworkService.h"
#include <QObject>
#include <Network/GenericServices/Messages/FanStateListResponse.h>
#include <Network/GenericServices/Messages/FanInfoListRsponse.h>
#include <Models/VentControllerState.h>

class VentilationService : public INetworkService
{
    Q_OBJECT
public:
    explicit VentilationService(QObject *parent = nullptr);
    void GetControllerState();
    void GetFanStateList();
    void GetFanInfoList();
    void UpdateFanState(const FanState &state);
    void CreateOrUpdateFan(const FanInfo &info);
    void RemoveFan(const QString &fanKey);
signals:
    void FanStateListReceived(QList<FanState> response);
    void FanStateUpdated(FanState state);
    void FanInfoListReceived(QList<FanInfo> response);
    void ControllerStateReceived(VentControllerState state);
    void CreateOrUpdateComplete();
    // INetworkService interface
public:
    QString ServiceName(){return "VentilationControllerService";}
    QList<QString> Methods(){return QList<QString>();}
    void ProcessReply(NetworkResponse *reply);
};

