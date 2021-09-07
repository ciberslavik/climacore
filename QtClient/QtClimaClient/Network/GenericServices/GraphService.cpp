#include "GraphService.h"



GraphService::GraphService(QObject *parent) : INetworkService(parent)
{

}

void GraphService::GetTempInfos()
{
    GraphInfosRequest *request = new GraphInfosRequest();
    request->method = "GetTemperatureProfileInfos";

    emit SendRequest(request);
}
void GraphService::GetVentInfos()
{
    GraphInfosRequest *request = new GraphInfosRequest();
    request->method = "GetVentilationProfileInfos";

    emit SendRequest(request);
}
void GraphService::GetValveInfos()
{
    GraphInfosRequest *request = new GraphInfosRequest();
    request->method = "GetValveProfileInfos";

    emit SendRequest(request);
}

void GraphService::GetTemperatureProfile(const QString &key)
{
    NetworkRequest *request = new NetworkRequest();
    request->jsonrpc = "0.1a";
    request->service = "GraphProviderService";
    request->method = "GetTemperatureProfile";

    GetProfileRequest profileRequest;
    profileRequest.ProfileType = (int)ProfileType::Temperature;;
    profileRequest.ProfileKey = key;

    request->params = profileRequest.toJsonString();

    emit SendRequest(request);
}
void GraphService::GetVentilationProfile(const QString &key)
{
    NetworkRequest *request = new NetworkRequest();
    request->jsonrpc = "0.1a";
    request->service = "GraphProviderService";
    request->method = "GetVentilationProfile";

    GetProfileRequest profileRequest;
    profileRequest.ProfileType = (int)ProfileType::Ventilation;;
    profileRequest.ProfileKey = key;

    request->params = profileRequest.toJsonString();

    emit SendRequest(request);
}
void GraphService::GetValveProfile(const QString &key)
{
    NetworkRequest *request = new NetworkRequest();
    request->jsonrpc = "0.1a";
    request->service = "GraphProviderService";
    request->method = "GetValveProfile";

    GetProfileRequest profileRequest;
    profileRequest.ProfileType = (int)ProfileType::ValveByVent;
    profileRequest.ProfileKey = key;

    request->params = profileRequest.toJsonString();

    emit SendRequest(request);
}

void GraphService::CreateTemperatureProfile(ProfileInfo info)
{
    NetworkRequest *request = new NetworkRequest();
    request->jsonrpc = "0.1a";
    request->service = "GraphProviderService";
    request->method = "CreateTemperatureProfile";

    CreateGraphRequest createRequest;


    createRequest.GraphType = (int)ProfileType::Temperature;
    createRequest.Info = info;

    request->params = createRequest.toJsonString();

    emit SendRequest(request);
}
void GraphService::CreateVentilationProfile(ProfileInfo info)
{
    NetworkRequest *request = new NetworkRequest();
    request->jsonrpc = "0.1a";
    request->service = "GraphProviderService";
    request->method = "CreateVentilationProfile";

    CreateGraphRequest createRequest;


    createRequest.GraphType = (int)ProfileType::Ventilation;
    createRequest.Info = info;

    request->params = createRequest.toJsonString();

    emit SendRequest(request);
}
void GraphService::CreateValveProfile(ProfileInfo info)
{
    NetworkRequest *request = new NetworkRequest();
    request->jsonrpc = "0.1a";
    request->service = "GraphProviderService";
    request->method = "CreateValveProfile";

    CreateGraphRequest createRequest;


    createRequest.GraphType = (int)ProfileType::ValveByVent;
    createRequest.Info = info;

    request->params = createRequest.toJsonString();

    emit SendRequest(request);
}

void GraphService::UpdateTemperatureProfile(ValueByDayProfile profile)
{
    NetworkRequest *request = new NetworkRequest();
    request->jsonrpc = "0.1a";
    request->service = "GraphProviderService";
    request->method = "UpdateTemperatureProfile";

    UpdateValueByDayProfileRequest profileRequest;
    profileRequest.Profile = profile;

    request->params = profileRequest.toJsonString();

    emit SendRequest(request);
}
void GraphService::UpdateVentilationProfile(MinMaxByDayProfile profile)
{
    NetworkRequest *request = new NetworkRequest();
    request->jsonrpc = "0.1a";
    request->service = "GraphProviderService";
    request->method = "UpdateVentilationProfile";

    UpdateMinMaxByDayProfileRequest profileRequest;
    profileRequest.Profile = profile;

    request->params = profileRequest.toJsonString();

    emit SendRequest(request);
}
void GraphService::UpdateValveProfile(ValueByValueProfile profile)
{
    NetworkRequest *request = new NetworkRequest();
    request->jsonrpc = "0.1a";
    request->service = "GraphProviderService";
    request->method = "UpdateVentilationProfile";

    UpdateValueByValueProfileRequest profileRequest;
    profileRequest.Profile = profile;

    request->params = profileRequest.toJsonString();

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
        if(reply->method == "GetTemperatureProfileInfos")
        {
            GraphInfosResponce infos;
            infos.fromJson(reply->result.toUtf8());
            emit TempInfosResponse(infos.Infos);
        }
        else if(reply->method == "GetVentilationProfileInfos")
        {
            GraphInfosResponce infos;
            infos.fromJson(reply->result.toUtf8());
            emit VentInfosResponse(infos.Infos);
        }
        else if(reply->method == "GetValveProfileInfos")
        {
            GraphInfosResponce infos;
            infos.fromJson(reply->result.toUtf8());
            emit ValveInfosResponse(infos.Infos);
        }
        else if(reply->method == "GetTemperatureProfile")
        {
            //qDebug() << "GetTemperatureProfile response:" << reply->result;
            ValueByDayProfile profile;
            profile.fromJson(reply->result.toUtf8());

            emit TempProfileResponse(profile);
        }

    }
}
