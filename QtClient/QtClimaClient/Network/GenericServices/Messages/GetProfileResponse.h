#pragma once
#include <QObject>
#include <Network/NetworkResponse.h>
#include <Models/Graphs/ValueByDayProfile.h>

class GetTempProfileResponse:public NetworkResponse
{
    Q_GADGET
    QS_SERIALIZABLE
public:

    QS_OBJECT(ValueByDayProfile, Profile);
};
