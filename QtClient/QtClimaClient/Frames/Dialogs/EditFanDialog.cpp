#include "EditFanDialog.h"
#include "ui_EditFanDialog.h"

EditFanDialog::EditFanDialog(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::EditFanDialog)
{
    ui->setupUi(this);
}

EditFanDialog::~EditFanDialog()
{
    delete ui;
}

FanInfo *EditFanDialog::getInfo()
{
    return m_fanInfo;
}

void EditFanDialog::setInfo(FanInfo *info)
{
    m_fanInfo = info;
}

void EditFanDialog::on_btnAccept_clicked()
{
    accept();
}


void EditFanDialog::on_btnCancel_clicked()
{
    reject();
}

