#include "LightControllerService.h"

#include <Network/GenericServices/Messages/CreateLightProfileRequest.h>
#include <Network/GenericServices/Messages/KeyRequest.h>
#include <Network/GenericServices/Messages/LightProfileListResponse.h>
#include <Network/GenericServices/Messages/LightProfileResponse.h>

LightControllerService::LightControllerService(QObject *parent) : INetworkService(parent)
{

}

void LightControllerService::CreateTimerProfile(const QString &profileName, const QString &description)
{
    NetworkRequest *request = new NetworkRequest();
    request->service = "LightControllerService";
    request->method = "CreateProfile";

    CreateLightProfileRequest createRequest;
    createRequest.ProfileName = profileName;
    createRequest.Description = description;
    request->params = createRequest.toJsonString();

    emit SendRequest(request);
}

void LightControllerService::UpdateProfile(const LightTimerProfile &profile)
{
    NetworkRequest *request = new NetworkRequest();
    request->service = "LightControllerService";
    request->method = "CreateProfile";

    request->params = profile.toJsonString();

    emit SendRequest(request);
}

void LightControllerService::RemoveProfile(const QString &key)
{

}

void LightControllerService::GetProfileInfoList()
{
    NetworkRequest *request = new NetworkRequest();
    request->service = "LightControllerService";
    request->method = "GetProfileInfoList";

    emit SendRequest(request);
}

void LightControllerService::GetProfile(const QString &key)
{
    NetworkRequest *request = new NetworkRequest();
    request->service = "LightControllerService";
    request->method = "GetProfile";

    request->params = "\"" + key.toUtf8() + "\"";

    emit SendRequest(request);
}

void LightControllerService::GetCurrentProfileInfo()
{

}

QList<QString> LightControllerService::Methods()
{
    return QList<QString>();
}

void LightControllerService::ProcessReply(const NetworkResponse &reply)
{
    if(reply.method == "CreateProfile")
    {
        CreateLightProfileResponse resp;
        resp.fromJson(reply.result.toUtf8());
        emit ProfileCreated(resp.Profile);
    }
    else if(reply.method == "GetProfileInfoList")
    {
        LightProfileListResponse resp;
        resp.fromJson(reply.result.toUtf8());

        emit PresetListReceived(resp.Profiles);
    }
    else if(reply.method == "GetProfile")
    {
        LightProfileResponse resp;
        resp.fromJson(reply.result.toUtf8());
        qDebug() << reply.result.toUtf8();
        emit ProfileReceived(resp.Profile);
    }
}
