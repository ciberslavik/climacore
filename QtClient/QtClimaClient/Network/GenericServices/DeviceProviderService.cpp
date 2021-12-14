#include "DeviceProviderService.h"

#include <Network/GenericServices/Messages/RelayInfoListResponse.h>

DeviceProviderService::DeviceProviderService(QObject *parent) : INetworkService(parent)
{

}

void DeviceProviderService::GetRelayList()
{
    NetworkRequest *request = new NetworkRequest();
    request->service = ServiceName();
    request->method = "GetRelayList";

    emit SendRequest(request);
}

void DeviceProviderService::ProcessReply(const NetworkResponse &reply)
{
    if(reply.service == ServiceName())
    {
        if(reply.method == "GetRelayList")
        {
            RelayInfoListResponse response;
            response.fromJson(reply.result.toUtf8());
            emit RelayListReceived(response.Infos);
        }
    }
}
