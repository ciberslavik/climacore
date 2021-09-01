#pragma once

#include "Network/INetworkService.h"
#include <QObject>
#include <Network/GenericServices/Messages/VentilationStateResponse.h>

class VentilationService : public INetworkService
{
    Q_OBJECT
public:
    explicit VentilationService(QObject *parent = nullptr);

    void GetFanStates();

signals:
    void FanStatesReceived(VentilationStateResponse *response);
    // INetworkService interface
public:
    QString ServiceName(){return "ventilationService";}
    QList<QString> Methods(){return QList<QString>();}
    void ProcessReply(NetworkResponse *reply);
};

