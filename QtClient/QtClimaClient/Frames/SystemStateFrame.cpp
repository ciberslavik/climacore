#include "SystemStateFrame.h"
#include "ui_SystemStateFrame.h"

#include <ApplicationWorker.h>

#include <Network/GenericServices/ServerInfoService.h>

SystemStateFrame::SystemStateFrame(SystemState *systemState, QWidget *parent) :
    FrameBase(parent),
    ui(new Ui::SystemStateFrame),
    m_SystemState(systemState)
{
    ui->setupUi(this);
    setTitle("Обзор системы");
    connect(m_SystemState, &SystemState::StateUpdate, this, &SystemStateFrame::onSystemStateUpdate);
}

SystemStateFrame::~SystemStateFrame()
{
    delete ui;
}

void SystemStateFrame::onSystemStateUpdate()
{
    ui->lblFrontTemp->setText(QString::number(m_SystemState->FrontTemperature()));
    ui->lblRearTemp->setText(QString::number(m_SystemState->RearTemperature()));
    ui->lblOutdoorTemp->setText(QString::number(m_SystemState->OutdoorTemperature()));
    ui->lblHumidity->setText(QString::number(m_SystemState->Humidity()));
}

void SystemStateFrame::on_pushButton_3_clicked()
{
    INetworkService *service = ApplicationWorker::Instance()->GetNetworkService("ServerInfoService");
    if(service !=nullptr)
    {
        ServerInfoService *serverInfoService = dynamic_cast<ServerInfoService*>(service);
        if(serverInfoService != nullptr)
        {
            serverInfoService->GetServerInfo();
        }
    }
}

