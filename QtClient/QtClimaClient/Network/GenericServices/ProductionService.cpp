#include "ProductionService.h"

#include <Network/GenericServices/Messages/PreparingConfigRequest.h>
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

void ProductionService::StartPreparing(float temperature)
{
    NetworkRequest *request = new NetworkRequest();
    request->jsonrpc = "0.1a";
    request->service = "ProductionService";
    request->method = "StartPreparing";
    PreparingConfigRequest cfg;
    cfg.Config.TemperatureSetPoint = temperature;

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

void ProductionService::ProcessReply(NetworkResponse *reply)
{
    if(reply->service == "ProductionService")
    {
        ProductionStateResponse resp;
        resp.fromJson(reply->result.toUtf8());

        if(reply->method == "StartPreparing")
        {
            emit PreparingStarted(resp.State);
        }
        if(reply->method == "StopProduction")
        {
            emit ProductionStopped(resp.State);
        }

        emit ProductionStateChanged(resp.State);
    }
}
