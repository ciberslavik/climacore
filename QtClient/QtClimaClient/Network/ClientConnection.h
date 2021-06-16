#pragma once

#include "NetworkRequest.h"

#include <QObject>
#include <QTcpSocket>

class ClientConnection:public QObject
{
    Q_OBJECT
public:
    explicit ClientConnection(QObject *parent = nullptr);
    void ConnectToHost(const QString &host, const int &port);
public slots:
    void SendRequest(const NetworkRequest &request);
private slots:
    void onReadyRead();

private:
    QTcpSocket *m_socket;
    QString readStrBuffer;
    QByteArray *readBuffer;
};
