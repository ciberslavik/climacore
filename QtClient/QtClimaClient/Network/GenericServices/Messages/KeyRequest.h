#pragma once
#include <QObject>
#include "Services/QSerializer.h"

class KeyRequest:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    KeyRequest(){}
    KeyRequest(const QString &key)
    {
        Key = key;
    }
    virtual ~KeyRequest(){}

    QS_FIELD(QString, Key)
};
