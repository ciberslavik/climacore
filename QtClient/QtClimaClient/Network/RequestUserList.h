#pragma once

#include <Services/QSerializer.h>
#include <QObject>

class RequestUserList : public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE
public:
     RequestUserList()
    {

    }
    QS_FIELD(QString, RequestType)
    QS_FIELD(QString, Parameters)
};

