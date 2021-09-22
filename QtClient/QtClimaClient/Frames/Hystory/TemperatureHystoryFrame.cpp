#include "TemperatureHystoryFrame.h"
#include "ui_TemperatureHystoryFrame.h"

TemperatureHystoryFrame::TemperatureHystoryFrame(QWidget *parent) :
    QWidget(parent),
    ui(new Ui::TemperatureHystoryFrame)
{
    ui->setupUi(this);
}

TemperatureHystoryFrame::~TemperatureHystoryFrame()
{
    delete ui;
}
