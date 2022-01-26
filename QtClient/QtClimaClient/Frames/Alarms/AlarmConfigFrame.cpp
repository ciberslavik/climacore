#include "AlarmConfigFrame.h"
#include "ui_AlarmConfigFrame.h"

AlarmConfigFrame::AlarmConfigFrame(QWidget *parent) :
    QWidget(parent),
    ui(new Ui::AlarmConfigFrame)
{
    ui->setupUi(this);
}

AlarmConfigFrame::~AlarmConfigFrame()
{
    delete ui;
}
