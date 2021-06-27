#pragma once

#include <QObject>
#include <QUuid>

class Session : public QObject
{
    Q_OBJECT
    Q_PROPERTY(QUuid SessionId READ SessionId WRITE setSessionId)

    QUuid m_SessionId;

public:
    explicit Session(QObject *parent = nullptr);

    long Send(char *buffer, int offset, int size);
    const QUuid &SessionId() const;
    void setSessionId(const QUuid &newSessionId);

signals:
    void DataReceived();
};

