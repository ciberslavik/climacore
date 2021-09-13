#include "HeaterControllerService.h"

#include <Network/GenericServices/Messages/DefaultRequest.h>
#include <Network/GenericServices/Messages/HeaterStateListResponse.h>
#include <Network/GenericServices/Messages/HeaterStateRequest.h>
#include <Network/GenericServices/Messages/HeaterStateResponse.h>

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
    NetworkRequest *request = new NetworkRequest();
    request->jsonrpc = "0.1a";
    request->service = "HeaterControllerService";
    request->method = "SetHeaterState";
    HeaterStateRequest st;

    st.Key = state.Info.Key;
    st.State = state;
    request->params = st.toJsonString();

    emit SendRequest(request);
}

void HeaterControllerService::ProcessReply(NetworkResponse *reply)
{
    if(reply->service == "HeaterControllerService")
    {
        if(reply->method == "GetStateList")
        {
            HeaterStateListResponse resp;
            resp.fromJson(reply->result.toUtf8());

            emit HeaterStateListReceived(resp.States);
        }
        else if(reply->method == "SetHeaterState")
        {
            HeaterStateResponse resp;
            resp.fromJson(reply->result.toUtf8());
            emit HeaterStateUpdated(resp.State);
        }
    }
}
