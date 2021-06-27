#pragma once

#include "NetworkReply.h"
#include "NetworkRequest.h"

#include <QObject>
#include <QTcpSocket>

class ClientConnection:public QObject
{
    Q_OBJECT
public:
    explicit ClientConnection(QObject *parent = nullptr);
    void ConnectToHost(const QString &host, const int &port);
    void Disconnect();
    bool isConnected(){return m_socket->isOpen();}
signals:
    void ReplyReceived(const NetworkReply &reply);
public slots:
    void SendRequest(NetworkRequest *request);
private slots:
    void onReadyRead();

private:
    QTcpSocket *m_socket;
    QString readStrBuffer;
    QByteArray *readBuffer;
    bool m_connected;
};
