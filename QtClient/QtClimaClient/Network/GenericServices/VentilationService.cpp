#include "VentilationService.h"

#include <Network/GenericServices/Messages/FanInfoRequest.h>
#include <Network/GenericServices/Messages/FanStateResponse.h>
#include <Network/GenericServices/Messages/ServoStateResponse.h>
#include <Network/GenericServices/Messages/FanStateRequest.h>
#include <Network/GenericServices/Messages/UpdateServoStateRequest.h>
#include <Network/GenericServices/Messages/FanRemoveRequest.h>
#include <Network/GenericServices/Messages/FanModeRequest.h>
#include <Network/GenericServices/Messages/VentilationStatusResponse.h>
#include <Network/GenericServices/Messages/FanKeyRequest.h>


VentilationService::VentilationService(QObject *parent) : INetworkService(parent)
{

}

void VentilationService::GetControllerState()
{

}



void VentilationService::GetFanInfoList()
{
    NetworkRequest *request = new NetworkRequest();
    request->service = "VentilationControllerService";
    request->method = "GetFanInfoList";

    emit SendRequest(request);
}

void VentilationService::GetFanInfo(const QString &fanKey)
{
    NetworkRequest *request = new NetworkRequest();
    request->service = "VentilationControllerService";
    request->method = "GetFanInfo";
    FanKeyRequest r;
    r.FanKey = fanKey;
    request->params = r.toJsonString();

    emit SendRequest(request);
}

void VentilationService::SetFanState(const QString &key, const FanStateEnum &state)
{
    NetworkRequest *request = new NetworkRequest();
    request->service = "VentilationControllerService";
    request->method = "SetFanState";

    FanStateRequest fsRequ;
    fsRequ.Key = key;
    fsRequ.State = (int)state;

    request->params = fsRequ.toJsonString();

    emit SendRequest(request);
}

void VentilationService::SetFanMode(const QString &key, const FanModeEnum &state)
{
    NetworkRequest *request = new NetworkRequest();
    request->service = "VentilationControllerService";
    request->method = "SetFanMode";

    FanModeRequest fsRequ;
    fsRequ.Key = key;
    fsRequ.Mode = (int)state;

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
    request->method = "RemoveFan";

    FanRemoveRequest fInfoRequest;
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

void VentilationService::GetVentilationStatus()
{
    NetworkRequest *request = new NetworkRequest();
    request->service = "VentilationControllerService";
    request->method = "GetVentilationStatus";

    emit SendRequest(request);
}

void VentilationService::ProcessReply(NetworkResponse *reply)
{
    if(reply->service == ServiceName())
    {
        if(reply->method == "GetFanInfoList")
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
            //emit FanStateUpdated(rsp.State);
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
        else if(reply->method == "GetVentilationStatus")
        {
            VentilationStatusResponse rsp;
            rsp.fromJson(reply->result.toUtf8());

            emit VentilationStatusReceived(rsp.ValveCurrentPos,
                                           rsp.ValveSetPoint,
                                           rsp.MineCurrentPos,
                                           rsp.MineSetPoint,
                                           rsp.VentSetPoint);
        }
        else if(reply->method == "GetFanInfo")
        {
            FanInfo info;
            info.fromJson(reply->result.toUtf8());
            emit FanInfoReceived(info);
        }
    }
}
