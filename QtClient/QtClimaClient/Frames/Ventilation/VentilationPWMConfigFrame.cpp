#include "VentilationPWMConfigFrame.h"
#include "ui_VentilationPWMConfigFrame.h"

VentilationPWMConfigFrame::VentilationPWMConfigFrame(QWidget *parent) :
    FrameBase(parent),
    ui(new Ui::VentilationPWMConfigFrame)
{
    ui->setupUi(this);
}

VentilationPWMConfigFrame::~VentilationPWMConfigFrame()
{
    delete ui;
}


QString VentilationPWMConfigFrame::getFrameName()
{
    return "VentilationPWMConfigFrame";
}
