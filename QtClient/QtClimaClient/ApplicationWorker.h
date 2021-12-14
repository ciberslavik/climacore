#pragma once

#include <QMap>
#include <QObject>

#include <Network/ClientConnection.h>
#include <Network/INetworkService.h>

class ApplicationWorker : public QObject
{
    Q_OBJECT
public:
    explicit ApplicationWorker(ClientConnection *connection, QObject *parent = nullptr);

    void StartWorker();

    static ApplicationWorker* Instance()
    {
        return _instance;
    }

    void RegisterNetworkService(INetworkService *service);
    INetworkService *GetNetworkService(const QString &serviceName);
signals:

private slots:
    void onConnctionEstabilished();
    void onReplyReceived(const NetworkResponse &reply);
    void onSendRequest(NetworkRequest *request);
    void onConnectionError(const QString &message);
private:
    ClientConnection *m_connection;
    int m_requestCounter;

    static ApplicationWorker *_instance;
    QMap<QString, INetworkService*> *m_services;
};

