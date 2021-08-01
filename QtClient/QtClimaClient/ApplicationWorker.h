#pragma once

#include <QObject>

#include <Network/ClientConnection.h>

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
signals:

private slots:
    void onConnctionEstabilished();
    void onReplyReceived(const NetworkReply &reply);
private:
    ClientConnection *m_connection;
    int m_requestCounter;

    static ApplicationWorker *_instance;
};

