#pragma once

#include "Network/INetworkService.h"
#include <QObject>

class VentilationService : public INetworkService
{
    Q_OBJECT
public:
    explicit VentilationService(QObject *parent = nullptr);

    // INetworkService interface
public:
    QString ServiceName(){return "ventilationService";}
    QList<QString> Methods(){return QList<QString>();}
    void ProcessReply(NetworkResponse *reply);
};

