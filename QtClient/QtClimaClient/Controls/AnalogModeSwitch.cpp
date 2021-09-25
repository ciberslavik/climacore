#include "AnalogModeSwitch.h"
#include "ui_AnalogModeSwitch.h"

#include <Frames/Dialogs/inputdigitdialog.h>

AnalogModeSwitch::AnalogModeSwitch(QWidget *parent) :
    QWidget(parent),
    ui(new Ui::AnalogModeSwitch)
{
    ui->setupUi(this);
}

AnalogModeSwitch::~AnalogModeSwitch()
{
    delete ui;
}

bool AnalogModeSwitch::getMode()
{
    if(ui->btnAuto->isChecked())
        return false;
    else
        return true;
}

void AnalogModeSwitch::setMode(const bool &mode)
{
    if(mode)
        ui->btnManual->setChecked(true);
    else
        ui->btnAuto->setChecked(true);
}

float AnalogModeSwitch::getManualPower()
{
    return ui->sldManualPower->value() / 10;
}

void AnalogModeSwitch::setManualPower(const float &power)
{
    ui->sldManualPower->setValue(power * 10);
}

float AnalogModeSwitch::getMaxLimit()
{
    return ui->txtMaxAnalog->text().toFloat();
}

void AnalogModeSwitch::setMaxLimit(const float &maxLimit)
{
    ui->txtMaxAnalog->setText(QString::number(maxLimit));
}

float AnalogModeSwitch::getMinLimit()
{
    return ui->txtMinAnalog->text().toFloat();
}

void AnalogModeSwitch::setMinLimit(const float &minLimit)
{
    ui->txtMinAnalog->setText(QString::number(minLimit));
}

void AnalogModeSwitch::on_btnAccept_clicked()
{
    emit AcceptEdit();
}


void AnalogModeSwitch::on_btnReject_clicked()
{
    emit RejectEdit();
}

void AnalogModeSwitch::onTxtClicked()
{
    QLineEdit *txt = dynamic_cast<QLineEdit*>(sender());
    QString prev = txt->text();
    InputDigitDialog *dlg = new InputDigitDialog(txt, this);
    if(dlg->exec() == QDialog::Rejected)
    {
        txt->setText(prev);
    }
}


void AnalogModeSwitch::on_sldManualPower_sliderReleased()
{
    float val = ui->sldManualPower->value() / 10;
    ui->lblManualValue->setText(QString::number(val,'f',1));
}


void AnalogModeSwitch::on_btnAuto_toggled(bool checked)
{
    Q_UNUSED(checked)
    if(ui->btnAuto->isChecked())
    {
        ui->groupBox_2->setEnabled(false);
        emit ModeChanged(false);
    }
    else
    {
        ui->groupBox_2->setEnabled(true);
        emit ModeChanged(true);
    }


}

