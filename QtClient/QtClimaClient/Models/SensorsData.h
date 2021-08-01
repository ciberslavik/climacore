#pragma once

#include <Services/QSerializer.h>
#include <QObject>

class SensorsData : public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE

public:
    SensorsData(){}
    QS_FIELD(QString, FrontTemperature)
    QS_FIELD(QString, FearTemperature)
    QS_FIELD(QString, OutdoorTemperature)
    QS_FIELD(QString, Humidity)
    QS_FIELD(QString, Presure)
};

