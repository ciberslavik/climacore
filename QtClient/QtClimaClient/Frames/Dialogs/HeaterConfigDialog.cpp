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
    if(m_state.IsManual)
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
        ui->lblGraph->setEnabled(false);
        ui->lblGraphName->setEnabled(false);

        ui->txtSetPoint->setEnabled(true);
        ui->txtHyst->setEnabled(true);
    }
    else
    {
        ui->lblGraph->setEnabled(true);
        ui->lblGraphName->setEnabled(true);

        ui->txtSetPoint->setEnabled(false);
        ui->txtHyst->setEnabled(false);
    }
}

