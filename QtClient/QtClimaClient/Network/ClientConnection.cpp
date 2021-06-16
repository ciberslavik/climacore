#include "ClientConnection.h"
#include "NetworkReply.h"

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

void ClientConnection::SendRequest(const NetworkRequest &request)
{
    if(m_socket->isOpen())
    {
        QJsonDocument doc;
        doc.setObject(request.toJson());

        QString message = doc.toJson();
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
            NetworkReply reply;
            reply.fromJson(doc.object());

            qDebug()<< "Read data:" << reply.Data;

            readStrBuffer = "";
        }

    }
}
