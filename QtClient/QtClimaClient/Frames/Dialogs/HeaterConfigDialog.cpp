#include "HeaterConfigDialog.h"
#include "ui_HeaterConfigDialog.h"
#include "Frames/Dialogs/inputdigitdialog.h"

HeaterConfigDialog::HeaterConfigDialog(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::HeaterConfigDialog)
{
    ui->setupUi(this);

    ui->cboControlZone->addItem("Фронт");
    ui->cboControlZone->addItem("Тыл");
}

HeaterConfigDialog::~HeaterConfigDialog()
{
    delete ui;
}

void HeaterConfigDialog::setState(HeaterState *state)
{
    m_state = state;
    ui->btnManual->setChecked(m_state->Info.IsManual);

    ui->btnManualOn->setEnabled(m_state->Info.IsManual);
    ui->btnManualOn->setChecked(m_state->IsRunning);
}

HeaterState *HeaterConfigDialog::getState()
{
    return m_state;
}

void HeaterConfigDialog::on_btnAuto_toggled(bool checked)
{
    Q_UNUSED(checked)
    if(ui->btnAuto->isChecked())
    {                                           //Auto mode
        ui->txtSetPoint->setEnabled(false);
        ui->btnManualOn->setEnabled(false);

        m_state->Info.IsManual = false;
        emit onModeChanged(false);
    }
}
void HeaterConfigDialog::on_btnManual_clicked()
{
    if(ui->btnManual->isChecked())
    {
        ui->txtSetPoint->setEnabled(true);
        ui->btnManualOn->setEnabled(true);

        m_state->Info.IsManual = true;
        emit onModeChanged(true);
    }
}

void HeaterConfigDialog::onTxtClicked()
{

}

void HeaterConfigDialog::on_btnManualOn_toggled(bool checked)
{
    m_state->IsRunning = checked;
    emit onStateChanged(checked);
    if(ui->btnManualOn->isChecked())
    {
        ui->btnManualOn->setText("Вкл.");
        emit onStateChanged(true);
    }
    else
    {
        ui->btnManualOn->setText("Выкл.");
        emit onStateChanged(false);
    }
}





void HeaterConfigDialog::on_btnAccept_clicked()
{

    accept();
}


void HeaterConfigDialog::on_btnCancel_clicked()
{
    reject();
}




