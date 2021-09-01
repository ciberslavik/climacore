#include "ProductionService.h"

ProductionService::ProductionService(QObject *parent) : INetworkService(parent)
{

}

QList<QString> ProductionService::Methods()
{
 return QList<QString>();
}

void ProductionService::ProcessReply(NetworkResponse *reply)
{

}
