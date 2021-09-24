#include "HystoryMenuFrame.h"
#include "ui_HystoryMenuFrame.h"
#include "Services/FrameManager.h"

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


void HystoryMenuFrame::on_btnReturn_clicked()
{
    FrameManager::instance()->PreviousFrame();
}

