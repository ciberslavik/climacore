#include "SystemStatusService.h"

#include <Network/GenericServices/Messages/DefaultRequest.h>

SystemStatusService::SystemStatusService(QObject *parent) : INetworkService(parent)
{

}

void SystemStatusService::getClimatStatus()
{
    DefaultRequest *request = new DefaultRequest();
    request->service = "SystemStatusService";
    request->method = "GetClimateState";

    emit SendRequest(request);
}

void SystemStatusService::getTemperatureStatus()
{
    DefaultRequest *request = new DefaultRequest();
    request->service = "SystemStatusService";
    request->method = "GetTemperatureState";

    emit SendRequest(request);
}



QList<QString> SystemStatusService::Methods()
{
    return QList<QString>();
}

void SystemStatusService::ProcessReply(const NetworkResponse &reply)
{
    if(reply.service=="SystemStatusService") {
        if(reply.method=="GetClimateState")
        {
            ClimatStatusResponse *climatStatus = new ClimatStatusResponse();

            climatStatus->fromJson(reply.result.toUtf8());

            emit onClimatStatusRecv(climatStatus);
        }
        else if(reply.method=="GetTemperatureState")
        {
            TemperatureStateResponse *temperatureState = new TemperatureStateResponse();
            temperatureState->fromJson(reply.result.toUtf8());

            emit onTemperatureStatusRecv(temperatureState);
        }
    }


}

