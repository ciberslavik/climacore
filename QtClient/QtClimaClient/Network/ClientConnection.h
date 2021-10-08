#pragma once

#include "NetworkResponse.h"
#include "NetworkRequest.h"
#include <QAbstractSocket>
#include <QApplication>
#include <QObject>
#include <QTcpSocket>
#include <QUuid>

class ClientConnection:public QObject
{
    Q_OBJECT
public:
    explicit ClientConnection(QObject *parent = nullptr);
    void ConnectToHost(const QString &host, const int &port, const int &waitTimeout = 5000);
    void Disconnect();
    bool isConnected(){return m_socket->isOpen();}
    QUuid *getConnectionId(){return m_connectionId;}

signals:
    void ReplyReceived(NetworkResponse *reply);
    void ConnectionEstabilished();
    void ConnectionError(const QString &message);
public slots:
    void SendRequest(NetworkRequest *request);
private slots:
    void onReadyRead();
    void socketConnected();
    void socketError(QAbstractSocket::SocketError error);
    //void onSoketError();
private:
    QTcpSocket *m_socket;
    QString readStrBuffer;
    QByteArray *readBuffer;
    bool m_connected;
    QUuid *m_connectionId;
    int request_counter;
};
