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

    INetworkService *service = ApplicationWorker::Instance()->GetNetworkService("HeaterControllerService");
    if(service != nullptr)
    {
        m_heaterService = dynamic_cast<HeaterControllerService*>(service);
    }

    connect(m_heaterService, &HeaterControllerService::HeaterStateUpdated, this, &TemperatureConfigFrame::onHeaterStateReceived);


    service = ApplicationWorker::Instance()->GetNetworkService("SchedulerControlService");
    if(service != nullptr)
    {
        m_scheduler = dynamic_cast<SchedulerControlService*>(service);
    }

    m_heaterService->GetHeaterStates();


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
    dlg.setState(&m_states[0]);

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
    dlg.setState(&m_states[1]);
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
    HeaterState state(m_states[0]);

//    //state.Info.Key = "HEAT:0";
//    if(state.Info.IsManual)
//    {
//        state.IsRunning = isRunning;
//        m_heaterService->UpdateHeaterState(state);
//    }
}

void TemperatureConfigFrame::onHeater1ModeChanged(bool isManual)
{
//    HeaterState state(m_states[0]);

//    state.Info.Key = "HEAT:0";
//    state.Info.IsManual = isManual;
//    m_heaterService->UpdateHeaterState(state);
}

void TemperatureConfigFrame::onHeater2StateChanged(bool isRunning)
{
    HeaterState state(m_states[1]);

    //state.Info.Key = "HEAT:0";
//    if(state.Info.IsManual)
//    {
//        state.IsRunning = isRunning;
//        m_heaterService->UpdateHeaterState(state);
//    }
}

void TemperatureConfigFrame::onHeater2ModeChanged(bool isManual)
{
//    HeaterState state;
//    state.Info.Key = "HEAT:1";
//    state.Info.IsManual = isManual;
//    m_heaterService->UpdateHeaterState(state);
}

void TemperatureConfigFrame::onHeaterStateReceived(HeaterState state)
{

}

void TemperatureConfigFrame::onHeaterStateListReceived(QList<HeaterState> states)
{
    m_states = states;

}

