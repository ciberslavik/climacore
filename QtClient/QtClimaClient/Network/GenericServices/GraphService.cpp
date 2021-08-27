#include "GraphService.h"

#include <Network/GenericServices/Messages/GraphInfosRequest.h>
#include <Network/GenericServices/Messages/CreateGraphRequest.h>
#include <Network/GenericServices/Messages/GetProfileRequest.h>
#include <Frames/Graphs/GraphType.h>

GraphService::GraphService(QObject *parent) : INetworkService(parent)
{

}

void GraphService::GetTempInfos()
{
    GraphInfosRequest *request = new GraphInfosRequest();
    request->method = "GetTemperatureProfileInfos";

    emit SendRequest(request);
}

void GraphService::CreateTemperatureProfile(ProfileInfo *info)
{
    NetworkRequest *request = new NetworkRequest();
    CreateGraphRequest *createRequest = new CreateGraphRequest();


    createRequest->GraphType = (int)GraphType::Temperature;
    createRequest->Info = *info;
    request->jsonrpc = "0.1a";
    request->service = "GraphProviderService";
    request->method = "CreateTemperatureProfile";
    request->params = createRequest->toJsonString();

    emit SendRequest(request);
}

void GraphService::GetTemperatureProfile(const QString &key)
{
    NetworkRequest *request = new NetworkRequest();
    request->jsonrpc = "0.1a";
    request->service = "GraphProviderService";
    request->method = "GetTemperatureProfile";

    GetProfileRequest *profileRequest = new GetProfileRequest();
    profileRequest->ProfileType = 0;
    profileRequest->ProfileKey = key;

    request->params = profileRequest->toJsonString();
}

void GraphService::UpdateTemperatureProfile(ValueByDayProfile profile)
{

}

void GraphService::GetVentInfos()
{
    GraphInfosRequest *request = new GraphInfosRequest();
    request->method = "GetVentilationProfileInfos";

    emit SendRequest(request);
}

QString GraphService::ServiceName()
{
    return "GraphProviderService";
}

QList<QString> GraphService::Methods()
{
    return QList<QString>();
}

void GraphService::ProcessReply(NetworkResponse *reply)
{
    if(reply->service=="GraphProviderService")
    {
        if(reply->method == "GetTemperatureProfilesInfos")
        {
            qDebug() << "GetTemperatureProfilesInfos response:" << reply->result;

            GraphInfosResponce *infos = new GraphInfosResponce();
            infos->fromJson(reply->result.toUtf8());

            emit TempInfosResponse(&infos->Infos);

        }
        else if(reply->method == "GetTemperatureProfile")
        {
            qDebug() << "GetTemperatureProfile response:" << reply->result;
            GetProfileResponse *profile = new GetProfileResponse();
            profile->fromJson(reply->result.toUtf8());

            emit TempProfileResponse(&profile->Profile);
        }
        else if(reply->method == "GetVentilationProfileInfos")
        {

        }
    }
}
