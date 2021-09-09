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

    m_services = new QMap<QString, INetworkService*>();
    _instance = this;
}

void ApplicationWorker::RegisterNetworkService(INetworkService *service)
{
    if(m_services->contains(service->ServiceName()))
        return;
    connect(service, &INetworkService::SendRequest, this, &ApplicationWorker::onSendRequest);
    m_services->insert(service->ServiceName(), service);
}

INetworkService *ApplicationWorker::GetNetworkService(const QString &serviceName)
{
    if(m_services->contains(serviceName))
        return m_services->value(serviceName);

    return nullptr;
}

void ApplicationWorker::onConnctionEstabilished()
{
//        NetworkRequest *request = new NetworkRequest();
//        request->jsonrpc = "0.1a";
//        request->service = "ServerInfoService";
//        request->method = "GetServerVersion";
//        m_connection->SendRequest(request);
}

void ApplicationWorker::onReplyReceived(NetworkResponse *reply)
{
    qDebug()<<"Service:"<< reply->service << " method:" << reply->method << " rsult:" << reply->result;
    if(m_services->contains(reply->service))
    {
        m_services->value(reply->service)->ProcessReply(reply);
    }
}

void ApplicationWorker::onSendRequest(NetworkRequest *request)
{
    qDebug() << "OnSendRequest";
    m_connection->SendRequest(request);
}
