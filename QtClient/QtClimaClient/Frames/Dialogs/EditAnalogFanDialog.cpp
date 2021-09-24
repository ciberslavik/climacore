#include "EditAnalogFanDialog.h"
#include "inputdigitdialog.h"
#include "ui_EditAnalogFanDialog.h"

EditAnalogFanDialog::EditAnalogFanDialog(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::EditAnalogFanDialog)
{
    ui->setupUi(this);

    connect(ui->txtMin, &QClickableLineEdit::clicked, this, &EditAnalogFanDialog::onTxtClicked);
    connect(ui->txtMax, &QClickableLineEdit::clicked, this, &EditAnalogFanDialog::onTxtClicked);
    connect(ui->txtSetPoint, &QClickableLineEdit::clicked, this, &EditAnalogFanDialog::onTxtClicked);
}

EditAnalogFanDialog::~EditAnalogFanDialog()
{
    delete ui;
}

void EditAnalogFanDialog::on_btnAccept_clicked()
{

}


void EditAnalogFanDialog::on_btnCancel_clicked()
{

}

void EditAnalogFanDialog::onTxtClicked()
{
    QLineEdit *txt = dynamic_cast<QLineEdit*>(sender());
    QString prevText = txt->text();

    InputDigitDialog *dlg = new InputDigitDialog(txt, this);
    if(dlg->exec()==QDialog::Rejected)
    {
        txt->setText(prevText);
    }
}

