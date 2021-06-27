#include "MainWindow.h"

#include <QApplication>

#include <Network/ClientConnection.h>

#include <Frames/Dialogs/AuthorizationDialog.h>
#include <Frames/MainMenuFrame.h>
#include <QMetaType>

int main(int argc, char *argv[])
{
    //qRegisterMetatype<MainMenuFrame*>("MainMenuFrame");

    QApplication a(argc, argv);
    ClientConnection *conn = new ClientConnection(&a);

    conn->ConnectToHost("127.0.0.1", 5911);

    NetworkRequest request;
    request.RequestType = QString("GetInfo").toUtf8();
    request.Data = QString("Hello Qt client").toUtf8();

    //conn->SendRequest(request);

    CMainWindow w;
    w.show();

    AuthorizationDialog *dlg = new AuthorizationDialog(conn, &w);

    if(dlg->exec()==QDialog::Accepted)
    {

    }
    int result = a.exec();

    conn->Disconnect();
    return result;
}
