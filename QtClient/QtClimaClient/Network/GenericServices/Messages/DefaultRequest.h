#pragma once
#include <Network/NetworkRequest.h>
#include <QObject>
#include <Services/QSerializer.h>

class DefaultRequest:public NetworkRequest
{
    Q_GADGET
    QS_SERIALIZABLE
public:
    DefaultRequest()
    {
        jsonrpc = "0.1a";
    }
};
