#pragma once
#include <QObject>
#include <Services/QSerializer.h>

class GetProfileRequest:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE
public:

    QS_FIELD(int, ProfileType)
    QS_FIELD(QString, ProfileKey)
};
