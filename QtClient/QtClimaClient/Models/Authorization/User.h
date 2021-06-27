#pragma once

#include <Services/QSerializer.h>
#include <QObject>

class User : public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE
public:
        User(){}
    QS_FIELD(QString, FirstName)
    QS_FIELD(QString, LastName)
    QS_FIELD(QString, PasswordHash)
    QS_FIELD(QString, Login)

};

