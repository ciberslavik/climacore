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


    connect(m_ventService, &VentilationService::FanInfoListReceived, this, &VentilationOverviewFrame::onFanInfosReceived);
    connect(m_ventService, &VentilationService::MineStateReceived, this, &VentilationOverviewFrame::onMineStateReceived);
    connect(m_ventService, &VentilationService::ValveStateReceived, this, &VentilationOverviewFrame::onValveStateReceived);
    connect(m_ventService, &VentilationService::VentilationStatusReceived, this, &VentilationOverviewFrame::onVentilationStatusReceived);

    m_updateTimer = TimerPool::instance()->getUpdateTimer();
    m_needConfig = false;
}

VentilationOverviewFrame::~VentilationOverviewFrame()
{
    disconnect(m_ventService, &VentilationService::FanInfoListReceived, this, &VentilationOverviewFrame::onFanInfosReceived);
    disconnect(m_ventService, &VentilationService::MineStateReceived, this, &VentilationOverviewFrame::onMineStateReceived);
    disconnect(m_ventService, &VentilationService::ValveStateReceived, this, &VentilationOverviewFrame::onValveStateReceived);
    disconnect(m_ventService, &VentilationService::VentilationStatusReceived, this, &VentilationOverviewFrame::onVentilationStatusReceived);

    disconnect(m_updateTimer, &QTimer::timeout, this, &VentilationOverviewFrame::onUpdateTimer);
    delete ui;
}


void VentilationOverviewFrame::closeEvent(QCloseEvent *event)
{
    Q_UNUSED(event)
}

void VentilationOverviewFrame::showEvent(QShowEvent *event)
{
    Q_UNUSED(event)
    connect(m_updateTimer, &QTimer::timeout, this, &VentilationOverviewFrame::onUpdateTimer);
    //m_ventService->GetFanStateList();
}


void VentilationOverviewFrame::on_pushButton_clicked()
{
    disconnect(m_updateTimer, &QTimer::timeout, this, &VentilationOverviewFrame::onUpdateTimer);
    FrameManager::instance()->PreviousFrame();
}

void VentilationOverviewFrame::onFanInfosReceived(QMap<QString, FanInfo> infos)
{
    m_fanInfos = infos;

    if(m_fanWidgets.count() == m_fanInfos.count())
        updateFanWidgets();
    else
        createFanWidgets();

    if(m_needConfig)
    {
        m_needConfig = false;
        VentilationConfigFrame *frame = new VentilationConfigFrame();
        qDebug()<<"Ventilation editor in FanInfosReceived";
        disconnect(m_updateTimer, &QTimer::timeout, this, &VentilationOverviewFrame::onUpdateTimer);
        FrameManager::instance()->setCurrentFrame(frame);
    }
}

void VentilationOverviewFrame::onEditFanStateChanged(const QString &fanKey, FanStateEnum_t newState)
{
    qDebug() << "FAN State changed" << fanKey << " mode:" << (int)newState;

    if(m_fanInfos[fanKey].Mode == (int)FanModeEnum::Manual)
    {
        m_fanInfos[fanKey].State = (int)newState;

        m_ventService->SetFanState(fanKey, newState);
    }
}

void VentilationOverviewFrame::onEditFanModeChanged(const QString &fanKey, FanMode_t newMode)
{
    qDebug() <<"Edit mode changed:" << fanKey << " mode:" << (int)newMode;

    m_fanInfos[fanKey].Mode = (int)newMode;

    m_ventService->SetFanMode(fanKey, newMode);
}

void VentilationOverviewFrame::onEditFanValueChanged(const QString &fanKey, const float &value)
{
    if(m_fanInfos[fanKey].Mode == (int)FanModeEnum::Manual)
    {
        m_fanInfos[fanKey].AnalogPower = value;

        m_ventService->SetFanState(fanKey, (FanStateEnum)m_fanInfos[fanKey].State, value);
    }
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

void VentilationOverviewFrame::onVentilationStatusReceived(float valvePos, float valveSetpoint, float minePos, float mineSetpoint, float ventilationSp)
{
    ui->pbValves->setValue(valvePos);
    ui->pbMines->setValue(minePos);
    ui->lblValveSetPoint->setText(QString::number(valveSetpoint, 'f', 1));
    ui->lblMintSetPoint->setText(QString::number(mineSetpoint, 'f', 1));
    ui->lblVentSetPoint->setText(QString::number(ventilationSp, 'f', 2));

    if(m_needConfig)
    {
        m_needConfig = false;
        VentilationConfigFrame *frame = new VentilationConfigFrame();
        qDebug()<<"Ventilation editor in VentilationStatus";
        disconnect(m_updateTimer, &QTimer::timeout, this, &VentilationOverviewFrame::onUpdateTimer);
        FrameManager::instance()->setCurrentFrame(frame);
    }
}

void VentilationOverviewFrame::createFanWidgets()
{
    if(m_fanWidgets.count()>0)
    {
        removeFanWidgets();
    }
    QList<FanInfo> infos = m_fanInfos.values();
    qSort(infos.begin(),infos.end(),
              [](const FanInfo &a, const FanInfo &b) -> bool
    {
        int na = a.Key.mid(4).toInt();
        int nb = b.Key.mid(4).toInt();

        return na < nb;
    });
    foreach(const FanInfo &info, infos)
    {

        FanWidget *widget = new FanWidget(info.Key, info.IsAnalog, this);
        widget->setFanName(info.FanName);
        if(info.Mode == (int)FanModeEnum::Manual)
            widget->setFanMode(FanModeEnum::Manual);
        else
        {
            widget->setFanMode((FanMode_t)info.Mode);
        }
        widget->setFanState((FanStateEnum)info.State);

        connect(widget, &FanWidget::FanModeChanged, this, &VentilationOverviewFrame::onEditFanModeChanged);
        connect(widget, &FanWidget::FanStateChanged, this, &VentilationOverviewFrame::onEditFanStateChanged);
        connect(widget, &FanWidget::EditBegin, this, &VentilationOverviewFrame::onBeginEditFan);
        connect(widget, &FanWidget::EditAccept, this, &VentilationOverviewFrame::onAcceptEditFan);
        connect(widget, &FanWidget::EditCancel, this, &VentilationOverviewFrame::onCancelEditFan);
        connect(widget, &FanWidget::FanAnalogValueChanged, this, &VentilationOverviewFrame::onEditFanValueChanged);
        if(info.IsAnalog)
        {
            widget->setAnalogValue(info.AnalogPower);
            widget->setAnalogMax(info.StartValue);
            widget->setAnalogMin(info.StopValue);
            ui->layAnalogFans->addWidget(widget);
            widget->setMaximumSize(QSize(110,1100));
        }
        else
        {
            ui->layDiscrFans->addWidget(widget);
        }
        m_fanWidgets.insert(info.Key, widget);
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
    for(int i = 0; i < m_fanInfos.count(); i++)
    {
        FanInfo *s = &m_fanInfos[m_fanInfos.keys().at(i)];
        FanWidget *w = m_fanWidgets[s->Key];

        w->setFanName(s->FanName);
        if(s->Mode == (int)FanModeEnum::Manual)
            w->setFanMode(FanModeEnum::Manual);
        else
        {
            w->setFanMode((FanMode_t)s->Mode);
        }
        w->setFanState((FanStateEnum)s->State, s->IsAlarm);
        if(s->IsAnalog)
        {
            w->setAnalogValue(s->AnalogPower);
        }
    }
}


void VentilationOverviewFrame::on_btnConfigure_clicked()
{
    m_needConfig = true;

}

void VentilationOverviewFrame::onBeginEditFan(const QString &fanKey)
{
    m_updateTimer->stop();
    qDebug() << "Begin edit fan:" << fanKey;
}

void VentilationOverviewFrame::onAcceptEditFan(const QString &fanKey)
{
    m_updateTimer->start();
    qDebug() << "Accept edit fan:" << fanKey;
    if(m_fanInfos[fanKey].IsAnalog)
    {

    }
}

void VentilationOverviewFrame::onCancelEditFan(const QString &fanKey)
{
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
    disconnect(m_updateTimer, &QTimer::timeout, this, &VentilationOverviewFrame::onUpdateTimer);
    FrameManager::instance()->setCurrentFrame(configFrame);
}

void VentilationOverviewFrame::onUpdateTimer()
{
    switch (m_updateCounter) {
        case 0:
            m_ventService->GetVentilationStatus();
        break;
        case 1:
            m_ventService->GetFanInfoList();
        break;
        default:
        break;
    }
    if(m_updateCounter == 1)
        m_updateCounter = 0;
    else
        m_updateCounter++;
}


void VentilationOverviewFrame::on_btnVentPWMConfig_clicked()
{

}


void VentilationOverviewFrame::on_btnResetAlarms_clicked()
{
    m_ventService->ResetAlarms();
}

