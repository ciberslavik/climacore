#include "SelectGraphFrame.h"
#include "ui_SelectGraphFrame.h"

SelectGraphFrame::SelectGraphFrame(QWidget *parent) :
    FrameBase(parent),
    ui(new Ui::SelectGraphFrame)
{
    ui->setupUi(this);
    setTitle("Выбор графика");
}

SelectGraphFrame::~SelectGraphFrame()
{
    delete ui;
}

QString SelectGraphFrame::getFrameName()
{
    return "SelectGraphFrame";
}
