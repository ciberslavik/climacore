#include "MainWindow.h"

#include <ApplicationWorker.h>
#include <QApplication>
#include <QTimer>

#include <Network/ClientConnection.h>
#include <Network/Request.h>

#include <Frames/MainMenuFrame.h>
#include <Frames/SystemStateFrame.h>
#include <Network/GenericServices/SensorsService.h>
#include <Network/GenericServices/ServerInfoService.h>
#include <QMetaType>
#include <Services/FrameManager.h>

int main(int argc, char *argv[])
{
    //qRegisterMetatype<MainMenuFrame*>("MainMenuFrame");

    QApplication a(argc, argv);
    ClientConnection *conn = new ClientConnection(&a);
    ApplicationWorker *worker = new ApplicationWorker(conn,&a);

    //conn->ConnectToHost("10.0.10.146", 5911);
    conn->ConnectToHost("127.0.0.1", 5911);
    //conn->ConnectToHost("192.168.0.11", 5911);

    if(!conn->isConnected())
        return 0;

    worker->RegisterNetworkService(new ServerInfoService());
    worker->RegisterNetworkService(new SensorsService());

    CMainWindow w;
    w.show();
    QTimer timer(&a);
    QObject::connect(&timer, &QTimer::timeout, &w, &CMainWindow::updateData);
    timer.setInterval(1000);
    timer.start();
    FrameManager *frameManager = new FrameManager(&w, &a);

    SystemState *state = new SystemState();
    state->setFrontTemperature(12.2);
    state->setFrontTemperature(12.4);
    state->setOutdoorTemperature(10.4);

    SystemStateFrame *stateFrame = new SystemStateFrame();

    FrameManager::instance()->setCurrentFrame(stateFrame);

    state->InvokeUpdate();
    // AuthorizationDialog *dlg = new AuthorizationDialog(conn, &w);

    //if(dlg->exec()==QDialog::Accepted)
    //{

    //}
    int result = a.exec();

    //conn->Disconnect();
    return result;
}
