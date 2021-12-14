#include "EditTimerDialog.h"
#include "ui_EditTimerDialog.h"

#include <QMessageBox>

EditTimerDialog::EditTimerDialog(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::EditTimerDialog)
{
    ui->setupUi(this);
}

EditTimerDialog::~EditTimerDialog()
{
    delete ui;
}

void EditTimerDialog::setTimerInfo(LightTimerItem *info)
{
    ui->txtOnTime->setText(info->OnTime.toString("hh:mm"));
    ui->txtOffTime->setText(info->OffTime.toString("hh:mm"));
}

QDateTime EditTimerDialog::onTime() const
{
    return m_onTime;
}

QDateTime EditTimerDialog::offTime() const
{
    return m_offTime;
}

void EditTimerDialog::on_btnAccept_clicked()
{
    m_onTime = QDateTime(QDate(), QTime::fromString(ui->txtOnTime->text()));
    m_offTime = QDateTime(QDate(), QTime::fromString(ui->txtOffTime->text()));
    if(m_onTime > m_offTime)
    {
        QMessageBox mb;
        mb.setWindowTitle("Ошибка");
        mb.setInformativeText("Время выключения не может быть меньше времени включения!");
        mb.setStandardButtons(QMessageBox::StandardButtons(
                                  QMessageBox::StandardButton::Ok |
                                  QMessageBox::StandardButton::Cancel));
        int result = mb.exec();
        if(result == QMessageBox::Ok)
            return;
        else if(result == QMessageBox::Cancel)
            reject();
    }
    accept();
}


void EditTimerDialog::on_btnCancel_clicked()
{
    reject();
}

