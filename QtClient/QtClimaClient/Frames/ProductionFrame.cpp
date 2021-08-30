#include "ProductionFrame.h"
#include "ui_ProductionFrame.h"

ProductionFrame::ProductionFrame(QWidget *parent) :
    QWidget(parent),
    ui(new Ui::ProductionFrame)
{
    ui->setupUi(this);
}

ProductionFrame::~ProductionFrame()
{
    delete ui;
}
