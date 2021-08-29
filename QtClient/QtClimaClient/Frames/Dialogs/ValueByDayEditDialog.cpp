#include "ValueByDayEditDialog.h"
#include "ui_ValueByDayEditDialog.h"

ValueByDayEditDialog::ValueByDayEditDialog(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::ValueByDayEditDialog)
{
    ui->setupUi(this);
    connect(ui->txtDayEdit, &QClickableLineEdit::clicked, this, &ValueByDayEditDialog::on_txtEdit_clicked);
    connect(ui->txtValueEdit, &QClickableLineEdit::clicked, this, &ValueByDayEditDialog::on_txtEdit_clicked);
}

ValueByDayEditDialog::~ValueByDayEditDialog()
{
    delete ui;
}

void ValueByDayEditDialog::setValue(ValueByDayPoint *value)
{
    m_value = value;

    ui->txtDayEdit->setText(QString::number(m_value->Day));
    ui->txtValueEdit->setText(QString::number(m_value->Value));
}

void ValueByDayEditDialog::setTitle(const QString &title)
{
    ui->lblTitle->setText(title);
}

void ValueByDayEditDialog::setValueName(const QString &valueName)
{
    ui->lblValueName->setText(valueName);
}

void ValueByDayEditDialog::on_btnAccept_clicked()
{
    m_value->Day = ui->txtDayEdit->text().toInt();
    m_value->Value = ui->txtValueEdit->text().toFloat();

    accept();
}


void ValueByDayEditDialog::on_txtEdit_clicked()
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


void ValueByDayEditDialog::on_btnCancel_clicked()
{
    reject();
}

