#pragma once
#include <QObject>
#include <Services/QSerializer.h>

class RelayInfo:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    RelayInfo(){}
    virtual ~RelayInfo(){}

    QS_FIELD(QString, Key)
    QS_FIELD(QString, Name)
    QS_FIELD(bool, CurrentState)
};
