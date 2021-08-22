#pragma once

#include <Services/QSerializer.h>
#include <QObject>
#include <QDateTime>

class ProfileInfo : public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE
public:
    ProfileInfo(){}
    virtual ~ProfileInfo(){}
    QS_FIELD(QString, Key)
    QS_FIELD(QString, Name)
    QS_FIELD(QString, Description)
    QS_FIELD(QDateTime, CreationTime)
    QS_FIELD(QDateTime, ModifiedTime)
};

