#pragma once
#include <QObject>
#include <Network/NetworkRequest.h>
#include <Models/Graphs/ValueByDayProfile.h>

class UpdateValueByDayProfileRequest:public NetworkRequest
{
    Q_GADGET
    QS_SERIALIZABLE
public:
        UpdateValueByDayProfileRequest(){}
    virtual ~UpdateValueByDayProfileRequest(){}

    QS_OBJECT(ValueByDayProfile, Profile)
};
