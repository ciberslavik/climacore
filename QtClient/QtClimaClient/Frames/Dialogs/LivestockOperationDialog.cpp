#include "LivestockOperationDialog.h"
#include "ui_LivestockOperationDialog.h"

LivestockOperationDialog::LivestockOperationDialog(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::LivestockOperationDialog)
{
    ui->setupUi(this);
    ui->txtHeads->setText("0");

    QDateTime opDate = QDateTime::currentDateTime();

    ui->txtDay->setText(opDate.toString("dd"));
    ui->txtMonth->setText(opDate.toString("MM"));
    ui->txtYear->setText(opDate.toString("yyyy"));
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
    ui->txtDay->setText(opDate.toString("dd"));
    ui->txtMonth->setText(opDate.toString("MM"));
    ui->txtYear->setText(opDate.toString("yyyy"));
}

void LivestockOperationDialog::setTitle(const QString &title)
{
    ui->lblTitle->setText(title);
    setWindowTitle(title);
}

void LivestockOperationDialog::on_btnAccept_clicked()
{
    QString dateStr = ui->txtDay->text() + "." + ui->txtMonth->text() + "." + ui->txtYear->text();


    m_opDate = QDateTime(
                   QDate(
                       ui->txtYear->text().toInt(),
                       ui->txtMonth->text().toInt(),
                       ui->txtDay->text().toInt()),
                   QTime::currentTime());

    m_heads = ui->txtHeads->text().toInt();

    accept();
}

void LivestockOperationDialog::on_txtHeads_clicked()
{
    QLineEdit *lineEdit = dynamic_cast<QLineEdit*>(sender());

    showDigitDialog(lineEdit, "Поголовъе");
}

void LivestockOperationDialog::on_txtDay_clicked()
{
    QLineEdit *lineEdit = dynamic_cast<QLineEdit*>(sender());

    showDigitDialog(lineEdit, "День");
}

void LivestockOperationDialog::on_txtMonth_clicked()
{
    QLineEdit *lineEdit = dynamic_cast<QLineEdit*>(sender());

    showDigitDialog(lineEdit, "Месяц");
}

void LivestockOperationDialog::on_txtYear_clicked()
{
    QLineEdit *lineEdit = dynamic_cast<QLineEdit*>(sender());

    showDigitDialog(lineEdit, "Год");
}



void LivestockOperationDialog::on_btnReject_clicked()
{
    reject();
}

void LivestockOperationDialog::showDigitDialog(QLineEdit *txt, const QString &title)
{

    QString prevValue = txt->text();
    InputDigitDialog *dlg = new InputDigitDialog(txt, FrameManager::instance()->MainWindow());
    dlg->setTitle(title);
    if(dlg->exec()==QDialog::Rejected)
    {
        txt->setText(prevValue);
    }

    dlg->deleteLater();
}

