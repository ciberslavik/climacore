#pragma once
#include <QObject>
#include <Services/QSerializer.h>

class Request : public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE
public:
    QS_FIELD(QString, jsonrpc)
    QS_FIELD(QString, method)

};

