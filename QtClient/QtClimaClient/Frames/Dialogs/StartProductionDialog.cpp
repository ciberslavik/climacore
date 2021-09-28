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
    connect(ui->txtStartDay, &QClickableLineEdit::clicked, this, &StartProductionDialog::onTxtClicked);
    connect(ui->txtStartMonth, &QClickableLineEdit::clicked, this, &StartProductionDialog::onTxtClicked);
    connect(ui->txtStartYear, &QClickableLineEdit::clicked, this, &StartProductionDialog::onTxtClicked);

    connect(ui->txtPlendingDay, &QClickableLineEdit::clicked, this, &StartProductionDialog::onTxtClicked);
    connect(ui->txtPlendingMonth, &QClickableLineEdit::clicked, this, &StartProductionDialog::onTxtClicked);
    connect(ui->txtPlendingYear, &QClickableLineEdit::clicked, this, &StartProductionDialog::onTxtClicked);

    QDateTime currDate = QDateTime::currentDateTime();

    ui->txtStartDay->setText(currDate.toString("dd"));
    ui->txtStartMonth->setText(currDate.toString("MM"));
    ui->txtStartYear->setText(currDate.toString("yyyy"));

    ui->txtPlendingDay->setText(currDate.toString("dd"));
    ui->txtPlendingMonth->setText(currDate.toString("MM"));
    ui->txtPlendingYear->setText(currDate.toString("yyyy"));
}

StartProductionDialog::~StartProductionDialog()
{
    delete ui;
}

QDateTime StartProductionDialog::getStartDate()
{
    QString dateStr = ui->txtStartDay->text() + "." + ui->txtStartMonth->text() + "." + ui->txtStartYear->text();
    QDateTime t = QDateTime::fromString(dateStr,"dd.MM.yyyy");

    return t;
}

QDateTime StartProductionDialog::getPlendingDate()
{
    QString dateStr = ui->txtPlendingDay->text() + "." + ui->txtPlendingMonth->text() + "." + ui->txtPlendingYear->text();
    QDateTime t = QDateTime::fromString(dateStr,"dd.MM.yyyy");

    return t;
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

