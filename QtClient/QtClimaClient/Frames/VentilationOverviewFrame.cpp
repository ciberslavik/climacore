#include "VentilationOverviewFrame.h"
#include "ui_VentilationOverviewFrame.h"

VentilationOverviewFrame::VentilationOverviewFrame(QWidget *parent) :
    QWidget(parent),
    ui(new Ui::VentilationOverviewFrame)
{
    ui->setupUi(this);
}

VentilationOverviewFrame::~VentilationOverviewFrame()
{
    delete ui;
}
