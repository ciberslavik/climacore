#include "TemperatureConfigFrame.h"
#include "TemperatureOwerviewFrame.h"
#include "ui_TemperatureOwerviewFrame.h"

#include <Frames/Dialogs/inputdigitdialog.h>
#include <Network/GenericServices/SystemStatusService.h>
#include <Services/FrameManager.h>



TemperatureOwerviewFrame::TemperatureOwerviewFrame(QWidget *parent) :
    FrameBase(parent),
    ui(new Ui::TemperatureOwerviewFrame)
{
    ui->setupUi(this);
    setTitle("Обзор температуры");

    INetworkService *systemStatusService = ApplicationWorker::Instance()->GetNetworkService("SystemStatusService");

    if(systemStatusService != nullptr)
    {
        m_systemStatus = dynamic_cast<SystemStatusService*>(systemStatusService);
    }
    else
    {
        qDebug() << "Service not found";
    }

    m_updateTimer = TimerPool::instance()->getUpdateTimer();


    connect(ui->txtCorrection, &QClickableLineEdit::clicked, this, &TemperatureOwerviewFrame::onCorrectionClicked);


}

TemperatureOwerviewFrame::~TemperatureOwerviewFrame()
{
    qDebug() << "TemperatureOverwievFrame deleted";
    disconnect(m_systemStatus, &SystemStatusService::onTemperatureStatusRecv, this, &TemperatureOwerviewFrame::onTempStateRecv);
    disconnect(m_updateTimer, &QTimer::timeout, this, &TemperatureOwerviewFrame::onUpdateTimeout);
    delete ui;
}

void TemperatureOwerviewFrame::on_btnConfigTemp_clicked()
{
    TemperatureConfigFrame *tempConfigFrame = new TemperatureConfigFrame();
    FrameManager::instance()->setCurrentFrame(tempConfigFrame);
}

void TemperatureOwerviewFrame::onUpdateTimeout()
{
    m_systemStatus->getTemperatureStatus();
}

void TemperatureOwerviewFrame::onTempStateRecv(TemperatureStateResponse *response)
{
    ui->lblFront->setText(QString::number(response->FrontTemperature));
    ui->lblRear->setText(QString::number(response->RearTemperature));
    ui->lblOutdoor->setText(QString::number(response->OutdoorTemperature));
    ui->txtCorrection->setText(QString::number(response->TemperatureCorrection));
}

QString TemperatureOwerviewFrame::getFrameName()
{
    return "TemperatureOwerviewFrame";
}

void TemperatureOwerviewFrame::closeEvent(QCloseEvent *event)
{
    disconnect(
                m_systemStatus, &SystemStatusService::onTemperatureStatusRecv,
                this, &TemperatureOwerviewFrame::onTempStateRecv);
    disconnect(
                m_updateTimer, &QTimer::timeout,
                this, &TemperatureOwerviewFrame::onUpdateTimeout);
}

void TemperatureOwerviewFrame::showEvent(QShowEvent *event)
{
    connect(
                m_systemStatus, &SystemStatusService::onTemperatureStatusRecv,
                this, &TemperatureOwerviewFrame::onTempStateRecv);
    connect(
                m_updateTimer, &QTimer::timeout,
                this, &TemperatureOwerviewFrame::onUpdateTimeout);
    //request data on show frame
    m_systemStatus->getTemperatureStatus();
}


void TemperatureOwerviewFrame::on_btnReturn_clicked()
{
    FrameManager::instance()->PreviousFrame();
}

void TemperatureOwerviewFrame::onCorrectionClicked()
{
    QClickableLineEdit *lineEdit = dynamic_cast<QClickableLineEdit*>(sender());
    QString prevValue = lineEdit->text();
    InputDigitDialog *dlg = new InputDigitDialog(lineEdit, FrameManager::instance()->MainWindow());

    if(dlg->exec()==QDialog::Accepted)
    {
        return;
    }
    lineEdit->setText(prevValue);
}

