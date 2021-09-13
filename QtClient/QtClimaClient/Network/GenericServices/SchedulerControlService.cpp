#include "SchedulerControlService.h"

#include <Network/GenericServices/Messages/SchedulerInfoResponse.h>

SchedulerControlService::SchedulerControlService(QObject *parent) : INetworkService(parent)
{

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
    }
}
