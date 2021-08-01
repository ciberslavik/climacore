#pragma once

#include <QObject>

#include <Network/NetworkRequest.h>

class ServerInfoRequest : public NetworkRequest
{
    Q_GADGET
    QS_SERIALIZABLE
public:
    ServerInfoRequest()
    {
        jsonrpc = "0.1a";
        service = "ServerInfoService";
    }


};

