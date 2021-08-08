#pragma once


#include <QObject>

#include <Network/NetworkReply.h>

class GraphInfosResponce : public NetworkReply
{
    Q_GADGET
    QS_SERIALIZABLE
public:
    GraphInfosResponce();
};

