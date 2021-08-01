#pragma once

#include <QObject>
#include <Services/QSerializer.h>

class NetworkReply : public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE
public:
    explicit NetworkReply()
    {

    }

    QS_FIELD(QString, id)
    QS_FIELD(QString, jsonrpc)
    QS_FIELD(QString, service)
    QS_FIELD(QString, method)
    QS_FIELD(QString, result);
};

