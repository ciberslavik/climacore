#pragma once

#include <QObject>
#include <Services/QSerializer.h>
#include "ProfileInfo.h"
#include "ValueByDayPoint.h"

class ValueByDayProfile : public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE
public:
    QS_OBJECT(ProfileInfo, Info)
    QS_COLLECTION_OBJECTS(QList, ValueByDayPoint, Points)

private:
};

