#include "ProductionService.h"

ProductionService::ProductionService(QObject *parent) : INetworkService(parent)
{

}

void ProductionService::GetProductionState()
{

}

QList<QString> ProductionService::Methods()
{
 return QList<QString>();
}

void ProductionService::ProcessReply(NetworkResponse *reply)
{
    if(reply->service == "ProductionService")
    {
        if(reply->method == "GetProductionState")
        {

        }
    }
}
