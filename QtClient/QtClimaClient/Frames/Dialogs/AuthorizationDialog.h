#pragma once

#include <QDialog>
#include <QCryptographicHash>

#include <Network/ClientConnection.h>
#include <Network/ReplyUserList.h>

namespace Ui {
class AuthorizationDialog;
}

class AuthorizationDialog : public QDialog
{
    Q_OBJECT

public:
    explicit AuthorizationDialog(ClientConnection *conn, QWidget *parent = nullptr);
    ~AuthorizationDialog();

private slots:
    void on_btnAccept_clicked();
    void onReplyReceived(const NetworkReply &reply);
private:
    Ui::AuthorizationDialog *ui;

    ClientConnection *m_Connection;

    NetworkRequest *CreateUserListRequest(QString requestType, QString parameter = "");
};

