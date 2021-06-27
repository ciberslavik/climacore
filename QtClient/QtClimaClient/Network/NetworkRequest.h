#pragma once

#include <QObject>
#include <Services/QSerializer.h>


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
    QS_FIELD(QByteArray, RequestType)
    QS_FIELD(QByteArray, Data)
};

