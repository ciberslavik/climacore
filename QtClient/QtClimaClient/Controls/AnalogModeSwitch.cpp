#include "AnalogModeSwitch.h"
#include "ui_AnalogModeSwitch.h"

#include <Frames/Dialogs/inputdigitdialog.h>

AnalogModeSwitch::AnalogModeSwitch(QWidget *parent) :
    QDialog(parent),
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

void AnalogModeSwitch::setMode(const FanModeEnum &mode)
{
    if(mode == FanModeEnum::Manual)
        ui->btnManual->setChecked(true);
    else if (mode == FanModeEnum::Auto)
        ui->btnAuto->setChecked(true);
}

void AnalogModeSwitch::setTitle(const QString &title)
{
    ui->lblTitle->setText(title);
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
    accept();
}


void AnalogModeSwitch::on_btnReject_clicked()
{
    reject();
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
    ui->lblManualValue->setText(QString::number(val, 'f', 0) + "%");
    emit fanValueChanged(val);
}


void AnalogModeSwitch::on_btnAuto_toggled(bool checked)
{
    Q_UNUSED(checked)
    if(ui->btnAuto->isChecked())
    {
        ui->groupBox_2->setEnabled(false);
        emit fanModeChanged(FanModeEnum::Auto);
    }
    else
    {
        ui->groupBox_2->setEnabled(true);
        emit fanModeChanged(FanModeEnum::Manual);
    }
}


void AnalogModeSwitch::on_sldManualPower_valueChanged(int value)
{
    ui->lblManualValue->setText(QString::number(value / 10, 'f', 0) + "%");
}

