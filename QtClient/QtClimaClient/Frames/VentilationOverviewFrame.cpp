#include "VentilationOverviewFrame.h"
#include "ui_VentilationOverviewFrame.h"
#include <Services/FrameManager.h>

VentilationOverviewFrame::VentilationOverviewFrame(QWidget *parent) :
    FrameBase(parent),
    ui(new Ui::VentilationOverviewFrame)
{
    ui->setupUi(this);
}

VentilationOverviewFrame::~VentilationOverviewFrame()
{
    delete ui;
}


void VentilationOverviewFrame::closeEvent(QCloseEvent *event)
{
}

void VentilationOverviewFrame::showEvent(QShowEvent *event)
{
}


void VentilationOverviewFrame::on_pushButton_clicked()
{
    FrameManager::instance()->PreviousFrame();
}

