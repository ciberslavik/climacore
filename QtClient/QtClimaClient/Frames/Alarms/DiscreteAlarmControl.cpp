#include "DiscreteAlarmControl.h"
#include "ui_DiscreteAlarmControl.h"

DiscreteAlarmControl::DiscreteAlarmControl(QWidget *parent) :
    QWidget(parent),
    ui(new Ui::DiscreteAlarmControl)
{
    ui->setupUi(this);
}

DiscreteAlarmControl::~DiscreteAlarmControl()
{
    delete ui;
}
