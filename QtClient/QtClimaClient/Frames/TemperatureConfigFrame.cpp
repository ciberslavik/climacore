#include "TemperatureConfigFrame.h"
#include "ui_TemperatureConfigFrame.h"

#include <Services/FrameManager.h>

TemperatureConfigFrame::TemperatureConfigFrame(QWidget *parent) :
    FrameBase(parent),
    ui(new Ui::TemperatureConfigFrame)
{
    ui->setupUi(this);
}

TemperatureConfigFrame::~TemperatureConfigFrame()
{
    delete ui;
}

void TemperatureConfigFrame::on_btnReturn_clicked()
{
    FrameManager::instance()->PreviousFrame();
}

QString TemperatureConfigFrame::getFrameName()
{
    return "TemperatureConfigFrame";
}


void TemperatureConfigFrame::on_btnSelectGraph_clicked()
{

}

