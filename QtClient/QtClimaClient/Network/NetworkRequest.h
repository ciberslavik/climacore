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
    {
        jsonrpc = "0.1a";
    }
    virtual ~NetworkRequest()
    {

    }
    QS_FIELD(QString, jsonrpc)
    QS_FIELD(QString, service)
    QS_FIELD(QString, method)
    QS_FIELD(QString, params)
    //QS_OBJECT(RequestParams, params)
    QS_FIELD(int, id)

};

