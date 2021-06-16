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

    QS_FIELD(QString, ReplyType)
    QS_FIELD(QString, Data)

};

