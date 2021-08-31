#include "VentilationOverviewFrame.h"
#include "ui_VentilationOverviewFrame.h"
#include <Services/FrameManager.h>

VentilationOverviewFrame::VentilationOverviewFrame(QWidget *parent) :
    FrameBase(parent),
    ui(new Ui::VentilationOverviewFrame)
{
    ui->setupUi(this);
}

VentilationOverviewFrame::~VentilationOverviewFrame()
{
    delete ui;
}


void VentilationOverviewFrame::closeEvent(QCloseEvent *event)
{
}

void VentilationOverviewFrame::showEvent(QShowEvent *event)
{
}


void VentilationOverviewFrame::on_pushButton_clicked()
{
    FrameManager::instance()->PreviousFrame();
}


void VentilationOverviewFrame::on_btnSelectProfile_clicked()
{
    m_ProfileSelector = new SelectProfileFrame(ProfileType::Ventilation);
    connect(m_ProfileSelector, &SelectProfileFrame::ProfileSelected, this, &VentilationOverviewFrame::onProfileSelectorComplete);
    FrameManager::instance()->setCurrentFrame(m_ProfileSelector);
}

void VentilationOverviewFrame::onProfileSelectorComplete(ProfileInfo profileInfo)
{
   // disconnect(m_ProfileSelector, &SelectProfileFrame::ProfileSelected, this, &VentilationOverviewFrame::onProfileSelectorComplete);
    ui->lblProfileName->setText(profileInfo.Name);
}

