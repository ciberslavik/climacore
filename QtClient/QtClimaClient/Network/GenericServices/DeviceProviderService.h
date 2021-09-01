#pragma once

#include <Network/INetworkService.h>
#include <QObject>

class DeviceProviderService : public INetworkService
{
    Q_OBJECT
public:
    explicit DeviceProviderService(QObject *parent = nullptr);
};

