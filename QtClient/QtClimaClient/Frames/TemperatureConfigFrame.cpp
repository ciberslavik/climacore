#include "TemperatureConfigFrame.h"
#include "ui_TemperatureConfigFrame.h"

#include <Services/FrameManager.h>

#include <Frames/Dialogs/HeaterConfigDialog.h>

#include <ApplicationWorker.h>

TemperatureConfigFrame::TemperatureConfigFrame(QWidget *parent) :
    FrameBase(parent),
    ui(new Ui::TemperatureConfigFrame),
    m_ProfileSelector(nullptr),
    m_heaterService(nullptr)
{
    ui->setupUi(this);
    connect(ui->txtMinTemp, &QClickableLineEdit::clicked, this, &TemperatureConfigFrame::onTxtClicked);
    connect(ui->txtMaxTemp, &QClickableLineEdit::clicked, this, &TemperatureConfigFrame::onTxtClicked);
    connect(ui->txtCorrection, &QClickableLineEdit::clicked, this, &TemperatureConfigFrame::onTxtClicked);

    INetworkService *service = ApplicationWorker::Instance()->GetNetworkService("HeaterService");
    if(service != nullptr)
    {
        m_heaterService = dynamic_cast<HeaterControllerService*>(service);
    }
}

TemperatureConfigFrame::~TemperatureConfigFrame()
{
    delete ui;
}

void TemperatureConfigFrame::on_btnReturn_clicked()
{
    FrameManager::instance()->PreviousFrame();
}

void TemperatureConfigFrame::onProfileSelected(ProfileInfo profileInfo)
{
    ui->lblProfileName->setText(profileInfo.Name);
}

QString TemperatureConfigFrame::getFrameName()
{
    return "TemperatureConfigFrame";
}


void TemperatureConfigFrame::on_btnSelectGraph_clicked()
{
    m_ProfileSelector = new SelectProfileFrame(ProfileType::Temperature);
    connect(m_ProfileSelector,
            &SelectProfileFrame::ProfileSelected,
            this,
            &TemperatureConfigFrame::onProfileSelected);

    FrameManager::instance()->setCurrentFrame(m_ProfileSelector);
}


void TemperatureConfigFrame::on_btnHeater1_clicked()
{
    HeaterConfigDialog dlg(FrameManager::instance()->MainWindow());
    connect(&dlg, &HeaterConfigDialog::onModeChanged, this, &TemperatureConfigFrame::onHeater1ModeChanged);
    connect(&dlg, &HeaterConfigDialog::onStateChanged, this, &TemperatureConfigFrame::onHeater1StateChanged);
    if(dlg.exec()==QDialog::Accepted)
    {

    }
    disconnect(&dlg, &HeaterConfigDialog::onModeChanged, this, &TemperatureConfigFrame::onHeater1ModeChanged);
    disconnect(&dlg, &HeaterConfigDialog::onStateChanged, this, &TemperatureConfigFrame::onHeater1StateChanged);
}


void TemperatureConfigFrame::on_btnHeater2_clicked()
{
    HeaterConfigDialog dlg(FrameManager::instance()->MainWindow());
    connect(&dlg, &HeaterConfigDialog::onModeChanged, this, &TemperatureConfigFrame::onHeater2ModeChanged);
    connect(&dlg, &HeaterConfigDialog::onStateChanged, this, &TemperatureConfigFrame::onHeater2StateChanged);
    if(dlg.exec()==QDialog::Accepted)
    {

    }
    disconnect(&dlg, &HeaterConfigDialog::onModeChanged, this, &TemperatureConfigFrame::onHeater2ModeChanged);
    disconnect(&dlg, &HeaterConfigDialog::onStateChanged, this, &TemperatureConfigFrame::onHeater2StateChanged);
}

void TemperatureConfigFrame::onTxtClicked()
{
    QLineEdit *txt = static_cast<QLineEdit*>(sender());
    QString oldValue = txt->text();
    InputDigitDialog dlg(txt,FrameManager::instance()->MainWindow());
    if(dlg.exec()==QDialog::Rejected)
    {
        txt->setText(oldValue);
    }

}

void TemperatureConfigFrame::onHeater1StateChanged(bool isRunning)
{
    qDebug()<< "State changed";
}

void TemperatureConfigFrame::onHeater1ModeChanged(bool isManual)
{
qDebug()<< "Mode changed";
}

void TemperatureConfigFrame::onHeater2StateChanged(bool isRunning)
{
qDebug()<< "State2 changed";
}

void TemperatureConfigFrame::onHeater2ModeChanged(bool isManual)
{
qDebug()<< "Mode2 changed";
}

