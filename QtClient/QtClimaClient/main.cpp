#include "MainWindow.h"

#include <ApplicationWorker.h>
#include <QApplication>
#include <QTimer>
#include <TimerPool.h>

#include <Network/ClientConnection.h>
#include <Network/Request.h>

#include <Frames/MainMenuFrame.h>
#include <Frames/SystemStateFrame.h>
#include <Network/GenericServices/GraphService.h>
#include <Network/GenericServices/LightControllerService.h>
#include <Network/GenericServices/SensorsService.h>
#include <Network/GenericServices/ServerInfoService.h>
#include <QMetaType>
#include <Services/FrameManager.h>
#include <Frames/TestModeFrame.h>
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
    worker->RegisterNetworkService(new SystemStatusService());
    worker->RegisterNetworkService(new GraphService());
    worker->RegisterNetworkService(new LightControllerService());

    CMainWindow w;
    w.resize(800, 480);
    w.show();
//    QTimer timer(&a);
//    QObject::connect(&timer, &QTimer::timeout, &w, &CMainWindow::updateData);
//    timer.setInterval(1000);
//    timer.start();

    FrameManager *frameManager = FrameManager::instance();
    frameManager->Initialize(&w, &a);

    TestModeFrame *testFrame = new TestModeFrame();
    SystemStateFrame *stateFrame = new SystemStateFrame();

    FrameManager::instance()->setCurrentFrame(testFrame);


    int result = a.exec();

    conn->Disconnect();
    return result;
}
