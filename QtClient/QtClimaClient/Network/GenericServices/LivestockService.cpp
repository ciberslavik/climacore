#include "LivestockService.h"

#include <Network/GenericServices/Messages/LivestockOperationRequest.h>
#include <Network/GenericServices/Messages/LivestockStateResponse.h>

LivestockService::LivestockService(QObject *parent) : INetworkService(parent)
{

}

void LivestockService::GetLivestockState()
{
    NetworkRequest *request = new NetworkRequest();

    request->jsonrpc = "0.1a";
    request->service = "LivestockService";
    request->method = "GetState";

    emit SendRequest(request);
}

void LivestockService::Plant(const int &heads, const QDateTime &date)
{
    NetworkRequest *request = new NetworkRequest();

    request->jsonrpc = "0.1a";
    request->service = "LivestockService";
    request->method = "PlantHeads";

    LivestockOperationRequest op;
    op.HeadsCount = heads;
    op.OperationDate = date;

    request->params = op.toJsonString();

    emit SendRequest(request);
}

void LivestockService::Kill(const int &heads, const QDateTime &date)
{
    NetworkRequest *request = new NetworkRequest();

    request->jsonrpc = "0.1a";
    request->service = "LivestockService";
    request->method = "KillHeads";

    LivestockOperationRequest op;
    op.HeadsCount = heads;
    op.OperationDate = date;

    request->params = op.toJsonString();

    emit SendRequest(request);
}

void LivestockService::Refraction(const int &heads, const QDateTime &date)
{
    NetworkRequest *request = new NetworkRequest();

    request->jsonrpc = "0.1a";
    request->service = "LivestockService";
    request->method = "RefractHeads";
    LivestockOperationRequest op;
    op.HeadsCount = heads;
    op.OperationDate = date;

    request->params = op.toJsonString();

    emit SendRequest(request);
}

void LivestockService::Death(const int &heads, const QDateTime &date)
{
    NetworkRequest *request = new NetworkRequest();

    request->jsonrpc = "0.1a";
    request->service = "LivestockService";
    request->method = "DeathHeads";
    LivestockOperationRequest op;
    op.HeadsCount = heads;
    op.OperationDate = date;

    request->params = op.toJsonString();

    emit SendRequest(request);
}

void LivestockService::ProcessReply(NetworkResponse *reply)
{
    if(reply->service == "LivestockService")
    {
        if(reply->method == "GetState")
        {
            LivestockStateResponse resp;
            resp.fromJson(reply->result.toUtf8());
            emit ListockStateReceived(resp.State);
        }
    }
}
