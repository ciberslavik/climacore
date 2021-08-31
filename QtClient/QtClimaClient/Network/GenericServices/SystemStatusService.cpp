#include "SystemStatusService.h"

#include <Network/GenericServices/Messages/DefaultRequest.h>

SystemStatusService::SystemStatusService(QObject *parent) : INetworkService(parent)
{

}

void SystemStatusService::getClimatStatus()
{
    DefaultRequest *request = new DefaultRequest();
    request->service = "SystemStatusService";
    request->method = "GetClimatState";

    emit SendRequest(request);

    delete request;
}

void SystemStatusService::getTemperatureStatus()
{
    DefaultRequest *request = new DefaultRequest();
    request->service = "SystemStatusService";
    request->method = "GetTemperatureState";

    emit SendRequest(request);

    delete request;
}



QList<QString> SystemStatusService::Methods()
{
    return QList<QString>();
}

void SystemStatusService::ProcessReply(NetworkResponse *reply)
{
    if(reply->service=="SystemStatusService")
        if(reply->method=="GetClimatState")
        {
            ClimatStatusResponse *climatStatus = new ClimatStatusResponse();

            climatStatus->fromJson(reply->result.toUtf8());

            emit onClimatStatusRecv(climatStatus);
        }
        else if(reply->method=="GetTemperatureState")
        {
            TemperatureStateResponse *temperatureState = new TemperatureStateResponse();
            temperatureState->fromJson(reply->result.toUtf8());

            emit onTemperatureStatusRecv(temperatureState);
        }


}

