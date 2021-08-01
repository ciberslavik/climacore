#include "ClientConnection.h"
#include "NetworkReply.h"

#include <QUuid>

ClientConnection::ClientConnection(QObject *parent):QObject(parent)
{
    m_socket = new QTcpSocket(this);
    readBuffer = new QByteArray();
    connect(m_socket, &QIODevice::readyRead, this, &ClientConnection::onReadyRead);
    connect(m_socket, &QTcpSocket::connected, this, &ClientConnection::socketConnected);
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
        qDebug() << "Send Rquest:" << message;
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

            if(data.contains("GetSessionID:"))
            {
                int idIndex = data.indexOf(':');
                QString sessionId = data.right(data.length() - (idIndex + 1));
                m_connectionId = new QUuid(sessionId);
                qDebug()<<"Session id:" << sessionId;
                emit ConnectionEstabilished();
            }
            else
            {
                qDebug()<< "Reply:" << data;
                QJsonDocument doc = QJsonDocument::fromJson(data.toLocal8Bit());
                QJsonObject currentObject = doc.object();

                QJsonObject result = currentObject["result"].toObject();

                NetworkReply *reply = new NetworkReply();
                reply->fromJson(doc.object());
                reply->result = QJsonDocument(result).toJson(QJsonDocument::Indented);
                if(reply->result != "")
                    emit ReplyReceived(reply);
            }
            //qDebug()<< "Read data:" << data;
            //QUuid *id = new QUuid(data);
            //qDebug() << "Session id:"<< id->toString();
            readStrBuffer = "";
        }

    }
}

void ClientConnection::socketConnected()
{
    QString sessionIdRequest = "GetSessionID<EOF>";
    m_socket->write(sessionIdRequest.toUtf8(),sessionIdRequest.toUtf8().length());
    m_socket->flush();
}
