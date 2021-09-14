#include "SchedulerControlService.h"

#include <Network/GenericServices/Messages/SchedulerInfoResponse.h>
#include <Network/GenericServices/Messages/SetProfileRequest.h>

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

void SchedulerControlService::GetSchedulerInfo()
{
    NetworkRequest *request = new NetworkRequest();
    request->jsonrpc = "0.1a";
    request->service = "SchedulerControlService";
    request->method = "GetSchedulerInfo";
    emit SendRequest(request);
}

void SchedulerControlService::ProcessReply(NetworkResponse *reply)
{
    if(reply->service == "SchedulerControlService")
    {
        if(reply->method == "GetSchedulerInfo")
        {
            SchedulerInfoResponse info;
            info.fromJson(reply->result.toUtf8());
            emit SchedulerInfoReceived(info.Info);
        }
        else if(reply->method == "SetTemperatureProfile" ||
                reply->method == "SetVentilationProfile" ||
                reply->method == "SetValveProfile" ||
                reply->method == "SetMineProfile")
        {
            emit SchedulerUpdated();
        }
    }
}
