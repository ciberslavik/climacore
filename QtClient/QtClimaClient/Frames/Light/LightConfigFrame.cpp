#include "LightConfigFrame.h"
#include "ui_LightConfigFrame.h"

LightConfigFrame::LightConfigFrame(QWidget *parent) :
    QWidget(parent),
    ui(new Ui::LightConfigFrame)
{
    ui->setupUi(this);
}

LightConfigFrame::~LightConfigFrame()
{
    delete ui;
}
