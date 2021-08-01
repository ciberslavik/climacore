#pragma once

#include <QObject>
#include <Services/QSerializer.h>
#include "RequestParams.h"

class NetworkRequest : public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE
public:
    explicit NetworkRequest()
    {}
    virtual ~NetworkRequest()
    {

    }
    QS_FIELD(QString, jsonrpc)
    QS_FIELD(QString, service)
    QS_FIELD(QString, method)
    QS_OBJECT(RequestParams, params)
    QS_FIELD(int, id)

};

