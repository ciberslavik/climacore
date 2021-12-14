#include "HeaterControllerService.h"

#include <Network/GenericServices/Messages/DefaultRequest.h>
#include <Network/GenericServices/Messages/HeaterParamsListResponse.h>
#include <Network/GenericServices/Messages/HeaterParamsRequest.h>
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

void HeaterControllerService::GetHeaterParams()
{
    DefaultRequest *request = new DefaultRequest();
    request->jsonrpc = "0.1a";
    request->service = "HeaterControllerService";
    request->method = "GetHeaterParamsList";

    emit SendRequest(request);
}

void HeaterControllerService::UpdateHeaterParams(QList<HeaterParams> heaterParams)
{
    NetworkRequest *request = new NetworkRequest();
    request->jsonrpc = "0.1a";
    request->service = "HeaterControllerService";
    request->method = "UpdateHeaterParamsList";

    HeaterParamsRequest par;
    par.ParamsList = heaterParams;

    request->params = par.toJsonString();

    emit SendRequest(request);
}

void HeaterControllerService::UpdateHeaterState(const QString &key, HeaterState state)
{
    NetworkRequest *request = new NetworkRequest();
    request->jsonrpc = "0.1a";
    request->service = "HeaterControllerService";
    request->method = "UpdateHeaterState";
    HeaterStateRequest st;

    st.Key = key;
    st.State = state;
    request->params = st.toJsonString();

    emit SendRequest(request);
}

void HeaterControllerService::ProcessReply(const NetworkResponse &reply)
{
    if(reply.service == "HeaterControllerService")
    {
        if(reply.method == "GetStateList")
        {
            HeaterStateListResponse resp;
            resp.fromJson(reply.result.toUtf8());

            emit HeaterStateListReceived(resp.SetPoint, resp.Front, resp.Rear, resp.States);
        }
        else if(reply.method == "SetHeaterState")
        {
            HeaterStateResponse resp;
            resp.fromJson(reply.result.toUtf8());
            emit HeaterStateUpdated(resp.State);
        }
        else if(reply.method == "GetHeaterParamsList")
        {
            HeaterParamsListResponse resp;
            resp.fromJson(reply.result.toUtf8());
            emit HeaterParamsListReceived(resp.ParamsList);
        }
        else if(reply.method == "UpdateHeaterParamsList")
        {
            HeaterParamsListResponse resp;
            resp.fromJson(reply.result.toUtf8());
            emit HeaterParamsUpdated(resp.ParamsList);
        }
    }
}
