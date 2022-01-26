#include "LightOwerviewFrame.h"
#include "SelectTimerProfileFrame.h"
#include "ui_LightOwerviewFrame.h"

#include <Services/FrameManager.h>
#include <Network/INetworkService.h>
#include <ApplicationWorker.h>
#include <TimerPool.h>

LightOwerviewFrame::LightOwerviewFrame(QWidget *parent) :
    FrameBase(parent),
    ui(new Ui::LightOwerviewFrame)
{
    ui->setupUi(this);
    INetworkService *service = ApplicationWorker::Instance()->GetNetworkService("LightControllerService");
    if(service != nullptr)
    {
        m_lightController = dynamic_cast<LightControllerService*>(service);
    }

    ui->btnMode->setText("График");
    ui->btnOn->setEnabled(false);
    ui->btnOff->setEnabled(false);
}

LightOwerviewFrame::~LightOwerviewFrame()
{
    delete ui;
}

void LightOwerviewFrame::setLightProfile(const LightTimerProfile &profile)
{
    m_profile = profile;
    ui->timerPresenter->setTimerProfile(m_profile);
}


QString LightOwerviewFrame::getFrameName()
{
    return "LightOwerviewFrame";
}

void LightOwerviewFrame::on_btnReturn_clicked()
{
    FrameManager::instance()->PreviousFrame();
}

void LightOwerviewFrame::on_btnLightConfig_clicked()
{
    SelectTimerProfileFrame *frame = new SelectTimerProfileFrame();

    FrameManager::instance()->setCurrentFrame(frame);
}


void LightOwerviewFrame::on_btnMode_clicked()
{
    if(ui->btnMode->isChecked())
    {
        if(m_isAuto)
        {
            //QTimer *tmr = TimerPool::instance()->getUpdateTimer();
            //disconnect(tmr, &QTimer::timeout, this, &LightOwerviewFrame::onUpdate);
            qDebug()<<"Light begin to manual";
            m_lightController->ToManual();
        }
    }
    else
    {
        if(!m_isAuto)
        {
            //QTimer *tmr = TimerPool::instance()->getUpdateTimer();
            //disconnect(tmr, &QTimer::timeout, this, &LightOwerviewFrame::onUpdate);
            qDebug()<<"Light begin to auto";
            m_lightController->ToAuto();
        }
    }
}

void LightOwerviewFrame::CurrentProfileReceived(const LightTimerProfile &profile)
{
    ui->timerPresenter->setTimerProfile(profile);
    ui->lblProfileName->setText(profile.Name);

    m_lightController->GetLightStatus();
}

void LightOwerviewFrame::LightStatusReceived(const LightStatusResponse &status)
{
    if(status.IsAuto)
    {
        m_isAuto = true;
        ui->btnMode->setText("График");
        ui->btnMode->setChecked(false);

        ui->btnOn->setEnabled(false);
        ui->btnOff->setEnabled(false);
    }
    else
    {
        m_isAuto = false;
        ui->btnMode->setText("Ручное");
        ui->btnMode->setChecked(true);

        ui->btnOn->setEnabled(true);
        ui->btnOff->setEnabled(true);
    }

    if(status.IsOn)
    {
        ui->btnOn->setChecked(true);
        ui->btnOff->setChecked(false);
    }
    else
    {
        ui->btnOn->setChecked(false);
        ui->btnOff->setChecked(true);
    }
}

void LightOwerviewFrame::LightModeChanged(bool isAuto)
{
    if(isAuto)
    {
        qDebug()<<"Light mode to auto";
        ui->btnMode->setText("График");
        ui->btnOn->setEnabled(false);
        ui->btnOff->setEnabled(false);
    }
    else
    {
        qDebug()<<"Light mode to manual";
        ui->btnMode->setText("Ручное");
        ui->btnOn->setEnabled(true);
        ui->btnOff->setEnabled(true);
    }
    m_isAuto = isAuto;
}

void LightOwerviewFrame::onUpdate()
{
    m_lightController->GetLightStatus();
}



void LightOwerviewFrame::showEvent(QShowEvent *event)
{
    connect(m_lightController, &LightControllerService::ProfileReceived, this, &LightOwerviewFrame::CurrentProfileReceived);
    connect(m_lightController, &LightControllerService::LightStatusReceived, this, &LightOwerviewFrame::LightStatusReceived);
    connect(m_lightController, &LightControllerService::LightModeChanged, this, &LightOwerviewFrame::LightModeChanged);
    m_lightController->GetCurrentProfile();
}

void LightOwerviewFrame::closeEvent(QCloseEvent *event)
{
    disconnect(m_lightController, &LightControllerService::ProfileReceived, this, &LightOwerviewFrame::CurrentProfileReceived);
    disconnect(m_lightController, &LightControllerService::LightStatusReceived, this, &LightOwerviewFrame::LightStatusReceived);
    disconnect(m_lightController, &LightControllerService::LightModeChanged, this, &LightOwerviewFrame::LightModeChanged);
}

void LightOwerviewFrame::on_btnOn_clicked()
{
    if(!m_isAuto)
    {
        if(ui->btnOn->isChecked())
        {
            m_lightController->LightOn();
        }
    }
}


void LightOwerviewFrame::on_btnOff_clicked()
{
    if(!m_isAuto)
    {
        if(ui->btnOff->isChecked())
        {
            m_lightController->LightOff();
        }
    }
}

