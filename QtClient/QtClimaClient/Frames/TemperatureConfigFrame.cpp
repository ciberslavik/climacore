#include "TemperatureConfigFrame.h"
#include "ui_TemperatureConfigFrame.h"

#include <Services/FrameManager.h>

TemperatureConfigFrame::TemperatureConfigFrame(QWidget *parent) :
    FrameBase(parent),
    ui(new Ui::TemperatureConfigFrame),
    m_ProfileSelector(nullptr)
{
    ui->setupUi(this);
}

TemperatureConfigFrame::~TemperatureConfigFrame()
{
    delete ui;
    if(m_ProfileSelector!=nullptr)
        delete m_ProfileSelector;
}

void TemperatureConfigFrame::on_btnReturn_clicked()
{
    FrameManager::instance()->PreviousFrame();
}

void TemperatureConfigFrame::onProfileSelected(ProfileInfo profileInfo)
{

}

QString TemperatureConfigFrame::getFrameName()
{
    return "TemperatureConfigFrame";
}


void TemperatureConfigFrame::on_btnSelectGraph_clicked()
{
    m_ProfileSelector = new SelectProfileFrame(ProfileType::Temperature);
    connect(
                m_ProfileSelector,
                &SelectProfileFrame::ProfileSelected,
                this,
                &TemperatureConfigFrame::onProfileSelected);

    FrameManager::instance()->setCurrentFrame(m_ProfileSelector);
}

