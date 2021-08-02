#pragma once

#include <QObject>

#include <Network/NetworkRequest.h>

class SensorsServiceRequest:public NetworkRequest
{
    Q_GADGET
    QS_SERIALIZABLE
public:
    SensorsServiceRequest()
    {
        jsonrpc = "0.1a";
        service = "SensorsService";
    }
};

