#pragma once
#include <QObject>
#include <Network/NetworkResponse.h>

class LightPresetListResponse:public NetworkResponse
{
    Q_GADGET
    QS_SERIALIZABLE

public:
    LightPresetListResponse()
    {

    }

};
