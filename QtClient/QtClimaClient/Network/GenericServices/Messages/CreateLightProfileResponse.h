#pragma once
#include <QObject>
#include "Services/QSerializer.h"
#include "Models/Timers/LightTimerProfile.h"

class CreateLightProfileResponse:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    CreateLightProfileResponse(){}
    virtual ~CreateLightProfileResponse(){}

    QS_OBJECT(LightTimerProfile, Profile);
};
