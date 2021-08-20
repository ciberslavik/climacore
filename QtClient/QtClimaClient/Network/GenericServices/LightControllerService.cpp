#include "LightControllerService.h"

LightControllerService::LightControllerService(QObject *parent) : INetworkService(parent)
{

}

void LightControllerService::GetPresetList()
{

}

QList<QString> LightControllerService::Methods()
{
    return QList<QString>();
}

void LightControllerService::ProcessReply(NetworkResponse *reply)
{

}
