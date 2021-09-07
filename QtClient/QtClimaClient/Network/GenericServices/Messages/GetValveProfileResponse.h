#pragma once
#include <QObject>
#include <Network/NetworkResponse.h>
#include <Models/Graphs/ValueByValueProfile.h>

class GetValveProfileResponse:public NetworkResponse
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    GetValveProfileResponse(){}
    virtual ~GetValveProfileResponse(){}

    QS_OBJECT(ValueByValueProfile, Profile);
};
