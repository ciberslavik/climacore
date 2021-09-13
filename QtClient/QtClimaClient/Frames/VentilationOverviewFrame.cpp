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
    qDebug() << "FAN State changed";
}

void VentilationOverviewFrame::onFanModeChanged(FanMode_t newMode)
{
    qDebug() << "FAN Mode changed";
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
        //widget->setMaximumSize(QSize(130,130));
        //widget->setMinimumSize(QSize(130,130));

        connect(widget, &FanWidget::FanModeChanged, this, &VentilationOverviewFrame::onFanModeChanged);
        connect(widget, &FanWidget::FanStateChanged, this, &VentilationOverviewFrame::onFanStateChanged);
        connect(widget, &FanWidget::EditBegin, this, &VentilationOverviewFrame::onBeginEditFan);
        connect(widget, &FanWidget::EditAccept, this, &VentilationOverviewFrame::onAcceptEditFan);
        connect(widget, &FanWidget::EditCancel, this, &VentilationOverviewFrame::onCancelEditFan);
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
        FanWidget *widget = m_fanWidgets.values().at(i);
        disconnect(widget, &FanWidget::FanModeChanged, this, &VentilationOverviewFrame::onFanModeChanged);
        disconnect(widget, &FanWidget::FanStateChanged, this, &VentilationOverviewFrame::onFanStateChanged);
        disconnect(widget, &FanWidget::EditBegin, this, &VentilationOverviewFrame::onBeginEditFan);
        disconnect(widget, &FanWidget::EditAccept, this, &VentilationOverviewFrame::onAcceptEditFan);
        disconnect(widget, &FanWidget::EditCancel, this, &VentilationOverviewFrame::onCancelEditFan);
        if(widget->isAnalog())
        {
            ui->layAnalogFans->removeWidget(widget);
        }
        else
        {
            ui->layDiscrFans->removeWidget(widget);
        }
        delete widget;
    }
    m_fanWidgets.clear();
}


void VentilationOverviewFrame::on_btnConfigure_clicked()
{
    VentilationConfigFrame *frame = new VentilationConfigFrame();

    frame->setService(m_ventService);

    FrameManager::instance()->setCurrentFrame(frame);
}

void VentilationOverviewFrame::onBeginEditFan()
{
    FanWidget *w = dynamic_cast<FanWidget*>(sender());
    qDebug() << "Begin edit fan:" << w->getStateObj()->Info.Key;
}

void VentilationOverviewFrame::onAcceptEditFan()
{
    FanWidget *w = dynamic_cast<FanWidget*>(sender());
    qDebug() << "Accept edit fan:" << w->getStateObj()->Info.Key;
}

void VentilationOverviewFrame::onCancelEditFan()
{
    FanWidget *w = dynamic_cast<FanWidget*>(sender());
    qDebug() << "Cancel edit fan:" << w->getStateObj()->Info.Key;
}

