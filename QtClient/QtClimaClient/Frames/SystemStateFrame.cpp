#include "MainMenuFrame.h"
#include "SystemStateFrame.h"
#include "ui_SystemStateFrame.h"

#include <ApplicationWorker.h>
#include <TimerPool.h>

#include <Network/GenericServices/SensorsService.h>
#include <Network/GenericServices/ServerInfoService.h>

SystemStateFrame::SystemStateFrame(QWidget *parent) :
    FrameBase(parent),
    ui(new Ui::SystemStateFrame)
{
    ui->setupUi(this);
    setTitle("Обзор системы");


    INetworkService *service = ApplicationWorker::Instance()->GetNetworkService("SystemStatusService");
    if(service !=nullptr)
    {
        m_statusService = dynamic_cast<SystemStatusService*>(service);
    }


    m_updateTmer = TimerPool::instance()->getUpdateTimer();
}

SystemStateFrame::~SystemStateFrame()
{
    disconnect(m_updateTmer, &QTimer::timeout, this, &SystemStateFrame::onTimerElapsed);
    delete ui;
}

void SystemStateFrame::closeEvent(QCloseEvent *event)
{
    disconnect(m_updateTmer, &QTimer::timeout, this, &SystemStateFrame::onTimerElapsed);
    disconnect(m_statusService,&SystemStatusService::onClimatStatusRecv,this, &SystemStateFrame::onClimatStateUpdate);
    qDebug()<<"SystemStateFrame close event";
}

void SystemStateFrame::showEvent(QShowEvent *ev)
{
    connect(m_updateTmer, &QTimer::timeout, this, &SystemStateFrame::onTimerElapsed);
    connect(m_statusService,&SystemStatusService::onClimatStatusRecv,this, &SystemStateFrame::onClimatStateUpdate);
}

void SystemStateFrame::onClimatStateUpdate(ClimatStatusResponse *data)
{
    ui->lblFrontTemp->setText(QString::number(data->FrontTemperature));
    ui->lblRearTemp->setText(QString::number(data->RearTemperature));
    ui->lblOutdoorTemp->setText(QString::number(data->OutdoorTemperature));
    ui->lblHumidity->setText(QString::number(data->Humidity));
    ui->barPresure->setValue(data->Pressure);
    ui->barValves->setValue(data->ValvePosition);
    ui->barMines->setValue(data->MinePosition);
    ui->barAnalogFan->setValue(data->AnalogFanPower);
    ui->lblTempSetpoint->setText(QString::number(data->TempSetPoint));
}



void SystemStateFrame::onTimerElapsed()
{
    m_statusService->getClimatStatus();
}


void SystemStateFrame::on_btnMainMenu_clicked()
{
    MainMenuFrame *mainMenu = new MainMenuFrame();
    FrameManager::instance()->setCurrentFrame(mainMenu);

}

