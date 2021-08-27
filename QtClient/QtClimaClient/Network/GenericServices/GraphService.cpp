#include "GraphService.h"

#include <Network/GenericServices/Messages/GraphInfosRequest.h>
#include <Network/GenericServices/Messages/CreateGraphRequest.h>
#include <Frames/Graphs/GraphType.h>

GraphService::GraphService(QObject *parent) : INetworkService(parent)
{

}

void GraphService::TempInfosRequest()
{
    GraphInfosRequest *request = new GraphInfosRequest();
    request->method = "GetTemperatureGraphInfos";

    emit SendRequest(request);
}

void GraphService::CreateTemperatureProfile(ProfileInfo *info)
{
    NetworkRequest *request = new NetworkRequest();
    CreateGraphRequest *createRequest = new CreateGraphRequest();


    createRequest->GraphType = (int)GraphType::Temperature;
    createRequest->Info = *info;
    request->jsonrpc = "0.1a";
    request->service = "GraphProviderService";
    request->method = "CreateTemperatureGraph";
    request->params = createRequest->toJsonString();

    emit SendRequest(request);
}

QString GraphService::ServiceName()
{
    return "GraphProviderService";
}

QList<QString> GraphService::Methods()
{

}

void GraphService::ProcessReply(NetworkResponse *reply)
{
    if(reply->service=="GraphProviderService")
    {
        if(reply->method == "GetTemperatureGraphInfos")
        {
            qDebug() << "GetTemperatureGraphInfos:" << reply->result;
        }
    }
}
