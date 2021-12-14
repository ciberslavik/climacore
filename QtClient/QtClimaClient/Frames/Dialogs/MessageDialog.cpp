#include "MessageDialog.h"
#include "ui_MessageDialog.h"

MessageDialog::MessageDialog(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::MessageDialog)
{
    ui->setupUi(this);
}

MessageDialog::MessageDialog(const QString &title, const QString &message, const DialogType dialogType, QWidget *parent) :
    QDialog(parent),
    ui(new Ui::MessageDialog)
{
    ui->setupUi(this);
    m_dialogType = dialogType;
    setDialogType(OkCancelDialog);
    setWindowTitle(title);
    ui->lblMessage->setText(message);
}

MessageDialog::~MessageDialog()
{
    delete ui;
}

void MessageDialog::setTitle(const QString &title)
{
    ui->lblTitle->setText(title);
}

void MessageDialog::setMessage(const QString &message)
{
    ui->lblMessage->setText(message);
}

void MessageDialog::setDialogType(const DialogType &dialogType)
{
    switch (dialogType) {
    case OkCancelDialog:
        ui->btnOK->setText("OK");
        ui->btnCancel->setText("Отмена");
        ui->btnCancel->setVisible(true);
        break;
    case OkDialog:
        ui->btnOK->setText("OK");
        ui->btnCancel->setVisible(false);
        break;
    case YesNoDialog:
        ui->btnOK->setText("Да");
        ui->btnCancel->setText("Нет");
        ui->btnCancel->setVisible(true);
        break;
    }
}

void MessageDialog::on_btnOK_clicked()
{
    accept();
}


void MessageDialog::on_btnCancel_clicked()
{
    reject();
}

