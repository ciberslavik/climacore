#pragma once
#include <QObject>
#include "Services/QSerializer.h"

class LightTimerProfileInfo:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    LightTimerProfileInfo(){}
    virtual ~LightTimerProfileInfo(){}
    QS_FIELD(QString, Key)
    QS_FIELD(QString, Name)
    QS_FIELD(QString, Description)
};
