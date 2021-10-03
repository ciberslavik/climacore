#include "ConfigProfilesFrame.h"
#include "ui_ConfigProfilesFrame.h"
#include "ApplicationWorker.h"
#include "Services/FrameManager.h"

ConfigProfilesFrame::ConfigProfilesFrame(QWidget *parent) :
    FrameBase(parent),
    ui(new Ui::ConfigProfilesFrame),
    m_selector(nullptr)
{
    ui->setupUi(this);
    INetworkService *service = ApplicationWorker::Instance()->GetNetworkService("SchedulerControlService");
    if(service != nullptr)
    {
        m_schedService = dynamic_cast<SchedulerControlService*>(service);
        connect(m_schedService, &SchedulerControlService::SchedulerProfilesInfoReceived, this, &ConfigProfilesFrame::onSchedulerInfoReceived);
    }

    m_schedService->GetProfilesInfo();
}

ConfigProfilesFrame::~ConfigProfilesFrame()
{
    delete ui;
}

void ConfigProfilesFrame::on_btnReturn_clicked()
{
    FrameManager::instance()->PreviousFrame();
}

void ConfigProfilesFrame::onSchedulerInfoReceived(SchedulerProfilesInfo info)
{
    ui->lblTemperatureName->setText(info.TemperatureProfileName);
    ui->lblVentilationName->setText(info.VentilationProfileName);
    ui->lblValveName->setText(info.ValveProfileName);
    ui->lblMineName->setText(info.MineProfileName);
}


void ConfigProfilesFrame::on_btnSelectTempGraph_clicked()
{

    m_currentProfile = 0;

    m_selector = new SelectProfileFrame(ProfileType::Temperature);
    connect(m_selector, &SelectProfileFrame::ProfileSelected, this, &ConfigProfilesFrame::onProfileSelected);

    FrameManager::instance()->setCurrentFrame(m_selector);
}


void ConfigProfilesFrame::on_btnSelectVentGraph_clicked()
{
    m_currentProfile = 1;

    m_selector = new SelectProfileFrame(ProfileType::Ventilation);
    connect(m_selector, &SelectProfileFrame::ProfileSelected, this, &ConfigProfilesFrame::onProfileSelected);

    FrameManager::instance()->setCurrentFrame(m_selector);
}


void ConfigProfilesFrame::on_btnSelectValveGraph_clicked()
{

    m_currentProfile = 2;

    m_selector = new SelectProfileFrame(ProfileType::ValveByVent);
    connect(m_selector, &SelectProfileFrame::ProfileSelected, this, &ConfigProfilesFrame::onProfileSelected);

    FrameManager::instance()->setCurrentFrame(m_selector);
}


void ConfigProfilesFrame::on_btnSelectMineGraph_clicked()
{

    m_currentProfile = 3;

    m_selector = new SelectProfileFrame(ProfileType::ValveByVent);
    connect(m_selector, &SelectProfileFrame::ProfileSelected, this, &ConfigProfilesFrame::onProfileSelected);

    FrameManager::instance()->setCurrentFrame(m_selector);
}

void ConfigProfilesFrame::onProfileSelected(ProfileInfo info)
{
    switch (m_currentProfile) {
    case 0:
        m_schedService->SetTemperatureProfile(info.Key);
        ui->lblTemperatureName->setText(info.Name);
        break;
    case 1:
        m_schedService->SetVentilationProfile(info.Key);
        ui->lblVentilationName->setText(info.Name);
        break;
    case 2:
        m_schedService->SetValveProfile(info.Key);
        ui->lblValveName->setText(info.Name);
        break;
    case 3:
        m_schedService->SetMineProfile(info.Key);
        ui->lblMineName->setText(info.Name);
        break;
    default:
        break;
    }

    FrameManager::instance()->PreviousFrame();
}

