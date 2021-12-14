#include "ServerInfoService.h"

#include <Network/GenericServices/Messages/ServerInfoRequest.h>

ServerInfoService::ServerInfoService(QObject *parent) : INetworkService(parent)
{

}

void ServerInfoService::GetServerInfo()
{
    ServerInfoRequest *request = new ServerInfoRequest();
    request->method = "GetServerVersion";

    emit SendRequest(request);
}

QString ServerInfoService::ServiceName()
{
    return "ServerInfoService";
}

QList<QString> ServerInfoService::Methods()
{
    return QList<QString>();
}

void ServerInfoService::ProcessReply(const NetworkResponse &reply)
{
    qDebug()<<"ServerInfo: Process reply";
}

