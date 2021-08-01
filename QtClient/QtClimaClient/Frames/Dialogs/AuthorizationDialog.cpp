#include "AuthorizationDialog.h"
#include "ui_AuthorizationDialog.h"

#include <Network/AuthRequest.h>
#include <Network/RequestUserList.h>

#include <QCryptographicHash>

AuthorizationDialog::AuthorizationDialog(ClientConnection *conn, QWidget *parent) :
    QDialog(parent),
    ui(new Ui::AuthorizationDialog)
{
    ui->setupUi(this);
    m_Connection = conn;

    RequestUserList requestUserList;// = new RequestUserList();
    requestUserList.RequestType = "AllUsers";

    //NetworkRequest *netRequest = new NetworkRequest();
    //netRequest->RequestType = "RequestUserList";

    //netRequest->Data = requestUserList.toJsonString().toUtf8();

    if(m_Connection->isConnected())
    {
        connect(m_Connection,&ClientConnection::ReplyReceived, this, &AuthorizationDialog::onReplyReceived);
        //m_Connection->SendRequest(netRequest);
        //delete netRequest;
    }
}

AuthorizationDialog::~AuthorizationDialog()
{
    delete ui;
}

void AuthorizationDialog::on_btnAccept_clicked()
{
    QString userName = ui->comboBox->currentText();
    NetworkRequest *request = CreateUserListRequest("SingleUser", userName);

    m_Connection->SendRequest(request);

}

void AuthorizationDialog::onReplyReceived(const NetworkReply &reply)
{

}

NetworkRequest *AuthorizationDialog::CreateUserListRequest(QString requestType, QString parameter)
{
    //NetworkRequest *request = new NetworkRequest();
    //request->RequestType = "RequestUserList";

    //RequestUserList requestUL;
    //requestUL.RequestType = requestType;
    //requestUL.Parameters = parameter;

    //request->Data = requestUL.toJsonString().toUtf8();

    return new NetworkRequest();
}



