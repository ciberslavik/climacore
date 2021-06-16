#include "SystemStateFrame.h"
#include "ui_SystemStateFrame.h"

SystemStateFrame::SystemStateFrame(QWidget *parent) :
    FrameBase(parent),
    ui(new Ui::SystemStateFrame)
{
    ui->setupUi(this);
}

SystemStateFrame::~SystemStateFrame()
{
    delete ui;
}
