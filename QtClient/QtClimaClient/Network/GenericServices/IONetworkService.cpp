#include "IONetworkService.h"

#include <Network/GenericServices/Messages/DefaultRequest.h>
#include <Network/GenericServices/Messages/PinInfoResponse.h>

IONetworkService::IONetworkService(QObject *parent) : INetworkService(parent)
{

}

void IONetworkService::getPinInfos()
{
    NetworkRequest *request = new NetworkRequest();
    request->service = "IONetworkService";
    request->method = "GetPinInfos";
    request->params = DefaultRequest().toJsonString();

    emit SendRequest(request);
}


QString IONetworkService::ServiceName()
{
    return "IONetworkService";
}

QList<QString> IONetworkService::Methods()
{
    return QList<QString>();
}

void IONetworkService::ProcessReply(const NetworkResponse &reply)
{
    if(reply.service == "IONetworkService")
    {
        if(reply.method == "GetPinInfos")
        {
            PinInfoResponse resp;
            resp.fromJson(reply.result.toUtf8());
            emit onPinInfosReceived(resp.Infos);
        }
    }
}
