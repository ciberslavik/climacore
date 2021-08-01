#pragma once

#include "NetworkReply.h"
#include "NetworkRequest.h"

#include <QObject>

class INetworkService : public QObject
{
    Q_OBJECT
public:
    INetworkService(QObject *parent = nullptr):QObject(parent)
    {

    }
    virtual QString ServiceName() = 0;
    virtual QList<QString> Methods() = 0;
    virtual void ProcessReply(NetworkReply *reply) = 0;
signals:
    void SendRequest(NetworkRequest *request);
};

