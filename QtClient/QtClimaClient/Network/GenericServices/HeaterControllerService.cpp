#include "HeaterControllerService.h"

#include <Network/GenericServices/Messages/DefaultRequest.h>

HeaterControllerService::HeaterControllerService(QObject *parent) : INetworkService(parent)
{

}

void HeaterControllerService::GetHeaterStates()
{
    DefaultRequest *request = new DefaultRequest();
    request->jsonrpc = "0.1a";
    request->service = "HeaterControllerService";
    request->method = "GetStateList";

    emit SendRequest(request);
}

void HeaterControllerService::UpdateHeaterState(HeaterState state)
{

}

void HeaterControllerService::ProcessReply(NetworkResponse *reply)
{
    if(reply->service == "HeaterControllerService")
    {

    }
}
