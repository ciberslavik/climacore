#pragma once
#include <Network/NetworkRequest.h>
#include <QObject>
#include <Services/QSerializer.h>
#include <Models/Graphs/ProfileInfo.h>

class CreateGraphRequest: public NetworkRequest
{
    Q_GADGET
    QS_SERIALIZABLE
public:
    CreateGraphRequest(){}
    QS_FIELD(int, GraphType)
    QS_OBJECT(ProfileInfo, Info)
};
