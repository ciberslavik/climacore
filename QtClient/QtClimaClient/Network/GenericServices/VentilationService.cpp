#include "VentilationService.h"

#include <Network/GenericServices/Messages/FanInfoRequest.h>
#include <Network/GenericServices/Messages/FanStateResponse.h>
#include <Network/GenericServices/Messages/ServoStateResponse.h>
#include <Network/GenericServices/Messages/UpdateFanStateRequest.h>
#include <Network/GenericServices/Messages/UpdateServoStateRequest.h>

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
    request->service = "VentilationControllerService";
    request->method = "CreateOrUpdateFan";

    FanInfoRequest fInfoRequest;
    fInfoRequest.FanKey = fanKey;

    request->params = fInfoRequest.toJsonString();

    emit SendRequest(request);
}

void VentilationService::GetValveState()
{
    NetworkRequest *request = new NetworkRequest();
    request->service = "VentilationControllerService";
    request->method = "GetValveState";

    emit SendRequest(request);
}

void VentilationService::UpdateValveState(bool isManual, float setPoint)
{
    NetworkRequest *request = new NetworkRequest();
    request->service = "VentilationControllerService";
    request->method = "UpdateValveState";
    UpdateServoStateRequest rq;
    rq.IsManual = isManual;
    rq.SetPoint = setPoint;
    request->params = rq.toJsonString();

    emit SendRequest(request);
}

void VentilationService::GetMineState()
{
    NetworkRequest *request = new NetworkRequest();
    request->service = "VentilationControllerService";
    request->method = "GetMineState";

    emit SendRequest(request);
}

void VentilationService::UpdateMineState(bool isManual, float setPoint)
{
    NetworkRequest *request = new NetworkRequest();
    request->service = "VentilationControllerService";
    request->method = "UpdateMineState";
    UpdateServoStateRequest rq;
    rq.IsManual = isManual;
    rq.SetPoint = setPoint;
    request->params = rq.toJsonString();

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
        else if(reply->method == "GetValveState")
        {
            ServoStateResponse rsp;
            rsp.fromJson(reply->result.toUtf8());

            emit ValveStateReceived(rsp.IsManual, rsp.CurrentPosition, rsp.SetPoint);
        }
        else if(reply->method == "GetMineState")
        {
            ServoStateResponse rsp;
            rsp.fromJson(reply->result.toUtf8());

            emit MineStateReceived(rsp.IsManual, rsp.CurrentPosition, rsp.SetPoint);
        }
        else if(reply->method == "UpdateMineState")
        {
            ServoStateResponse rsp;
            rsp.fromJson(reply->result.toUtf8());

            emit MineStateUpdated(rsp.IsManual, rsp.CurrentPosition, rsp.SetPoint);
        }
        else if(reply->method == "UpdateValveState")
        {
            ServoStateResponse rsp;
            rsp.fromJson(reply->result.toUtf8());

            emit ValveStateUpdated(rsp.IsManual, rsp.CurrentPosition, rsp.SetPoint);
        }
    }
}
