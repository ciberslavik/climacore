#include "VentControllerConfigFrame.h"
#include "ui_VentControllerConfigFrame.h"
#include "Services/FrameManager.h"

VentControllerConfigFrame::VentControllerConfigFrame(QWidget *parent) :
    FrameBase(parent),
    ui(new Ui::VentControllerConfigFrame)
{
    ui->setupUi(this);
}

VentControllerConfigFrame::~VentControllerConfigFrame()
{
    delete ui;
}

void VentControllerConfigFrame::on_btnReturn_clicked()
{
    FrameManager::instance()->PreviousFrame();
}

