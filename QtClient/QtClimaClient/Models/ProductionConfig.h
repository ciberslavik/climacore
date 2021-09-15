#pragma once
#include <QObject>
#include <QDateTime>
#include <Services/QSerializer.h>

class ProductionConfig:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    ProductionConfig(){}
    virtual ~ProductionConfig(){}

    QS_FIELD(int, PlaceHeads)
    QS_FIELD(QDateTime, PlandingDate)
    QS_FIELD(QDateTime, StartDate)
};
