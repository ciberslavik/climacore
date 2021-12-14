#include "ProductionService.h"

#include <Network/GenericServices/Messages/PreparingConfigRequest.h>
#include <Network/GenericServices/Messages/ProductionConfigRequest.h>
#include <Network/GenericServices/Messages/ProductionStateResponse.h>

ProductionService::ProductionService(QObject *parent) : INetworkService(parent)
{

}

void ProductionService::GetProductionState()
{
    NetworkRequest *request = new NetworkRequest();
    request->jsonrpc = "0.1a";
    request->service = "ProductionService";
    request->method = "GetProductionState";

    emit SendRequest(request);
}

void ProductionService::StartPreparing(float temperature, QDateTime startDate)
{
    NetworkRequest *request = new NetworkRequest();
    request->jsonrpc = "0.1a";
    request->service = "ProductionService";
    request->method = "StartPreparing";
    PreparingConfigRequest cfg;
    cfg.Config.TemperatureSetPoint = temperature;
    cfg.Config.StartDate = startDate;
    request->params = cfg.toJsonString();
    emit SendRequest(request);
}

void ProductionService::StartProduction(int placeHeads, QDateTime plandingDate, QDateTime startDate)
{
    NetworkRequest *request = new NetworkRequest();
    request->jsonrpc = "0.1a";
    request->service = "ProductionService";
    request->method = "StartProduction";

    ProductionConfigRequest cfg;
    cfg.Config.PlaceHeads = placeHeads;
    cfg.Config.PlandingDate = plandingDate;
    cfg.Config.StartDate = startDate;

    request->params = cfg.toJsonString();

    emit SendRequest(request);
}

void ProductionService::StopProduction()
{
    NetworkRequest *request = new NetworkRequest();
    request->jsonrpc = "0.1a";
    request->service = "ProductionService";
    request->method = "StopProduction";

    emit SendRequest(request);
}

QList<QString> ProductionService::Methods()
{
 return QList<QString>();
}

void ProductionService::ProcessReply(const NetworkResponse &reply)
{
    if(reply.service == "ProductionService")
    {
        ProductionStateResponse resp;
        resp.fromJson(reply.result.toUtf8());

        if(reply.method == "StartPreparing")
        {
            emit PreparingStarted(resp.State.State);
        }
        else if(reply.method == "StartProduction")
        {
            emit ProductionStarted(resp.State.State);
        }
        else if(reply.method == "StopProduction")
        {
            emit ProductionStopped(resp.State.State);
        }

        emit ProductionStateChanged(resp.State);
    }
}
