#pragma once

#include "Network/INetworkService.h"
#include <QObject>
#include <Network/GenericServices/Messages/FanInfoListRsponse.h>
#include <Models/VentControllerState.h>
#include <Models/FanControlsEnums.h>
class VentilationService : public INetworkService
{
    Q_OBJECT
public:
    explicit VentilationService(QObject *parent = nullptr);

    void GetControllerState();
    void GetFanInfoList();
    void UpdateFanInfoList(const QMap<QString, FanInfo> &infos);
    void GetFanInfo(const QString &fanKey);

    void SetFanState(const QString &key, const FanStateEnum &state);
    void SetFanState(const QString &key, const FanStateEnum &state, const float &analogPower);
    void SetFanMode(const QString &key, const FanModeEnum &state);

    void CreateOrUpdateFan(const FanInfo &info);
    void RemoveFan(const QString &fanKey);

    void GetValveState();
    void UpdateValveState(bool isManual, float setPoint);

    void GetMineState();
    void UpdateMineState(bool isManual, float setPoint);

    void GetVentilationStatus();

    void ResetAlarms();
signals:
    void FanInfoListReceived(QMap<QString, FanInfo> response);
    void FanInfoReceived(FanInfo info);
    void ControllerStateReceived(VentControllerState state);
    void CreateOrUpdateComplete();

    void ValveStateReceived(bool isManual, float currPos, float setPoint);
    void ValveStateUpdated(bool isManual, float currPos, float setPoint);

    void MineStateReceived(bool isManual, float currPos, float setPoint);
    void MineStateUpdated(bool isManual, float currPos, float setPoint);

    void VentilationStatusReceived(float valvePos,float valveSetpoint,float minePos, float mineSetpoint,float ventilationSp);
    // INetworkService interface
public:
    QString ServiceName(){return "VentilationControllerService";}
    QList<QString> Methods(){return QList<QString>();}
    void ProcessReply(const NetworkResponse &reply);
};

