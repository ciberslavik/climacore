#include "MainMenuFrame.h"
#include "ui_MainMenuFrame.h"

MainMenuFrame::MainMenuFrame(QWidget *parent) :
    FrameBase(parent),
    ui(new Ui::MainMenuFrame)
{
    ui->setupUi(this);
}

MainMenuFrame::~MainMenuFrame()
{
    delete ui;
}
