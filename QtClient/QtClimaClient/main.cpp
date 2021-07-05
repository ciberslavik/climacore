#include "MainWindow.h"

#include <QApplication>

#include <Network/ClientConnection.h>
#include <Network/Request.h>

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
    request.jsonrpc = "2.0";
    request.method = "rpc.version";

    conn->SendRequest(&request);


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
