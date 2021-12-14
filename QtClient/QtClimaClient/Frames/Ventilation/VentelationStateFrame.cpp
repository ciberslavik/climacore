#include "VentelationStateFrame.h"
#include "ui_VentelationStateFrame.h"

VentelationStateFrame::VentelationStateFrame(QWidget *parent) :
    FrameBase(parent),
    ui(new Ui::VentelationStateFrame)
{
    ui->setupUi(this);
}

VentelationStateFrame::~VentelationStateFrame()
{
    delete ui;
}
