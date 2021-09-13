#include "StartPreparingDialog.h"
#include "ui_StartPreparingDialog.h"

#include <QLineEdit>
#include "inputdigitdialog.h"
#include <Services/FrameManager.h>

StartPreparingDialog::StartPreparingDialog(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::StartPreparingDialog)
{
    ui->setupUi(this);

    connect(ui->txtTemp, &QClickableLineEdit::clicked, this, &StartPreparingDialog::onTxtClicked);
}

StartPreparingDialog::~StartPreparingDialog()
{
    delete ui;
}

float StartPreparingDialog::getTemperature()
{
    return ui->txtTemp->text().toFloat();
}

QDateTime StartPreparingDialog::getStartDate()
{
    return ui->dteStartDate->dateTime();
}

void StartPreparingDialog::onTxtClicked()
{
    QLineEdit *txt = dynamic_cast<QLineEdit*>(sender());
    QString prevValue = txt->text();
    InputDigitDialog *dlg = new InputDigitDialog(txt, FrameManager::instance()->MainWindow());
    if(dlg->exec() == QDialog::Rejected)
    {
        txt->setText(prevValue);
    }
}

void StartPreparingDialog::on_btnAccept_clicked()
{
    accept();
}


void StartPreparingDialog::on_btnCancel_clicked()
{
    reject();
}

