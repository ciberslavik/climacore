#pragma once

#include <QObject>


#include <Network/NetworkReply.h>

class SensorsServiceResponse:public NetworkReply
{
    Q_GADGET
    QS_SERIALIZABLE
public:
        SensorsServiceResponse(){}
    QS_FIELD(QString, FrontTemperature)
    QS_FIELD(QString, RearTemperature)
    QS_FIELD(QString, OutdoorTemperature)
    QS_FIELD(QString, Humidity)
    QS_FIELD(QString, Presure)
    QS_FIELD(QString, Valve1)
    QS_FIELD(QString, Valve2)
};

