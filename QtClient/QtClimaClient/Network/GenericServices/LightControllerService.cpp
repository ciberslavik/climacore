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
    request->method = "UpdateProfile";

    request->params = profile.toJsonString();

    emit SendRequest(request);
}

void LightControllerService::RemoveProfile(const QString &key)
{

}

void LightControllerService::GetLightStatus()
{
    NetworkRequest *request = new NetworkRequest();
    request->service = "LightControllerService";
    request->method = "GetLightStatus";

    emit SendRequest(request);
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

void LightControllerService::GetCurrentProfile()
{
    NetworkRequest *request = new NetworkRequest();
    request->service = "LightControllerService";
    request->method = "GetCurrentProfile";


    emit SendRequest(request);
}

void LightControllerService::SetCurrentProfile(const QString &profileKey)
{
    NetworkRequest *request = new NetworkRequest();
    request->service = "LightControllerService";
    request->method = "SetCurrentProfile";
    request->params = "\"" + profileKey.toUtf8() + "\"";

    emit SendRequest(request);
}

void LightControllerService::ToManual()
{
    NetworkRequest *request = new NetworkRequest();
    request->service = "LightControllerService";
    request->method = "ToManual";
    emit SendRequest(request);
}

void LightControllerService::ToAuto()
{
    NetworkRequest *request = new NetworkRequest();
    request->service = "LightControllerService";
    request->method = "ToAuto";
    emit SendRequest(request);
}

void LightControllerService::LightOn()
{
    NetworkRequest *request = new NetworkRequest();
    request->service = "LightControllerService";
    request->method = "LightOn";
    emit SendRequest(request);
}

void LightControllerService::LightOff()
{
    NetworkRequest *request = new NetworkRequest();
    request->service = "LightControllerService";
    request->method = "LightOff";
    emit SendRequest(request);
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
    else if((reply.method == "GetProfile")||(reply.method == "GetCurrentProfile"))
    {
        LightProfileResponse resp;
        resp.fromJson(reply.result.toUtf8());
        emit ProfileReceived(resp.Profile);
    }
    else if(reply.method == "GetLightStatus")
    {
        LightStatusResponse resp;
        resp.fromJson(reply.result.toUtf8());

        emit LightStatusReceived(resp);
    }
    else if(reply.method == "ToManual")
    {
        emit LightModeChanged(false);
    }
    else if(reply.method == "ToAuto")
    {
        emit LightModeChanged(true);
    }
}
