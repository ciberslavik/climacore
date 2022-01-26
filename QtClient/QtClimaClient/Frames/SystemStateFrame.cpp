#include "MainMenuFrame.h"
#include "SystemStateFrame.h"
#include "ui_SystemStateFrame.h"

#include <ApplicationWorker.h>
#include <TimerPool.h>

#include <Network/GenericServices/SensorsService.h>
#include <Network/GenericServices/ServerInfoService.h>
#include "GlobalContext.h"

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
    delete ui;
}

void SystemStateFrame::closeEvent(QCloseEvent *event)
{
    Q_UNUSED(event)

    disconnect(m_updateTmer, &QTimer::timeout, this, &SystemStateFrame::onTimerElapsed);
    disconnect(m_statusService,&SystemStatusService::onClimatStatusRecv,this, &SystemStateFrame::onClimatStateUpdate);
    qDebug()<<"SystemStateFrame close event";
}

void SystemStateFrame::showEvent(QShowEvent *ev)
{
    Q_UNUSED(ev)

    connect(m_updateTmer, &QTimer::timeout, this, &SystemStateFrame::onTimerElapsed);
    connect(m_statusService,&SystemStatusService::onClimatStatusRecv,this, &SystemStateFrame::onClimatStateUpdate);
}

void SystemStateFrame::onClimatStateUpdate(ClimatStatusResponse *data)
{
    ui->lblFrontTemp->setText(QString::number(data->FrontTemperature, 'f', 2) + " °C");
    ui->lblRearTemp->setText(QString::number(data->RearTemperature, 'f', 2) + " °C");
    ui->lblOutdoorTemp->setText(QString::number(data->OutdoorTemperature, 'f', 2) + " °C");
    ui->lblHumidity->setText(QString::number(data->Humidity, 'f', 1) + " %");
    ui->lblPressure->setText(QString::number(data->Pressure, 'f', 1) + " Па");
    //ui->barPresure->setValue(data->Pressure);
    ui->barValves->setValue(data->ValvePosition);
    ui->barMines->setValue(data->MinePosition);
    ui->barAnalogFan->setValue(data->AnalogFanPower);
    ui->lblAnalogPower->setText(QString::number(data->AnalogFanPower, 'f', 2) + " %");
    ui->lblAirPerHead->setText(QString::number(data->VentilationSetPoint, 'f', 3));
    ui->lblTempSetpoint->setText(QString::number(data->TempSetPoint, 'f', 2) + " °C");

    float realVent = data->VentilationSetPoint * GlobalContext::CurrentHeads;

    float percent = (realVent / data->TotalVentilation * 100);


    ui->lblVentPower->setText(QString::number(percent, 'f', 2) + " %");
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

