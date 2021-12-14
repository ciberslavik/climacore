#include "EngineIOOverviewFrame.h"
#include "EngineMenuFrame.h"
#include "ui_EngineMenuFrame.h"

#include <Services/FrameManager.h>

EngineMenuFrame::EngineMenuFrame(QWidget *parent) :
    FrameBase(parent),
    ui(new Ui::EngineMenuFrame)
{
    ui->setupUi(this);
}

EngineMenuFrame::~EngineMenuFrame()
{
    delete ui;
}


QString EngineMenuFrame::getFrameName()
{
    return "EngineMenuFrame";
}

void EngineMenuFrame::on_btnIOOverview_clicked()
{
    EngineIOOverviewFrame *frame = new EngineIOOverviewFrame(FrameManager::instance()->MainWindow());

    FrameManager::instance()->setCurrentFrame(frame);
}


void EngineMenuFrame::on_btnReturn_clicked()
{
    FrameManager::instance()->PreviousFrame();
}

