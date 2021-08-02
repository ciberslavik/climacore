#include "MainMenuFrame.h"
#include "SystemStateFrame.h"
#include "ui_SystemStateFrame.h"

#include <ApplicationWorker.h>

#include <Network/GenericServices/SensorsService.h>
#include <Network/GenericServices/ServerInfoService.h>

SystemStateFrame::SystemStateFrame(QWidget *parent) :
    FrameBase(parent),
    ui(new Ui::SystemStateFrame)
{
    ui->setupUi(this);
    setTitle("Обзор системы");


    INetworkService *service = ApplicationWorker::Instance()->GetNetworkService("SensorsService");
    if(service !=nullptr)
    {
        m_sensorsService = dynamic_cast<SensorsService*>(service);
    }
    connect(m_sensorsService,&SensorsService::SensorsReceived,this, &SystemStateFrame::onSystemStateUpdate);
    m_updateTmer = new QTimer();
    m_updateTmer->setInterval(1000);
    connect(m_updateTmer, &QTimer::timeout, this, &SystemStateFrame::onTimerElapsed);
    m_updateTmer->start();
}

SystemStateFrame::~SystemStateFrame()
{
    delete ui;
}

void SystemStateFrame::onSystemStateUpdate(SensorsServiceResponse *data)
{
    ui->lblFrontTemp->setText(data->FrontTemperature);
    ui->lblRearTemp->setText(data->RearTemperature);
    ui->lblOutdoorTemp->setText(data->OutdoorTemperature);
    ui->lblHumidity->setText(data->Humidity);

}

void SystemStateFrame::on_pushButton_3_clicked()
{
    INetworkService *service = ApplicationWorker::Instance()->GetNetworkService("SensorsService");
    if(service !=nullptr)
    {
        SensorsService *sensorsService = dynamic_cast<SensorsService*>(service);
        if(sensorsService != nullptr)
        {
            sensorsService->GetSensors();
        }
    }
}

void SystemStateFrame::onTimerElapsed()
{
    m_sensorsService->GetSensors();
}


void SystemStateFrame::on_pushButton_clicked()
{
    MainMenuFrame *mainMenu = new MainMenuFrame();
    FrameManager::instance()->setCurrentFrame(mainMenu);
}

