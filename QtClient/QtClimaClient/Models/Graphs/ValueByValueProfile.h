#pragma once

#include <QObject>
#include <Services/QSerializer.h>
#include "ProfileInfo.h"
#include "ValueByValuePoint.h"

class ValueByValueProfile : public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    ValueByValueProfile(){}
    virtual ~ValueByValueProfile(){}
    QS_OBJECT(ProfileInfo, Info)
    QS_COLLECTION_OBJECTS(QList, ValueByValuePoint, Points)

private:
};

