#include "HeaterControllerService.h"

HeaterControllerService::HeaterControllerService(QObject *parent) : INetworkService(parent)
{

}

void HeaterControllerService::ProcessReply(NetworkResponse *reply)
{
    if(reply->service == "HeaterControllerService")
    {

    }
}
