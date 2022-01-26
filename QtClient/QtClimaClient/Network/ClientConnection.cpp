#include "ClientConnection.h"
#include "NetworkResponse.h"

#include <QMessageBox>
#include <QTime>
#include <QUuid>

#include <Services/FrameManager.h>

ClientConnection::ClientConnection(QObject *parent):QObject(parent)
{
    m_socket = new QTcpSocket(this);

    readBuffer = new QByteArray();
    connect(m_socket, &QIODevice::readyRead, this, &ClientConnection::onReadyRead);
    connect(m_socket, &QTcpSocket::connected, this, &ClientConnection::socketConnected);
    connect(m_socket, QOverload<QAbstractSocket::SocketError>::of(&QAbstractSocket::error), this, &ClientConnection::socketError);

}

void ClientConnection::ConnectToHost(const QString &host, const int &port, const int &waitTimeout)
{
    m_socket->abort();

    QTime myTimer;
    myTimer.start();
    m_socket->connectToHost(host, port);
    do
    {
        qApp->processEvents();
        if(m_socket->state() == QAbstractSocket::ConnectedState)
        {
            m_connected = true;
            request_counter = 0;
            m_socket->setReadBufferSize(40000);
            return;
        }
    }
    while(myTimer.elapsed() < waitTimeout);

    QMessageBox *mb = new QMessageBox();
    mb->setText("Network error: timeout connect to host:" + host + " on port:" + QString::number(port));
    mb->exec();
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
        request_counter++;

        request->id = request_counter;
        QString message = request->toJsonString();

        delete request;
        message = message + "<EOF>";
        //qDebug() << "Send Rquest:" << message;
        m_socket->write(message.toUtf8(),message.toUtf8().size());
        m_socket->flush();
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
                //qDebug()<< "Reply:" << data;
                QJsonDocument doc = QJsonDocument::fromJson(data.toUtf8());
                QJsonObject currentObject = doc.object();

                QJsonObject result = currentObject["result"].toObject();

                NetworkResponse reply;
                reply.fromJson(doc.object());
                reply.result = QJsonDocument(result).toJson(QJsonDocument::Indented);

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

void ClientConnection::socketError(QAbstractSocket::SocketError error)
{

    switch (error) {
    case QAbstractSocket::ConnectionRefusedError:
        break;
    case QAbstractSocket::RemoteHostClosedError:
        qDebug() << "";
        break;
    case QAbstractSocket::HostNotFoundError:
        qDebug() << "";
        break;
    case QAbstractSocket::SocketAccessError:
        qDebug() << "";
        break;
    case QAbstractSocket::SocketResourceError:
        qDebug() << "";
        break;
    case QAbstractSocket::SocketTimeoutError:
        qDebug() << "";
        break;
    case QAbstractSocket::DatagramTooLargeError:
        break;
    case QAbstractSocket::NetworkError:
        break;
    case QAbstractSocket::AddressInUseError:
        break;
    case QAbstractSocket::SocketAddressNotAvailableError:
        break;
    case QAbstractSocket::UnsupportedSocketOperationError:
        break;
    case QAbstractSocket::UnfinishedSocketOperationError:
        break;
    case QAbstractSocket::ProxyAuthenticationRequiredError:
        break;
    case QAbstractSocket::SslHandshakeFailedError:
        break;
    case QAbstractSocket::ProxyConnectionRefusedError:
        break;
    case QAbstractSocket::ProxyConnectionClosedError:
        break;
    case QAbstractSocket::ProxyConnectionTimeoutError:
        break;
    case QAbstractSocket::ProxyNotFoundError:
        break;
    case QAbstractSocket::ProxyProtocolError:
        break;
    case QAbstractSocket::OperationError:
        break;
    case QAbstractSocket::SslInternalError:
        break;
    case QAbstractSocket::SslInvalidUserDataError:
        break;
    case QAbstractSocket::TemporaryError:
        break;
    case QAbstractSocket::UnknownSocketError:
        break;

    }
    emit ConnectionError(m_socket->errorString());

}
