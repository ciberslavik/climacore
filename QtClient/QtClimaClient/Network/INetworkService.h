#pragma once

#include <QObject>

class INetworkService
{
public:

    virtual QString ServiceName() = 0;
    virtual QList<QString> Methods() = 0;
};

