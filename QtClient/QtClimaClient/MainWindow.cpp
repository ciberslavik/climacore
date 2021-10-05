#include "GlobalContext.h"
#include "MainWindow.h"
#include "ui_MainWindow.h"

#include <ApplicationWorker.h>
#include <QTime>
#include <TimerPool.h>

#include <Network/INetworkService.h>

#include <Frames/SystemStateFrame.h>
#include <Frames/TestModeFrame.h>

CMainWindow::CMainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);

    INetworkService *service = ApplicationWorker::Instance()->GetNetworkService("ProductionService");
    if(service != nullptr)
    {
        m_prodService = dynamic_cast<ProductionService*>(service);
    }

    connect(m_prodService, &ProductionService::ProductionStateChanged, this, &CMainWindow::onProductionStateChanged);

    flag = true;
}

CMainWindow::~CMainWindow()
{
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
}


void CMainWindow::on_pushButton_clicked()
{

}


void CMainWindow::onProductionStateChanged(ProductionState newState)
{
    if(flag)
    {
        SystemStateFrame *stateFrame = new SystemStateFrame();
        //TestModeFrame *testFrame = new TestModeFrame();
        FrameManager::instance()->setCurrentFrame(stateFrame);
        flag = false;
    }
    switch (newState.State) {
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
    ui->lblHeads->setText(QString::number(newState.CurrentHeads));
    ui->lblDay->setText(QString::number(newState.CurrentDay));
    GlobalContext::CurrentDay = newState.CurrentDay;
    GlobalContext::CurrentHeads = newState.CurrentHeads;
}

void CMainWindow::updateUI()
{
    QTimer *tmr = TimerPool::instance()->getUpdateTimer();
    disconnect(tmr, &QTimer::timeout, this, &CMainWindow::updateUI);
    m_prodService->GetProductionState();
}

void CMainWindow::showEvent(QShowEvent *event)
{
    Q_UNUSED(event)

    QTimer *tmr = TimerPool::instance()->getUpdateTimer();
    connect(tmr, &QTimer::timeout, this, &CMainWindow::updateUI);

}

