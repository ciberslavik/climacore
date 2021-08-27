#pragma once
#include <Network/NetworkRequest.h>
#include <QObject>
#include <Services/QSerializer.h>
#include <Models/Graphs/ProfileInfo.h>

class CreateGraphRequest:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE
public:
    CreateGraphRequest(){
        //jsonrpc = "0.1a";
        //service = "GraphProviderService";
        //method = "CreateTemperatureGraph";


    }
    QS_FIELD(int, GraphType)
    QS_OBJECT(ProfileInfo, Info)
};
