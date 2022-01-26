#pragma once
#include <QObject>
#include "Services/QSerializer.h"

class LightStatusResponse:public QSerializer
{
    Q_GADGET;
    QS_SERIALIZABLE;
public:
    LightStatusResponse(){}
    virtual ~LightStatusResponse(){}

    QS_FIELD(QString, CurrentProfileName)
    QS_FIELD(QString, CurrentProfileKey)
    QS_FIELD(bool, IsAuto)
    QS_FIELD(bool, IsOn)
};
