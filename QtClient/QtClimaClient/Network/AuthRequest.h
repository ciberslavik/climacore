#pragma once

#include <Services/QSerializer.h>
#include <QObject>

class AuthRequest : public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE

public:
    AuthRequest();
    QS_FIELD(QString, UserName)
    QS_FIELD(QByteArray, PasswordHash)
};

