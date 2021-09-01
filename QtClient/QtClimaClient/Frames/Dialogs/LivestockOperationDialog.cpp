#include "LivestockOperationDialog.h"
#include "ui_LivestockOperationDialog.h"

LivestockOperationDialog::LivestockOperationDialog(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::LivestockOperationDialog)
{
    ui->setupUi(this);
    ui->txtHeads->setText("0");
    ui->dateEdit->setDateTime(QDateTime::currentDateTime());
}

LivestockOperationDialog::~LivestockOperationDialog()
{
    delete ui;
}

void LivestockOperationDialog::setHeads(int heads)
{
    m_heads = heads;
    ui->txtHeads->setText(QString::number(heads));
}

void LivestockOperationDialog::setOperationDate(QDateTime opDate)
{
    m_opDate = opDate;
    ui->dateEdit->setDateTime(m_opDate);
}

void LivestockOperationDialog::setTitle(const QString &title)
{
    ui->lblTitle->setText(title);
    setWindowTitle(title);
}

void LivestockOperationDialog::on_btnAccept_clicked()
{
    m_opDate = ui->dateEdit->dateTime();
    m_heads = ui->txtHeads->text().toInt();

    accept();
}

void LivestockOperationDialog::on_txtHeads_clicked()
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


void LivestockOperationDialog::on_btnReject_clicked()
{
    reject();
}

