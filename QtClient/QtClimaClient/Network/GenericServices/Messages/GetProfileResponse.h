#pragma once
#include <QObject>
#include <Network/NetworkResponse.h>
#include <Models/Graphs/ValueByDayProfile.h>

class GetProfileResponse:public NetworkResponse
{
    Q_GADGET
    QS_SERIALIZABLE
public:

    QS_FIELD(int, ProfileType)
    QS_OBJECT(ValueByDayProfile, Profile)
};
