#include "VentilationService.h"

#include <Network/GenericServices/Messages/FanInfoRequest.h>

VentilationService::VentilationService(QObject *parent) : INetworkService(parent)
{

}

void VentilationService::GetFanStateList()
{
    NetworkRequest *request = new NetworkRequest();
    request->service = "VentilationService";
    request->method = "GetFanStates";

    emit SendRequest(request);
}

void VentilationService::GetFanInfoList()
{
    NetworkRequest *request = new NetworkRequest();
    request->service = "VentilationControllerService";
    request->method = "GetFanInfoList";

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
    request->service = "VentilationService";
    request->method = "CreateOrUpdateFan";

    FanInfoRequest fInfoRequest;
    fInfoRequest.FanKey = fanKey;

    request->params = fInfoRequest.toJsonString();

    emit SendRequest(request);
}

void VentilationService::ProcessReply(NetworkResponse *reply)
{
    if(reply->service == "VentilationService")
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
    }
}
