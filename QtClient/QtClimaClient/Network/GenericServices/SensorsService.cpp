#include "SensorsService.h"

#include <Network/GenericServices/Messages/SensorsServiceRequest.h>

SensorsService::SensorsService(QObject *parent) : INetworkService(parent)
{

}

void SensorsService::GetSensors()
{
    SensorsServiceRequest *request = new SensorsServiceRequest();
    request->method = "ReadSensors";

    emit SendRequest(request);
}

QString SensorsService::ServiceName()
{
    return "SensorsService";
}

QList<QString> SensorsService::Methods()
{
    return QList<QString>();
}

void SensorsService::ProcessReply(NetworkReply *reply)
{
    qDebug()<< "SensorsService process reply:" << reply->result;
    SensorsServiceResponse *response = new SensorsServiceResponse();

    response->fromJson(reply->result.toUtf8());
    emit SensorsReceived(response);
}
