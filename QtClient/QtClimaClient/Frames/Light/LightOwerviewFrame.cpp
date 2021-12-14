#include "LightOwerviewFrame.h"
#include "SelectTimerProfileFrame.h"
#include "ui_LightOwerviewFrame.h"

#include <Services/FrameManager.h>
#include <Network/INetworkService.h>
#include <ApplicationWorker.h>

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
    m_lightController->GetProfile("\"PROFILE:0\"");
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

