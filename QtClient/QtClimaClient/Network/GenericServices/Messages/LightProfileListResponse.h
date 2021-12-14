#pragma once
#include <QObject>
#include <Network/NetworkResponse.h>
#include <Models/Timers/LightTimerProfileInfo.h>

class LightProfileListResponse:public NetworkResponse
{
    Q_GADGET
    QS_SERIALIZABLE

public:
    LightProfileListResponse(){    }
    virtual ~LightProfileListResponse(){}

    QS_COLLECTION_OBJECTS(QList, LightTimerProfileInfo, Profiles);
};
