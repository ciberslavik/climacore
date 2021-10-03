#include "VentControllerConfigFrame.h"
#include "ui_VentControllerConfigFrame.h"
#include "Services/FrameManager.h"
#include "ApplicationWorker.h"

#include <Frames/Dialogs/inputdigitdialog.h>

VentControllerConfigFrame::VentControllerConfigFrame(QWidget *parent) :
    FrameBase(parent),
    ui(new Ui::VentControllerConfigFrame)
{
    ui->setupUi(this);

    INetworkService *service = ApplicationWorker::Instance()->GetNetworkService("SchedulerControlService");
    if(service != nullptr)
    {
        m_scheduler = dynamic_cast<SchedulerControlService*>(service);
    }
    else
    {
        qDebug() << "Scheduller service not found";
    }

    service = ApplicationWorker::Instance()->GetNetworkService("VentilationControllerService");
    if(service != nullptr)
    {
        m_ventilation = dynamic_cast<VentilationService*>(service);
    }
    else
    {
        qDebug() << "Ventilation service not found";
    }

    connect(m_scheduler, &SchedulerControlService::SchedulerProcessInfoReceived, this, &VentControllerConfigFrame::onSchedulerProcessInfoReceived);
    connect(m_ventilation, &VentilationService::FanInfoReceived, this, &VentControllerConfigFrame::onFanInfoReceived);

    connect(ui->txtAnalogMax, &QClickableLineEdit::clicked, this, &VentControllerConfigFrame::onTxtClicked);
    connect(ui->txtAnalogMin, &QClickableLineEdit::clicked, this, &VentControllerConfigFrame::onTxtClicked);
    connect(ui->txtProportional, &QClickableLineEdit::clicked, this, &VentControllerConfigFrame::onTxtClicked);
}

VentControllerConfigFrame::~VentControllerConfigFrame()
{
    delete ui;
}

void VentControllerConfigFrame::on_btnReturn_clicked()
{
    FrameManager::instance()->PreviousFrame();
}

void VentControllerConfigFrame::onTxtClicked()
{
    QLineEdit *txt = qobject_cast<QLineEdit*>(sender());
    QString oldText = txt->text();
    InputDigitDialog dlg(txt, this);
    if(dlg.exec() == QDialog::Rejected)
    {
        txt->setText(oldText);
    }
    m_analogFan.StartValue = ui->txtAnalogMin->text().toFloat();
    m_analogFan.StopValue = ui->txtAnalogMax->text().toFloat();

    int analogPerformance = m_analogFan.Performance * m_analogFan.FanCount;

    float min = (analogPerformance / 100) * m_analogFan.StartValue;
    float max = (analogPerformance / 100) * m_analogFan.StopValue;

    ui->lblAnalogMin->setText(QString::number(min, 'f', 0));
    ui->lblAnalogMax->setText(QString::number(max, 'f', 0));
}

void VentControllerConfigFrame::onSchedulerProcessInfoReceived(SchedulerProcessInfo info)
{
    ui->lblProfileMax->setText(QString::number(info.VentilationMaxPoint, 'f', 2));
    ui->lblProfileMin->setText(QString::number(info.VentilationMinPoint, 'f', 2));
    ui->lblSetpoint->setText(QString::number(info.VentilationSetPoint,'f', 2));

    float max = info.VentilationMaxPoint * info.CurrentHeads;
    ui->lblMaxPerformance->setText(QString::number(max, 'f', 0));
    float min = info.VentilationMinPoint * info.CurrentHeads;
    ui->lblMinPerformance->setText(QString::number(min, 'f', 0));

    m_ventilation->GetFanInfo("FAN:0");
}

void VentControllerConfigFrame::onFanInfoReceived(FanInfo info)
{
    m_analogFan = info;
    ui->txtAnalogMax->setText(QString::number(info.StopValue));
    ui->txtAnalogMin->setText(QString::number(info.StartValue));

    int analogPerformance = info.Performance * info.FanCount;

    float min = (analogPerformance / 100) * info.StartValue;
    float max = (analogPerformance / 100) * info.StopValue;

    ui->lblAnalogMin->setText(QString::number(min, 'f', 0));
    ui->lblAnalogMax->setText(QString::number(max, 'f', 0));

}

void VentControllerConfigFrame::onFanUpdated()
{

}



void VentControllerConfigFrame::showEvent(QShowEvent *event)
{
    Q_UNUSED(event)
    m_scheduler->GetProcessInfo();
}

void VentControllerConfigFrame::on_btnApply_clicked()
{
     m_analogFan.StartValue = ui->txtAnalogMax->text().toFloat();
     m_analogFan.StopValue = ui->txtAnalogMin->text().toFloat();

     connect(m_ventilation, &VentilationService::CreateOrUpdateComplete, this, &VentControllerConfigFrame::onFanUpdated);
     m_ventilation->CreateOrUpdateFan(m_analogFan);

    float proportional = ui->txtProportional->text().toFloat();
}

