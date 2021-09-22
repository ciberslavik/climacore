#pragma once

#include <Services/QSerializer.h>
#include <Models/Authorization/User.h>

#include <QObject>

class ReplyUserList : public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE
public:
    ReplyUserList()
    {}
    virtual ~ReplyUserList(){}
    QS_FIELD(QString, RequestType)
    QS_COLLECTION_OBJECTS(QList, User, Users)
    QS_OBJECT(User, SingleUser)
};

