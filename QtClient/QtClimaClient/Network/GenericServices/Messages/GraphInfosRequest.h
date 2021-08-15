#pragma once

#include <QObject>
#include <Network/NetworkRequest.h>

class GraphInfosRequest:public NetworkRequest
{
    Q_GADGET
    QS_SERIALIZABLE
public:
    GraphInfosRequest()
    {
        jsonrpc = "0.1a";
        service = "GraphProviderService";
    }
};

