#include "MainWindow.h"
#include "SignalHandler.h"

#include <ApplicationWorker.h>
#include <QApplication>
#include <QFile>
#include <QIODevice>
#include <QTimer>
#include <TimerPool.h>
#include <signal.h>
#include <Network/ClientConnection.h>
#include <Network/Request.h>

#include <Frames/MainMenuFrame.h>
#include <Frames/SystemStateFrame.h>
#include <Network/GenericServices/DeviceProviderService.h>
#include <Network/GenericServices/GraphService.h>
#include <Network/GenericServices/HeaterControllerService.h>
#include <Network/GenericServices/IONetworkService.h>
#include <Network/GenericServices/LightControllerService.h>
#include <Network/GenericServices/LivestockService.h>
#include <Network/GenericServices/ProductionService.h>
#include <Network/GenericServices/SchedulerControlService.h>
#include <Network/GenericServices/SensorsService.h>
#include <Network/GenericServices/ServerInfoService.h>
#include <Network/GenericServices/VentilationService.h>
#include <QMetaType>
#include <Services/FrameManager.h>
#include <Frames/TestModeFrame.h>
#include <QSqlDatabase>
#include <QSqlError>
#include <QDir>
void dbTest()
{
    QSqlDatabase db = QSqlDatabase::addDatabase("QMYSQL", "ClimaDB");
    db.setHostName("localhost");
    db.setPort(3306);

    if(!db.open())
    {
        qDebug() << "Database connection error :" << db.lastError().text();
    }
}

bool CheckController()
{
    QString currDirectory = QApplication::applicationDirPath();
    QString scryptPath = QDir::cleanPath(currDirectory + QDir::separator() + "QtClimaClient.sh");
    return QFile::exists(scryptPath);
}
int main(int argc, char *argv[])
{
    //qRegisterMetatype<MainMenuFrame*>("MainMenuFrame");
    SignalHandler::init();

    QApplication a(argc, argv);
    ClientConnection *conn = new ClientConnection(&a);

    ApplicationWorker *worker = new ApplicationWorker(conn,&a);

    //if(CheckController())
    {
        conn->ConnectToHost("11.0.10.37", 5911);
    }
    //else
    {
     // conn->ConnectToHost("10.0.10.146", 5911);
    }
    //dbTest();
    //
    //conn->ConnectToHost("11.0.10.10", 5911);

    //conn->ConnectToHost("192.168.0.10", 5911);

    if(!conn->isConnected())
    {
        qDebug() << "Not connected";
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
    worker->RegisterNetworkService(new SchedulerControlService());
    worker->RegisterNetworkService(new IONetworkService());


    QFile green_led("/sys/class/leds/agava:programm:green/brightness");
    if(green_led.open(QFile::OpenModeFlag::WriteOnly))
    {
        green_led.write("0", 1);
        green_led.close();
    }
    CMainWindow w;
    w.resize(800, 480);
    w.show();
    TimerPool::instance()->getUpdateTimer()->setInterval(1000);
    TimerPool::instance()->getUpdateTimer()->start();

    FrameManager *frameManager = FrameManager::instance();
    frameManager->Initialize(&w, &a);

    // TestModeFrame *testFrame = new TestModeFrame();
    // testFrame->show();

    int result = a.exec();

    conn->Disconnect();
    return result;
}

void ProcessSocketError(const QString &message)
{

}
