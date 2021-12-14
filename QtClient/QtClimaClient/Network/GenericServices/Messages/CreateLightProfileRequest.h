#pragma once
#include <QObject>
#include "Services/QSerializer.h"

class CreateLightProfileRequest:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    CreateLightProfileRequest(){}
    virtual ~CreateLightProfileRequest(){}

    QS_FIELD(QString, ProfileName)
    QS_FIELD(QString, Description);
};
