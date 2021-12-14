#include "LivestockService.h"

#include <Network/GenericServices/Messages/DefaultRequest.h>
#include <Network/GenericServices/Messages/LivestockOpListResponse.h>
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
    request->method = "GetLivestockState";

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

void LivestockService::GetOpList()
{
    NetworkRequest *request = new NetworkRequest();

    request->jsonrpc = "0.1a";
    request->service = "LivestockService";
    request->method = "GetOperations";
    request->params = DefaultRequest().toJsonString();

    emit SendRequest(request);
}

void LivestockService::ProcessReply(const NetworkResponse &reply)
{
    if(reply.service == "LivestockService")
    {
        LivestockStateResponse resp;
        resp.fromJson(reply.result.toUtf8());

        if(reply.method == "GetLivestockState")
        {
            emit LivestockStateReceived(resp.State);
        }
        else if(reply.method == "KillHeads")
        {
            emit KillComlete();
        }
        else if(reply.method == "RefractHeads")
        {
            emit RefractionComplete();
        }
        else if(reply.method == "DeathHeads")
        {
            emit DeathComplete();
        }
        else if(reply.method == "PlantHeads")
        {
            emit PlantedComplete();
        }
        else if(reply.method == "GetOperations")
        {
            LivestockOpListResponse resp;
            resp.fromJson(reply.result.toUtf8());
            emit OpListReceived(resp.Operations);
        }

        emit LivestockUpdated(resp.State);
    }
}
