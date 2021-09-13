#include "StartProductionDialog.h"
#include "inputdigitdialog.h"
#include "ui_StartProductionDialog.h"

#include <QLineEdit>

#include <Services/FrameManager.h>

StartProductionDialog::StartProductionDialog(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::StartProductionDialog)
{
    ui->setupUi(this);
    connect(ui->txtHeadsCount, &QClickableLineEdit::clicked, this, &StartProductionDialog::onTxtClicked);
}

StartProductionDialog::~StartProductionDialog()
{
    delete ui;
}

QDateTime StartProductionDialog::getStartDate()
{
    return ui->dteStartDate->dateTime();
}

QDateTime StartProductionDialog::getPlendingDate()
{
    return ui->dtePlendingDate->dateTime();
}

int StartProductionDialog::getHeadsCount()
{
    return ui->txtHeadsCount->text().toInt();
}

void StartProductionDialog::onTxtClicked()
{
    QLineEdit *txt = dynamic_cast<QLineEdit*>(sender());
    QString prevValue = txt->text();
    InputDigitDialog *dlg = new InputDigitDialog(txt, FrameManager::instance()->MainWindow());
    if(dlg->exec() == QDialog::Rejected)
    {
        txt->setText(prevValue);
    }
}

void StartProductionDialog::on_btnAccept_clicked()
{
    accept();
}


void StartProductionDialog::on_btnCancel_clicked()
{
    reject();
}

