#pragma once

#include "NetworkReply.h"
#include "NetworkRequest.h"
#include <QApplication>
#include <QObject>
#include <QTcpSocket>
#include <QUuid>

class ClientConnection:public QObject
{
    Q_OBJECT
public:
    explicit ClientConnection(QObject *parent = nullptr);
    void ConnectToHost(const QString &host, const int &port, const int &waitTimeout = 500);
    void Disconnect();
    bool isConnected(){return m_socket->isOpen();}
    QUuid *getConnectionId(){return m_connectionId;}

signals:
    void ReplyReceived(NetworkReply *reply);
    void ConnectionEstabilished();
public slots:
    void SendRequest(NetworkRequest *request);
private slots:
    void onReadyRead();
    void socketConnected();
    //void onSoketError();
private:
    QTcpSocket *m_socket;
    QString readStrBuffer;
    QByteArray *readBuffer;
    bool m_connected;
    QUuid *m_connectionId;
};
