#include "MainWindow.h"

#include <ApplicationWorker.h>
#include <QApplication>
#include <QTimer>
#include <TimerPool.h>

#include <Network/ClientConnection.h>
#include <Network/Request.h>

#include <Frames/MainMenuFrame.h>
#include <Frames/SystemStateFrame.h>
#include <Network/GenericServices/DeviceProviderService.h>
#include <Network/GenericServices/GraphService.h>
#include <Network/GenericServices/HeaterControllerService.h>
#include <Network/GenericServices/LightControllerService.h>
#include <Network/GenericServices/LivestockService.h>
#include <Network/GenericServices/ProductionService.h>
#include <Network/GenericServices/SensorsService.h>
#include <Network/GenericServices/ServerInfoService.h>
#include <Network/GenericServices/VentilationService.h>
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
    //conn->ConnectToHost("192.168.0.10", 5911);

    if(!conn->isConnected())
    {
        qDebug()<< "Not connected";
        delete conn;
        delete worker;
        return 0;
    }
    worker->RegisterNetworkService(new ServerInfoService());
    worker->RegisterNetworkService(new SensorsService());
    worker->RegisterNetworkService(new SystemStatusService());
    worker->RegisterNetworkService(new GraphService());
    worker->RegisterNetworkService(new LightControllerService());
    worker->RegisterNetworkService(new VentilationService());
    worker->RegisterNetworkService(new DeviceProviderService());
    worker->RegisterNetworkService(new HeaterControllerService());
    worker->RegisterNetworkService(new ProductionService());
    worker->RegisterNetworkService(new LivestockService());

    CMainWindow w;
    w.resize(800, 480);
    w.show();
    TimerPool::instance()->getUpdateTimer()->setInterval(1000);
    TimerPool::instance()->getUpdateTimer()->start();
    //    QTimer timer(&a);
    //    QObject::connect(&timer, &QTimer::timeout, &w, &CMainWindow::updateData);
    //    timer.setInterval(1000);
    //    timer.start();

    FrameManager *frameManager = FrameManager::instance();
    frameManager->Initialize(&w, &a);

    //TestModeFrame *testFrame = new TestModeFrame();

    int result = a.exec();

    conn->Disconnect();
    return result;
}
