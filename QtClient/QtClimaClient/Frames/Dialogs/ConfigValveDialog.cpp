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
        ui->btnManual->setChecked(true);
    else
        ui->btnAuto->setCheckable(true);

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


void ConfigValveDialog::on_horizontalScrollBar_valueChanged(int value)
{
    if(ui->btnManual->isChecked())
    {
        switch(m_type)
        {
        case Valve:
            m_ventService->UpdateValveState(true, value / 10);
            break;
        case Mine:
            m_ventService->UpdateMineState(true, value / 10);
            break;
        }
    }

    ui->barSetPoint->setValue(value);
}


void ConfigValveDialog::on_btnAccept_clicked()
{
    accept();
}


void ConfigValveDialog::on_btnReject_clicked()
{
    reject();
}


void ConfigValveDialog::on_btnAuto_pressed()
{
    if(ui->btnManual->isChecked())
    {
        switch(m_type)
        {
        case Valve:
            m_ventService->UpdateValveState(false, ui->horizontalScrollBar->value() / 10);
            break;
        case Mine:
            m_ventService->UpdateMineState(false, ui->horizontalScrollBar->value() / 10);
            break;
        }
    }
}


void ConfigValveDialog::on_btnManual_pressed()
{
    if(ui->btnManual->isChecked())
    {
        switch(m_type)
        {
        case Valve:
            m_ventService->UpdateValveState(true, ui->horizontalScrollBar->value() / 10);
            break;
        case Mine:
            m_ventService->UpdateMineState(true, ui->horizontalScrollBar->value() / 10);
            break;
        }
    }
}

