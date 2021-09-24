#include "VentControllerConfigFrame.h"
#include "VentilationConfigFrame.h"
#include "VentilationOverviewFrame.h"
#include "ui_VentilationOverviewFrame.h"
#include <Services/FrameManager.h>
#include <ApplicationWorker.h>
#include <TimerPool.h>
#include <Frames/Dialogs/ConfigValveDialog.h>

VentilationOverviewFrame::VentilationOverviewFrame(QWidget *parent) :
    FrameBase(parent),
    ui(new Ui::VentilationOverviewFrame),
    m_updateCounter(0)
{
    ui->setupUi(this);

    INetworkService *service = ApplicationWorker::Instance()->GetNetworkService("VentilationControllerService");
    if(service != nullptr)
    {
        m_ventService = dynamic_cast<VentilationService*>(service);
    }


    connect(m_ventService, &VentilationService::FanStateListReceived, this, &VentilationOverviewFrame::onFanStatesReceived);
    connect(m_ventService, &VentilationService::FanStateUpdated, this, &VentilationOverviewFrame::onFanStateUpdated);
    connect(m_ventService, &VentilationService::MineStateReceived, this, &VentilationOverviewFrame::onMineStateReceived);
    connect(m_ventService, &VentilationService::ValveStateReceived, this, &VentilationOverviewFrame::onValveStateReceived);

    m_updateTimer = TimerPool::instance()->getUpdateTimer();
    connect(m_updateTimer, &QTimer::timeout, this, &VentilationOverviewFrame::onUpdateTimer);
}

VentilationOverviewFrame::~VentilationOverviewFrame()
{
    disconnect(m_ventService, &VentilationService::FanStateListReceived, this, &VentilationOverviewFrame::onFanStatesReceived);
    disconnect(m_ventService, &VentilationService::FanStateUpdated, this, &VentilationOverviewFrame::onFanStateUpdated);
    disconnect(m_ventService, &VentilationService::MineStateReceived, this, &VentilationOverviewFrame::onMineStateReceived);
    disconnect(m_ventService, &VentilationService::ValveStateReceived, this, &VentilationOverviewFrame::onValveStateReceived);
    delete ui;
}


void VentilationOverviewFrame::closeEvent(QCloseEvent *event)
{
}

void VentilationOverviewFrame::showEvent(QShowEvent *event)
{
    //m_ventService->GetFanStateList();
}


void VentilationOverviewFrame::on_pushButton_clicked()
{
    FrameManager::instance()->PreviousFrame();
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

    if(m_fanWidgets.count()==m_fanStates.count())
        updateFanWidgets();
    else
        createFanWidgets();
}

void VentilationOverviewFrame::onFanStateUpdated(FanState state)
{
    QString key = state.Info.Key;
    m_fanStates[key]->State = state.State;
    m_fanStates[key]->Info.IsManual = state.Info.IsManual;
    bool man = state.Info.IsManual;

    if(man)
        m_fanWidgets[key]->setFanMode(FanMode::Manual);
    else
    {
        if(m_fanStates[key]->Info.Hermetise)
            m_fanWidgets[key]->setFanMode(FanMode::Disabled);
        else
            m_fanWidgets[key]->setFanMode(FanMode::Auto);
    }
    m_fanWidgets[key]->setFanState((FanStateEnum)state.State);
}

void VentilationOverviewFrame::onEditFanStateChanged(const QString &fanKey, FanStateEnum_t newState)
{
    qDebug() << "FAN State changed" << fanKey << " mode:" << (int)newState;
    FanWidget *w = dynamic_cast<FanWidget*>(sender());
    QString key = w->FanKey();

    if(m_fanStates[key]->Info.IsManual)
    {
        m_fanStates[key]->State = (int)newState;

        m_ventService->UpdateFanState(*m_fanStates[key]);
    }
}

void VentilationOverviewFrame::onEditFanModeChanged(const QString &fanKey, FanMode_t newMode)
{
    qDebug() <<"Edit mode changed:" << fanKey << " mode:" << (int)newMode;
    m_fanStates[fanKey]->Info.IsManual = newMode == FanMode::Manual ? true : false;
    m_ventService->UpdateFanState(*m_fanStates[fanKey]);
}

void VentilationOverviewFrame::onValveStateReceived(bool isManual, float currPos, float setPoint)
{
    Q_UNUSED(isManual);
    Q_UNUSED(setPoint);
    ui->pbValves->setValue(currPos);
    ui->lblValveSetPoint->setText(QString::number(setPoint,'f',1));

}

void VentilationOverviewFrame::onMineStateReceived(bool isManual, float currPos, float setPoint)
{
    Q_UNUSED(isManual);
    Q_UNUSED(setPoint);
    ui->pbMines->setValue(currPos);
    ui->lblMintSetPoint->setText(QString::number(setPoint,'f',1));
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

        FanWidget *widget = new FanWidget(s->Info.Key,s->Info.IsAnalog, this);
        if(s->Info.IsManual)
            widget->setFanMode(FanMode::Manual);
        else
        {
            if(s->Info.Hermetise)
                widget->setFanMode(FanMode::Disabled);
            else
                widget->setFanMode(FanMode::Auto);
        }
        widget->setFanState((FanStateEnum)s->State);

        //widget->setMaximumSize(QSize(130,130));
        //widget->setMinimumSize(QSize(130,130));

        connect(widget, &FanWidget::FanModeChanged, this, &VentilationOverviewFrame::onEditFanModeChanged);
        connect(widget, &FanWidget::FanStateChanged, this, &VentilationOverviewFrame::onEditFanStateChanged);
        connect(widget, &FanWidget::EditBegin, this, &VentilationOverviewFrame::onBeginEditFan);
        connect(widget, &FanWidget::EditAccept, this, &VentilationOverviewFrame::onAcceptEditFan);
        connect(widget, &FanWidget::EditCancel, this, &VentilationOverviewFrame::onCancelEditFan);
        if(s->Info.IsAnalog)
        {
            widget->setAnalogValue(s->AnalogPower);
            ui->layAnalogFans->addWidget(widget);
            widget->setMaximumSize(QSize(110,1100));
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
        disconnect(widget, &FanWidget::FanModeChanged, this, &VentilationOverviewFrame::onEditFanModeChanged);
        disconnect(widget, &FanWidget::FanStateChanged, this, &VentilationOverviewFrame::onEditFanStateChanged);
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

void VentilationOverviewFrame::updateFanWidgets()
{
    for(int i = 0; i < m_fanStates.count(); i++)
    {
        FanState *s = m_fanStates.values().at(i);
        FanWidget *w = m_fanWidgets[s->Info.Key];
        if(s->Info.IsManual)
            w->setFanMode(FanMode::Manual);
        else
        {
            if(s->Info.Hermetise)
                w->setFanMode(FanMode::Disabled);
            else
                w->setFanMode(FanMode::Auto);
        }
        w->setFanState((FanStateEnum)s->State);
        if(s->Info.IsAnalog)
        {
            w->setAnalogValue(s->AnalogPower);
        }
    }
}


void VentilationOverviewFrame::on_btnConfigure_clicked()
{
    VentilationConfigFrame *frame = new VentilationConfigFrame();

    frame->setService(m_ventService);

    FrameManager::instance()->setCurrentFrame(frame);
}

void VentilationOverviewFrame::onBeginEditFan(const QString &fanKey)
{
    FanWidget *w = dynamic_cast<FanWidget*>(sender());

    m_editedOld = FanState(*m_fanStates[fanKey]);
    m_updateTimer->stop();
    qDebug() << "Begin edit fan:" << m_editedOld.Info.Key;

}

void VentilationOverviewFrame::onAcceptEditFan(const QString &fanKey)
{
    FanWidget *w = dynamic_cast<FanWidget*>(sender());
    m_updateTimer->start();
    qDebug() << "Accept edit fan:" << fanKey;
}

void VentilationOverviewFrame::onCancelEditFan(const QString &fanKey)
{
    FanWidget *w = dynamic_cast<FanWidget*>(sender());

    m_ventService->UpdateFanState(m_editedOld);
    m_updateTimer->start();
    qDebug() << "Cancel edit fan:" << fanKey;
}


void VentilationOverviewFrame::on_btnConfigMine_clicked()
{
    ConfigValveDialog dlg(ConfigValveDialog::Mine, FrameManager::instance()->MainWindow());

    if(dlg.exec() == QDialog::Accepted)
    {

    }
}


void VentilationOverviewFrame::on_btnConfigValve_clicked()
{
    ConfigValveDialog dlg(ConfigValveDialog::Valve, FrameManager::instance()->MainWindow());

    if(dlg.exec() == QDialog::Accepted)
    {

    }
}


void VentilationOverviewFrame::on_btnControllerConfig_clicked()
{
    VentControllerConfigFrame *configFrame = new VentControllerConfigFrame();

    FrameManager::instance()->setCurrentFrame(configFrame);
}

void VentilationOverviewFrame::onUpdateTimer()
{
    switch (m_updateCounter) {
    case 0:
        m_ventService->GetMineState();
        break;
    case 1:
        m_ventService->GetValveState();
        break;
    case 2:
        m_ventService->GetFanStateList();
        break;
    default:
        break;
    }
    if(m_updateCounter == 2)
        m_updateCounter = 0;
    else
        m_updateCounter++;
}

