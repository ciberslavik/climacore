#pragma once
#include <QObject>
#include <Services/QSerializer.h>

class RequestParams:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE
    public:

        QS_FIELD(QString, param1)
};
