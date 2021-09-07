#pragma once
#include <QObject>
#include <Network/NetworkRequest.h>
#include <Models/Graphs/MinMaxByDayProfile.h>

class UpdateMinMaxByDayProfileRequest:public NetworkRequest
{
    Q_GADGET
    QS_SERIALIZABLE
public:
        UpdateMinMaxByDayProfileRequest(){}
    virtual ~UpdateMinMaxByDayProfileRequest(){}

    QS_OBJECT(MinMaxByDayProfile, Profile)
};
