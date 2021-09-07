#pragma once
#include <QObject>
#include <Network/NetworkRequest.h>
#include <Models/Graphs/ValueByValueProfile.h>

class UpdateValueByValueProfileRequest:public NetworkRequest
{
    Q_GADGET
    QS_SERIALIZABLE
public:
        UpdateValueByValueProfileRequest(){}
    virtual ~UpdateValueByValueProfileRequest(){}

    QS_OBJECT(ValueByValueProfile, Profile)
};
