#include "AlarmOwerviewFrame.h"
#include "ui_AlarmOwerviewFrame.h"

#include <Services/FrameManager.h>

AlarmOwerviewFrame::AlarmOwerviewFrame(QWidget *parent) :
    FrameBase(parent),
    ui(new Ui::AlarmOwerviewFrame)
{
    ui->setupUi(this);
}

AlarmOwerviewFrame::~AlarmOwerviewFrame()
{
    delete ui;
}


QString AlarmOwerviewFrame::getFrameName()
{
    return "AlarmOwerviewFrame";
}

void AlarmOwerviewFrame::on_btnShowAlarmConfig_clicked()
{

}


void AlarmOwerviewFrame::on_btnReturn_clicked()
{
    FrameManager::instance()->PreviousFrame();

}

