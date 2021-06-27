#include "ClientConnection.h"
#include "NetworkReply.h"

#include <QUuid>

ClientConnection::ClientConnection(QObject *parent):QObject(parent)
{
    m_socket = new QTcpSocket(this);
    readBuffer = new QByteArray();
    connect(m_socket, &QIODevice::readyRead, this, &ClientConnection::onReadyRead);
}

void ClientConnection::ConnectToHost(const QString &host, const int &port)
{
    m_socket->abort();
    m_socket->connectToHost(host, port);

}

void ClientConnection::Disconnect()
{
    if(m_socket->isOpen())
    {
        m_socket->disconnectFromHost();
    }
}

void ClientConnection::SendRequest(NetworkRequest *request)
{
    if(m_socket->isOpen())
    {
        QString message = request->toJsonString();
        message = message + "<EOF>";

        m_socket->write(message.toUtf8(),message.toUtf8().size());
    }
}

void ClientConnection::onReadyRead()
{
    QTcpSocket *socket = static_cast<QTcpSocket*>(sender());
    while(socket->bytesAvailable() > 0)
    {
        readStrBuffer.append(QString::fromUtf8(socket->readAll()));
        if(readStrBuffer.indexOf("<EOF>")>-1)
        {
            QString data = readStrBuffer.left(readStrBuffer.indexOf("<EOF>"));
            QJsonDocument doc = QJsonDocument::fromJson(data.toLocal8Bit());
            NetworkReply *reply = new NetworkReply();
            reply->fromJson(doc.object());

            if(reply->RequestType != "")
                emit ReplyReceived(*reply);


            qDebug()<< "Read data:" << data;
            QUuid *id = new QUuid(data);
            qDebug() << "Session id:"<< id->toString();
            readStrBuffer = "";
        }

    }
}
