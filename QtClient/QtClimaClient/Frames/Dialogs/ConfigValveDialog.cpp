#include "ConfigValveDialog.h"
#include "ui_ConfigValveDialog.h"
#include <TimerPool.h>

#include <ApplicationWorker.h>

ConfigValveDialog::ConfigValveDialog(ValveType valveType, QWidget *parent) :
    QDialog(parent),
    ui(new Ui::ConfigValveDialog)
{
    m_type = valveType;
    ui->setupUi(this);

    INetworkService *service = ApplicationWorker::Instance()->GetNetworkService("VentilationControllerService");
    if(service != nullptr)
    {
        m_ventService = dynamic_cast<VentilationService*>(service);
    }
    switch(m_type)
    {
    case Valve:
        connect(m_ventService, &VentilationService::ValveStateReceived, this, &ConfigValveDialog::onServoStateReceived);
        break;
    case Mine:
        connect(m_ventService, &VentilationService::MineStateReceived, this, &ConfigValveDialog::onServoStateReceived);
        break;
    }

    m_updateTimer = TimerPool::instance()->getUpdateTimer();
    connect(m_updateTimer, &QTimer::timeout, this, &ConfigValveDialog::updateState);
    m_first = true;
}

ConfigValveDialog::~ConfigValveDialog()
{
    disconnect(m_updateTimer, &QTimer::timeout, this, &ConfigValveDialog::updateState);
    switch(m_type)
    {
    case Valve:
        disconnect(m_ventService, &VentilationService::ValveStateReceived, this, &ConfigValveDialog::onServoStateReceived);
        break;
    case Mine:
        disconnect(m_ventService, &VentilationService::MineStateReceived, this, &ConfigValveDialog::onServoStateReceived);
        break;
    }

    delete ui;
}

void ConfigValveDialog::onServoStateReceived(bool isManual, float currPos, float setPoint)
{
    if(isManual)
    {
        ui->btnManual->setChecked(true);
        ui->grpManual->setEnabled(true);
    }
    else
    {
        ui->btnAuto->setChecked(true);
        ui->grpManual->setEnabled(false);
    }
    if(m_first)
    {
        m_first = false;
        ui->txtManualValue->setText(QString::number(setPoint, 'f', 1));
    }
    ui->barCurrent->setValue((currPos * 10));
    ui->barSetPoint->setValue(setPoint * 10);
}

void ConfigValveDialog::updateState()
{
    switch(m_type)
    {
    case Valve:
        m_ventService->GetValveState();
        break;
    case Mine:
        m_ventService->GetMineState();
        break;
    }
}

void ConfigValveDialog::closeEvent(QCloseEvent *event)
{
    Q_UNUSED(event);
}

void ConfigValveDialog::showEvent(QShowEvent *event)
{
    Q_UNUSED(event);
}

void ConfigValveDialog::on_btnAccept_clicked()
{
    accept();
}


void ConfigValveDialog::on_btnReject_clicked()
{
    reject();
}

void ConfigValveDialog::on_btnMinus_clicked()
{
    float value = ui->txtManualValue->text().toFloat();

    value = value - 1;

    if(value < 0)
        value = 0;

    ui->txtManualValue->setText(QString::number(value, 'f', 1));
    if(ui->btnManual->isChecked())
    {

        switch (m_type) {
            case ConfigValveDialog::Valve:
                m_ventService->UpdateValveState(true, value);
            break;
            case ConfigValveDialog::Mine:
                m_ventService->UpdateValveState(true, value);
            break;

        }
    }
}

void ConfigValveDialog::on_btnPlus_clicked()
{
    float value = ui->txtManualValue->text().toFloat();

    value = value + 1;

    if(value > 100)
        value = 100;

    ui->txtManualValue->setText(QString::number(value, 'f', 1));
    if(ui->btnManual->isChecked())
    {

        switch (m_type) {
            case ConfigValveDialog::Valve:
                m_ventService->UpdateValveState(true, value);
            break;
            case ConfigValveDialog::Mine:
                m_ventService->UpdateValveState(true, value);
            break;

        }
    }
}

void ConfigValveDialog::on_btnAuto_toggled(bool checked)
{
    Q_UNUSED(checked)

    if(ui->btnAuto->isChecked())
    {
        ui->grpManual->setEnabled(false);
    }
    if(ui->btnManual->isChecked())
    {
        ui->grpManual->setEnabled(true);
    }
    float manual = ui->txtManualValue->text().toFloat();

    switch (m_type) {
        case ConfigValveDialog::Valve:
            m_ventService->UpdateValveState(ui->btnManual->isChecked(), manual);
        break;
        case ConfigValveDialog::Mine:
            m_ventService->UpdateMineState(ui->btnManual->isChecked(), manual);
        break;
    }
}





void ConfigValveDialog::on_btnOpen_clicked()
{
    ui->txtManualValue->setText(QString::number(100, 'f', 1));
    if(ui->btnManual->isChecked())
    {

        switch (m_type) {
            case ConfigValveDialog::Valve:
                m_ventService->UpdateValveState(true, 100);
            break;
            case ConfigValveDialog::Mine:
                m_ventService->UpdateValveState(true, 100);
            break;

        }
    }
}


void ConfigValveDialog::on_btnClose_clicked()
{
    ui->txtManualValue->setText(QString::number(0, 'f', 1));
    if(ui->btnManual->isChecked())
    {

        switch (m_type) {
            case ConfigValveDialog::Valve:
                m_ventService->UpdateValveState(true, 0);
            break;
            case ConfigValveDialog::Mine:
                m_ventService->UpdateValveState(true, 0);
            break;

        }
    }
}

