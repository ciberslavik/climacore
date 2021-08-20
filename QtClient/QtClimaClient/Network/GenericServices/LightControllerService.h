#pragma once


#include <QObject>

#include <Network/INetworkService.h>

class LightControllerService : public INetworkService
{
public:
    explicit LightControllerService(QObject *parent = nullptr);


    // INetworkService interface
public:
    QString ServiceName() override{return "LightControllerService";}
    QList<QString> Methods() override;
    void ProcessReply(NetworkResponse *reply) override;
};

