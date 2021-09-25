#pragma once

#include "Network/INetworkService.h"
#include <QObject>
#include <Network/GenericServices/Messages/FanStateListResponse.h>
#include <Network/GenericServices/Messages/FanInfoListRsponse.h>
#include <Models/VentControllerState.h>
#include <Models/FanControlsEnums.h>
class VentilationService : public INetworkService
{
    Q_OBJECT
public:
    explicit VentilationService(QObject *parent = nullptr);
    void GetControllerState();
    void GetFanStateList();
    void GetFanInfoList();
    void SetFanState(const QString &key, const FanStateEnum &state);
    void CreateOrUpdateFan(const FanInfo &info);
    void RemoveFan(const QString &fanKey);

    void GetValveState();
    void UpdateValveState(bool isManual, float setPoint);

    void GetMineState();
    void UpdateMineState(bool isManual, float setPoint);

signals:
    void FanStateListReceived(QList<FanState> response);
    void FanStateUpdated(FanState state);
    void FanInfoListReceived(QList<FanInfo> response);
    void ControllerStateReceived(VentControllerState state);
    void CreateOrUpdateComplete();

    void ValveStateReceived(bool isManual, float currPos, float setPoint);
    void ValveStateUpdated(bool isManual, float currPos, float setPoint);

    void MineStateReceived(bool isManual, float currPos, float setPoint);
    void MineStateUpdated(bool isManual, float currPos, float setPoint);
    // INetworkService interface
public:
    QString ServiceName(){return "VentilationControllerService";}
    QList<QString> Methods(){return QList<QString>();}
    void ProcessReply(NetworkResponse *reply);
};

