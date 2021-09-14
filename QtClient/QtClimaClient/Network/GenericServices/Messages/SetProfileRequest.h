#pragma once
#include <QObject>
#include<Services/QSerializer.h>

class SetProfileRequest:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    SetProfileRequest(){}
    virtual ~SetProfileRequest(){}

    QS_FIELD(QString, ProfileKey)
};
