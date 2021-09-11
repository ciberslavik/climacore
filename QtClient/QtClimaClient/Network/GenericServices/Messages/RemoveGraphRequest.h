#pragma once
#include <QObject>
#include <Services/QSerializer.h>

class RemoveGraphRequest:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    RemoveGraphRequest(){}
    virtual ~RemoveGraphRequest(){}

    QS_FIELD(int, GraphType)
    QS_FIELD(QString, Key)
};
