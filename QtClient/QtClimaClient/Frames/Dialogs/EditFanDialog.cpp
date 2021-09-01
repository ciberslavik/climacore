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
