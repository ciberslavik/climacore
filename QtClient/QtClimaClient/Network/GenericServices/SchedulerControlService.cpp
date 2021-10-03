#include "SchedulerControlService.h"

#include <Network/GenericServices/Messages/SchedulerInfoResponse.h>
#include <Network/GenericServices/Messages/SetProfileRequest.h>
#include <Network/GenericServices/Messages/VentilationParamsRequest.h>

SchedulerControlService::SchedulerControlService(QObject *parent) : INetworkService(parent)
{

}

void SchedulerControlService::SetTemperatureProfile(const QString &profileKey)
{
    NetworkRequest *request = new NetworkRequest();
    request->jsonrpc = "0.1a";
    request->service = "SchedulerControlService";
    request->method = "SetTemperatureProfile";
    SetProfileRequest rq;
    rq.ProfileKey = profileKey;

    request->params = rq.toJsonString();
    emit SendRequest(request);
}

void SchedulerControlService::SetVentilationProfile(const QString &profileKey)
{
    NetworkRequest *request = new NetworkRequest();
    request->jsonrpc = "0.1a";
    request->service = "SchedulerControlService";
    request->method = "SetVentilationProfile";
    SetProfileRequest rq;
    rq.ProfileKey = profileKey;

    request->params = rq.toJsonString();
    emit SendRequest(request);
}

void SchedulerControlService::SetValveProfile(const QString &profileKey)
{
    NetworkRequest *request = new NetworkRequest();
    request->jsonrpc = "0.1a";
    request->service = "SchedulerControlService";
    request->method = "SetValveProfile";
    SetProfileRequest rq;
    rq.ProfileKey = profileKey;

    request->params = rq.toJsonString();
    emit SendRequest(request);
}

void SchedulerControlService::SetMineProfile(const QString &profileKey)
{
    NetworkRequest *request = new NetworkRequest();
    request->jsonrpc = "0.1a";
    request->service = "SchedulerControlService";
    request->method = "SetMineProfile";
    SetProfileRequest rq;
    rq.ProfileKey = profileKey;

    request->params = rq.toJsonString();
    emit SendRequest(request);
}

void SchedulerControlService::GetProfilesInfo()
{
    NetworkRequest *request = new NetworkRequest();
    request->jsonrpc = "0.1a";
    request->service = "SchedulerControlService";
    request->method = "GetProfilesInfo";
    emit SendRequest(request);
}

void SchedulerControlService::GetProcessInfo()
{
    NetworkRequest *request = new NetworkRequest();
    request->jsonrpc = "0.1a";
    request->service = "SchedulerControlService";
    request->method = "GetProcessInfo";
    emit SendRequest(request);
}

void SchedulerControlService::GetVentilationParams()
{
    NetworkRequest *request = new NetworkRequest();
    request->jsonrpc = "0.1a";
    request->service = "SchedulerControlService";
    request->method = "GetVentilationParams";
    emit SendRequest(request);
}

void SchedulerControlService::UpdateVentilationParams(VentilationParams parameters)
{
    NetworkRequest *request = new NetworkRequest();
    request->jsonrpc = "0.1a";
    request->service = "SchedulerControlService";
    request->method = "UpdateVentParameters";

    VentilationParamsRequest pr;
    pr.Parameters = parameters;

    request->params = pr.toJsonString();
    emit SendRequest(request);
}

void SchedulerControlService::ProcessReply(NetworkResponse *reply)
{
    if(reply->service == "SchedulerControlService")
    {
        if(reply->method == "GetProfilesInfo")
        {
            SchedulerProfilesInfo info;
            info.fromJson(reply->result.toUtf8());
            emit SchedulerProfilesInfoReceived(info);
        }
        else if(reply->method == "GetProcessInfo")
        {
            SchedulerProcessInfo info;
            info.fromJson(reply->result.toUtf8());
            emit SchedulerProcessInfoReceived(info);
        }
        else if(reply->method == "SetTemperatureProfile" ||
                reply->method == "SetVentilationProfile" ||
                reply->method == "SetValveProfile" ||
                reply->method == "SetMineProfile")
        {
            emit SchedulerUpdated();
        }
        else if(reply->method == "GetVentilationParams")
        {
            VentilationParams params;
            params.fromJson(reply->result.toUtf8());
            emit VentilationParamsReceived(params);
        }
        else if(reply->method == "UpdateVentilationParams")
        {
            VentilationParams params;
            params.fromJson(reply->result.toUtf8());
            emit VentilationParamsUpdated(params);
        }
    }
}
