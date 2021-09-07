#include "VentilationConfigFrame.h"
#include "VentilationOverviewFrame.h"
#include "ui_VentilationOverviewFrame.h"
#include <Services/FrameManager.h>
#include <ApplicationWorker.h>

VentilationOverviewFrame::VentilationOverviewFrame(QWidget *parent) :
    FrameBase(parent),
    ui(new Ui::VentilationOverviewFrame)
{
    ui->setupUi(this);

    INetworkService *service = ApplicationWorker::Instance()->GetNetworkService("VentilationControllerService");
    if(service != nullptr)
    {
        m_ventService = dynamic_cast<VentilationService*>(service);
    }

    connect(m_ventService, &VentilationService::FanStateListReceived, this, &VentilationOverviewFrame::onFanStatesReceived);
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
    m_ventService->GetFanStateList();
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

void VentilationOverviewFrame::onFanStatesReceived(QList<FanState> states)
{
    if(m_fanStates.count()>0)
    {
        qDeleteAll(m_fanStates);
        m_fanStates.clear();
    }

    for(int i = 0; i < states.count(); i++)
    {
        FanState *state = new FanState(states.at(i));

        m_fanStates.insert(state->Info.Key, state);
    }

    createFanWidgets();
}

void VentilationOverviewFrame::onFanStateChanged(FanStateEnum_t newState)
{

}

void VentilationOverviewFrame::onFanModeChanged(FanMode_t newMode)
{

}

void VentilationOverviewFrame::createFanWidgets()
{
    if(m_fanWidgets.count()>0)
    {
        removeFanWidgets();
    }

    for(int i = 0; i < m_fanStates.count(); i++)
    {
        FanState *s = m_fanStates.values().at(i);

        FanWidget *widget = new FanWidget(s, this);
        connect(widget, &FanWidget::FanModeChanged, this, &VentilationOverviewFrame::onFanModeChanged);
        connect(widget, &FanWidget::FanStateChanged, this, &VentilationOverviewFrame::onFanStateChanged);
        if(s->Info.IsAnalog)
        {
            ui->layAnalogFans->addWidget(widget);
        }
        else
        {
            ui->layDiscrFans->addWidget(widget);
        }
        m_fanWidgets.insert(s->Info.Key, widget);
    }
}

void VentilationOverviewFrame::removeFanWidgets()
{
    for(int i = 0;i< m_fanWidgets.count(); i++)
    {
        FanWidget *w = m_fanWidgets.values().at(i);
        disconnect(w, &FanWidget::FanModeChanged, this, &VentilationOverviewFrame::onFanModeChanged);
        disconnect(w, &FanWidget::FanStateChanged, this, &VentilationOverviewFrame::onFanStateChanged);
        if(w->isAnalog())
        {
            ui->layAnalogFans->removeWidget(w);
        }
        else
        {
            ui->layDiscrFans->removeWidget(w);
        }
        delete w;
    }
    m_fanWidgets.clear();
}


void VentilationOverviewFrame::on_btnConfigure_clicked()
{
    VentilationConfigFrame *frame = new VentilationConfigFrame();

    frame->setService(m_ventService);

    FrameManager::instance()->setCurrentFrame(frame);
}

