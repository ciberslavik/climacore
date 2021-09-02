#pragma once

#include "Network/INetworkService.h"
#include <QObject>
#include <Network/GenericServices/Messages/FanStateListResponse.h>
#include <Network/GenericServices/Messages/FanInfoListRsponse.h>

class VentilationService : public INetworkService
{
    Q_OBJECT
public:
    explicit VentilationService(QObject *parent = nullptr);

    void GetFanStateList();
    void GetFanInfoList();
    void CreateOrUpdateFan(const FanInfo &info);
    void RemoveFan(const QString &fanKey);
signals:
    void FanStateListReceived(QList<FanState> response);
    void FanInfoListReceived(QList<FanInfo> response);
    // INetworkService interface
public:
    QString ServiceName(){return "ventilationService";}
    QList<QString> Methods(){return QList<QString>();}
    void ProcessReply(NetworkResponse *reply);
};

