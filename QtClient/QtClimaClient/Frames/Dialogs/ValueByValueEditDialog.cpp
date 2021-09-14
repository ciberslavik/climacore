#include "ValueByValueEditDialog.h"
#include "inputdigitdialog.h"
#include "ui_ValueByValueEditDialog.h"
#include "Services/FrameManager.h"

ValueByValueEditDialog::ValueByValueEditDialog(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::ValueByValueEditDialog)
{
    ui->setupUi(this);
    connect(ui->txtValueX, &QClickableLineEdit::clicked, this, &ValueByValueEditDialog::onTxtClicked);
    connect(ui->txtValueY, &QClickableLineEdit::clicked, this, &ValueByValueEditDialog::onTxtClicked);
}

ValueByValueEditDialog::~ValueByValueEditDialog()
{
    delete ui;
}

void ValueByValueEditDialog::setTitle(const QString &title)
{
    ui->lblTitle->setText(title);
}

void ValueByValueEditDialog::setValueXTitle(const QString &title)
{
    ui->lblValueXTitle->setText(title);
}

void ValueByValueEditDialog::setValueYTitle(const QString &title)
{
    ui->lblValueYTitle->setText(title);
}

void ValueByValueEditDialog::setValueXSuffix(const QString &suffix)
{
    ui->lblValueXSuffix->setText(suffix);
}

void ValueByValueEditDialog::setValueYSuffix(const QString &suffix)
{
    ui->lblValueYSuffix->setText(suffix);
}

void ValueByValueEditDialog::setValueX(float valuex)
{
    ui->txtValueX->setText(QString::number(valuex));
}

float ValueByValueEditDialog::getValueX()
{
    return ui->txtValueX->text().toFloat();
}

void ValueByValueEditDialog::setValueY(float valuey)
{
    ui->txtValueY->setText(QString::number(valuey));
}

float ValueByValueEditDialog::getValueY()
{
    return ui->txtValueY->text().toFloat();
}

void ValueByValueEditDialog::on_btnAccept_clicked()
{
    accept();
}


void ValueByValueEditDialog::on_btnCancel_clicked()
{
    reject();
}

void ValueByValueEditDialog::onTxtClicked()
{
    QClickableLineEdit *lineEdit = dynamic_cast<QClickableLineEdit*>(sender());
    QString prevValue = lineEdit->text();
    InputDigitDialog *dlg = new InputDigitDialog(lineEdit, FrameManager::instance()->MainWindow());

    if(dlg->exec()==QDialog::Rejected)
    {
        lineEdit->setText(prevValue);
    }

    dlg->deleteLater();
}

