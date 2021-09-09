#include "MainWindow.h"
#include "ui_MainWindow.h"

#include <ApplicationWorker.h>
#include <QTime>
#include <TimerPool.h>

#include <Network/INetworkService.h>

#include <Frames/SystemStateFrame.h>

CMainWindow::CMainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
    , m_livestock(nullptr)
{
    ui->setupUi(this);

    INetworkService *service = ApplicationWorker::Instance()->GetNetworkService("ProductionService");
    if(service != nullptr)
    {
        m_prodService = dynamic_cast<ProductionService*>(service);
    }
    service = ApplicationWorker::Instance()->GetNetworkService("Livestock");
    if(service != nullptr)
    {
        m_livestock = dynamic_cast<LivestockService*>(service);
    }

    flag=true;
}

CMainWindow::~CMainWindow()
{
    disconnect(m_livestock, &LivestockService::ListockStateReceived, this, &CMainWindow::LivestockStateReceived);
    delete ui;
}

void CMainWindow::setFrameTitle(const QString &title)
{
    ui->lblTitle->setText(title);
}

QFrame *CMainWindow::getMainFrame()
{
    return ui->mainFrame;
}

void CMainWindow::updateData()
{
    QString clockStr = QTime::currentTime().toString("hh:mm");
    clockStr += " " + QDate::currentDate().toString("dd.MM.yyyy");
    ui->lblClock->setText(clockStr);
}


void CMainWindow::on_pushButton_clicked()
{

}

void CMainWindow::LivestockStateReceived(LivestockState state)
{

}

void CMainWindow::onProductionStateChanged(int newState)
{
    if(flag)
    {
        SystemStateFrame *stateFrame = new SystemStateFrame();

        FrameManager::instance()->setCurrentFrame(stateFrame);
        flag = false;
    }
    switch (newState) {
    case 0:
    {ui->lblStatus_2->setText("Останов");}
        break;
    case 1:
    {ui->lblStatus_2->setText("Подготовка");}
        break;
    case 2:
    {ui->lblStatus_2->setText("Выращивание");}
        break;
    };
}

void CMainWindow::updateUI()
{
    QTimer *tmr = TimerPool::instance()->getUpdateTimer();
    disconnect(tmr, &QTimer::timeout, this, &CMainWindow::updateUI);
    m_prodService->GetProductionState();
}

void CMainWindow::showEvent(QShowEvent *event)
{
    if(m_livestock !=nullptr)
        connect(m_livestock, &LivestockService::ListockStateReceived, this, &CMainWindow::LivestockStateReceived);
    connect(m_prodService, &ProductionService::ProductionStateChanged, this, &CMainWindow::onProductionStateChanged);

    QTimer *tmr = TimerPool::instance()->getUpdateTimer();
    connect(tmr, &QTimer::timeout, this, &CMainWindow::updateUI);

}

