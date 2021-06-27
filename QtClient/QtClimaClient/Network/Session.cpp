#include "Session.h"

Session::Session(QObject *parent) : QObject(parent)
{

}

const QUuid &Session::SessionId() const
{
    return m_SessionId;
}

void Session::setSessionId(const QUuid &newSessionId)
{
    m_SessionId = newSessionId;
}
