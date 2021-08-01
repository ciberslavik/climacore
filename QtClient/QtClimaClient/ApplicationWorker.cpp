#include "ApplicationWorker.h"
ApplicationWorker* ApplicationWorker::_instance = nullptr;

ApplicationWorker::ApplicationWorker(ClientConnection *connection, QObject *parent) :
    QObject(parent),
    m_connection(connection)
{

    connect(m_connection,
            &ClientConnection::ConnectionEstabilished,
            this,
            &ApplicationWorker::onConnctionEstabilished);

    connect(m_connection,
            &ClientConnection::ReplyReceived,
            this,
            &ApplicationWorker::onReplyReceived);

    _instance = this;
}

void ApplicationWorker::onConnctionEstabilished()
{
        NetworkRequest request;
        request.jsonrpc = "0.1a";
        request.service = "Clima.Communication.Services.ServerInfoService";
        request.method = "GetServerVersion";
        m_connection->SendRequest(&request);
}

void ApplicationWorker::onReplyReceived(const NetworkReply &reply)
{
    qDebug()<<"Service:"<< reply.service << " method:" << reply.method << " rsult:" << reply.result;
}
