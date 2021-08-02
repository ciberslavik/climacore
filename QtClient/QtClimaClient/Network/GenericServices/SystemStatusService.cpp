#include "SystemStatusService.h"

SystemStatusService::SystemStatusService(QObject *parent) : INetworkService(parent)
{

}



QList<QString> SystemStatusService::Methods()
{
    return QList<QString>();
}

void SystemStatusService::ProcessReply(NetworkReply *reply)
{

}

