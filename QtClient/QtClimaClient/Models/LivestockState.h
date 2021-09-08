#pragma once
#include <QObject>
#include <Services/QSerializer.h>

class LivestockState:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    LivestockState(){}
    virtual ~LivestockState(){}

    QS_FIELD(int, CurrentHeads)
    QS_FIELD(int, CurrentDay)
    QS_FIELD(int, TotalPlantedHeads)
    QS_FIELD(int, TotalDeadHeads)
    QS_FIELD(int, TotalKilledHeads)
    QS_FIELD(int, TotalRefracted )
};
