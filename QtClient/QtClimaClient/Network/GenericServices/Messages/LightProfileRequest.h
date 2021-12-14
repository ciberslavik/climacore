#pragma once
#include <QObject>
#include "Services/QSerializer.h"
#include "Models/Timers/LightTimerProfile.h"

class LightProfileRequest:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    LightProfileRequest(){}
    virtual ~LightProfileRequest(){}

    QS_OBJECT(LightTimerProfile, Profile)
};
