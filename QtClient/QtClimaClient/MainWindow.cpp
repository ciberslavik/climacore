#include "MainWindow.h"
#include "ui_MainWindow.h"

#include <ApplicationWorker.h>
#include <QTime>

#include <Network/INetworkService.h>

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
    service = ApplicationWorker::Instance()->GetNetworkService("Livestock");
    if(service != nullptr)
    {
        m_livestock = dynamic_cast<LivestockService*>(service);
    }

    connect(m_livestock, &LivestockService::ListockStateReceived, this, &CMainWindow::LivestockStateReceived);
}

CMainWindow::~CMainWindow()
{
    connect(m_livestock, &LivestockService::ListockStateReceived, this, &CMainWindow::LivestockStateReceived);
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

