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

    INetworkService *service = ApplicationWorker::Instance()->GetNetworkService("");
    if(service != nullptr)
    {
        m_prodService = dynamic_cast<ProductionService*>(service);
    }

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
    ui->lblClock->setText(clockStr);
}


void CMainWindow::on_pushButton_clicked()
{

}

