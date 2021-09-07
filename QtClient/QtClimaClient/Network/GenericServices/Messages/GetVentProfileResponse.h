#pragma once
#include <QObject>
#include <Network/NetworkResponse.h>
#include <Models/Graphs/MinMaxByDayProfile.h>

class GetVentProfileResponse:public NetworkResponse
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    GetVentProfileResponse(){}
    virtual ~GetVentProfileResponse(){}

    QS_OBJECT(MinMaxByDayProfile, Profile);
};
