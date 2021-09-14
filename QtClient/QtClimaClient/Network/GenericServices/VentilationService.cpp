#include "VentilationService.h"

#include <Network/GenericServices/Messages/FanInfoRequest.h>
#include <Network/GenericServices/Messages/FanStateResponse.h>
#include <Network/GenericServices/Messages/UpdateFanStateRequest.h>

VentilationService::VentilationService(QObject *parent) : INetworkService(parent)
{

}

void VentilationService::GetControllerState()
{

}

void VentilationService::GetFanStateList()
{
    NetworkRequest *request = new NetworkRequest();
    request->service = "VentilationControllerService";
    request->method = "GetFanStateList";

    emit SendRequest(request);
}

void VentilationService::GetFanInfoList()
{
    NetworkRequest *request = new NetworkRequest();
    request->service = "VentilationControllerService";
    request->method = "GetFanInfoList";

    emit SendRequest(request);
}

void VentilationService::UpdateFanState(const FanState &state)
{
    NetworkRequest *request = new NetworkRequest();
    request->service = "VentilationControllerService";
    request->method = "SetFanState";

    UpdateFanStateRequest fsRequ;
    fsRequ.State = state;

    request->params = fsRequ.toJsonString();

    emit SendRequest(request);
}

void VentilationService::CreateOrUpdateFan(const FanInfo &info)
{
    NetworkRequest *request = new NetworkRequest();
    request->service = "VentilationControllerService";
    request->method = "CreateOrUpdateFan";

    FanInfoRequest fInfoRequest;
    fInfoRequest.FanKey = info.Key;
    fInfoRequest.Info = info;

    request->params = fInfoRequest.toJsonString();

    emit SendRequest(request);
}


void VentilationService::RemoveFan(const QString &fanKey)
{
    NetworkRequest *request = new NetworkRequest();
    request->service = ServiceName();
    request->method = "CreateOrUpdateFan";

    FanInfoRequest fInfoRequest;
    fInfoRequest.FanKey = fanKey;

    request->params = fInfoRequest.toJsonString();

    emit SendRequest(request);
}

void VentilationService::ProcessReply(NetworkResponse *reply)
{
    if(reply->service == ServiceName())
    {
        if(reply->method == "GetFanStateList")
        {
            FanStateListResponse *response = new FanStateListResponse();
            response->fromJson(reply->result.toUtf8());

            emit FanStateListReceived(response->States);

        }
        else if(reply->method == "GetFanInfoList")
        {
            FanInfoListResponse *response = new FanInfoListResponse();
            response->fromJson(reply->result.toUtf8());

            emit FanInfoListReceived(response->Infos);

        }
        else if(reply->method == "CreateOrUpdateFan")
        {
            emit CreateOrUpdateComplete();
        }
        else if(reply->method == "SetFanState")
        {
            FanStateResponse rsp;
            rsp.fromJson(reply->result.toUtf8());
            emit FanStateUpdated(rsp.State);
        }
    }
}
