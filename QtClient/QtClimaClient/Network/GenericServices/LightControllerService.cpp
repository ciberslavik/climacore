#include "LightControllerService.h"

LightControllerService::LightControllerService(QObject *parent) : INetworkService(parent)
{

}

QList<QString> LightControllerService::Methods()
{
    return QList<QString>();
}

void LightControllerService::ProcessReply(NetworkResponse *reply)
{

}
