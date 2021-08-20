#include "LightConfigFrame.h"
#include "ui_LightConfigFrame.h"
#include <Services/FrameManager.h>
LightConfigFrame::LightConfigFrame(QWidget *parent) :
    FrameBase(parent),
    ui(new Ui::LightConfigFrame)
{
    ui->setupUi(this);
    setTitle("Таймер освещения");
}

LightConfigFrame::~LightConfigFrame()
{
    qDebug() << "LightConfigFrame deleted";
    delete ui;
}

QString LightConfigFrame::getFrameName()
{
    return "LightConfigFrame";
}

void LightConfigFrame::on_btnReturn_clicked()
{
    FrameManager::instance()->PreviousFrame();
}

