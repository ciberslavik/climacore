#include "LineEditorDialog.h"
#include "ui_LineEditorDialog.h"

LineEditorDialog::LineEditorDialog(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::LineEditorDialog)
{
    ui->setupUi(this);
}

LineEditorDialog::~LineEditorDialog()
{
    delete ui;
}

void LineEditorDialog::setTitle(const QString &title)
{
    ui->lblTitle->setText(title);
}

void LineEditorDialog::setValueName(const QString &valueName)
{
    ui->lblValueName->setText(valueName);
}

void LineEditorDialog::setValue(const QString &value)
{
    ui->txtValue->setText(value);
}

void LineEditorDialog::setValidator(QValidator *validator)
{
    ui->txtValue->setValidator(validator);
}

QString LineEditorDialog::value() const
{
    return ui->txtValue->text();
}

void LineEditorDialog::on_btnAccept_clicked()
{
    accept();
}


void LineEditorDialog::on_btnReject_clicked()
{
    reject();
}

