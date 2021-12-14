#pragma once
#include <QObject>
#include "Services/QSerializer.h"
#include "Models/Timers/LightTimerProfile.h"

class LightProfileResponse:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    LightProfileResponse(){}
    virtual ~LightProfileResponse(){}

    QS_OBJECT(LightTimerProfile, Profile)
};
