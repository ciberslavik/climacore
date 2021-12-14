#include "VentilationMenuFrame.h"
#include "ui_VentilationMenuFrame.h"

VentilationMenuFrame::VentilationMenuFrame(QWidget *parent) :
    QWidget(parent),
    ui(new Ui::VentilationMenuFrame)
{
    ui->setupUi(this);
}

VentilationMenuFrame::~VentilationMenuFrame()
{
    delete ui;
}
