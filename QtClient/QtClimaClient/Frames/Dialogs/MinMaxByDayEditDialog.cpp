#include "MinMaxByDayEditDialog.h"
#include "ui_MinMaxByDayEditDialog.h"

MinMaxByDayEditDialog::MinMaxByDayEditDialog(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::MinMaxByDayEditDialog)
{
    ui->setupUi(this);
    connect(ui->txtDayEdit, &QClickableLineEdit::clicked, this, &MinMaxByDayEditDialog::on_txtEdit_clicked);
    connect(ui->txtMinValueEdit, &QClickableLineEdit::clicked, this, &MinMaxByDayEditDialog::on_txtEdit_clicked);
    connect(ui->txtMaxValueEdit, &QClickableLineEdit::clicked, this, &MinMaxByDayEditDialog::on_txtEdit_clicked);
}

MinMaxByDayEditDialog::~MinMaxByDayEditDialog()
{
    delete ui;
}

void MinMaxByDayEditDialog::setValue(MinMaxByDayPoint *value)
{
    m_value = value;

    ui->txtDayEdit->setText(QString::number(m_value->Day));
    ui->txtMaxValueEdit->setText(QString::number(m_value->MaxValue));
    ui->txtMinValueEdit->setText(QString::number(m_value->MinValue));
}

MinMaxByDayPoint *MinMaxByDayEditDialog::getValue()
{
    return m_value;
}

void MinMaxByDayEditDialog::setTitle(const QString &title)
{
    ui->lblTitle->setText(title);
}

void MinMaxByDayEditDialog::setMaxValueName(const QString &maxValueName)
{
    ui->lblMaxValueName->setText(maxValueName);
}

void MinMaxByDayEditDialog::setMinValueName(const QString &minValueName)
{
    ui->lblMinValueName->setText(minValueName);
}

void MinMaxByDayEditDialog::on_btnAccept_clicked()
{
    m_value->Day = ui->txtDayEdit->text().toInt();
    m_value->MaxValue = ui->txtMaxValueEdit->text().toFloat();
    m_value->MinValue = ui->txtMinValueEdit->text().toFloat();

    accept();
}

void MinMaxByDayEditDialog::on_txtEdit_clicked()
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


void MinMaxByDayEditDialog::on_btnCancel_clicked()
{
    reject();
}

