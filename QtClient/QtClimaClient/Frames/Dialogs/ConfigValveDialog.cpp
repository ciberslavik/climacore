#include "ConfigValveDialog.h"
#include "ui_ConfigValveDialog.h"

ConfigValveDialog::ConfigValveDialog(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::ConfigValveDialog)
{
    ui->setupUi(this);
}

ConfigValveDialog::~ConfigValveDialog()
{
    delete ui;
}
