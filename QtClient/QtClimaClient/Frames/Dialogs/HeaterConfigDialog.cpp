#include "HeaterConfigDialog.h"
#include "ui_HeaterConfigDialog.h"

HeaterConfigDialog::HeaterConfigDialog(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::HeaterConfigDialog)
{
    ui->setupUi(this);
}

HeaterConfigDialog::~HeaterConfigDialog()
{
    delete ui;
}

void HeaterConfigDialog::setState(HeaterState state)
{
    m_state = state;
    if(m_state.Info.IsManual)
        ui->btnManual->toggle();
    else
        ui->btnAuto->toggle();

    ui->btnManualOn->setChecked(m_state.IsRunning);
}

HeaterState HeaterConfigDialog::getState()
{

    return m_state;
}

void HeaterConfigDialog::on_btnAuto_toggled(bool checked)
{
    if(checked)
    {
        ui->txtSetPoint->setEnabled(true);
        ui->txtHyst->setEnabled(true);
        ui->btnManualOn->setEnabled(false);
        m_state.Info.IsManual = true;
        emit onModeChanged(true);
    }
    else
    {
        ui->txtSetPoint->setEnabled(false);
        ui->txtHyst->setEnabled(true);
        ui->btnManualOn->setEnabled(true);
        m_state.Info.IsManual = false;
        emit onModeChanged(false);
    }
}


void HeaterConfigDialog::on_btnManualOn_toggled(bool checked)
{
    m_state.IsRunning = checked;
    emit onStateChanged(checked);
    if(checked)
        ui->btnManualOn->setText("Вкл.");
    else
        ui->btnManualOn->setText("Выкл.");
}

