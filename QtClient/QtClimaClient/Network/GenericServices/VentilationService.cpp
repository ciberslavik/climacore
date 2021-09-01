#include "VentilationService.h"

VentilationService::VentilationService(QObject *parent) : INetworkService(parent)
{

}

void VentilationService::GetFanStates()
{
    NetworkRequest *request = new NetworkRequest();
    request->service = "VentilationService";
    request->method = "GetFanStates";

    emit SendRequest(request);

    delete request;
}

void VentilationService::ProcessReply(NetworkResponse *reply)
{
    if(reply->service == "VentilationService")
    {
        if(reply->method == "GetFanStates")
        {
            VentilationStateResponse *response = new VentilationStateResponse();
            response->fromJson(reply->result.toUtf8());

            emit FanStatesReceived(response);

            delete response;
        }
    }
}
