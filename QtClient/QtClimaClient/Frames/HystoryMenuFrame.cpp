#include "HystoryMenuFrame.h"
#include "ui_HystoryMenuFrame.h"

HystoryMenuFrame::HystoryMenuFrame(QWidget *parent) :
    FrameBase(parent),
    ui(new Ui::HystoryMenuFrame)
{
    ui->setupUi(this);
}

HystoryMenuFrame::~HystoryMenuFrame()
{
    delete ui;
}
