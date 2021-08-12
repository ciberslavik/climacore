#include "MainMenuFrame.h"
#include "VentilationConfigFrame.h"
#include "ui_MainMenuFrame.h"

#include <Services/FrameManager.h>

MainMenuFrame::MainMenuFrame(QWidget *parent) :
    FrameBase(parent),
    ui(new Ui::MainMenuFrame)
{
    ui->setupUi(this);
    setTitle("Главное меню");
}

MainMenuFrame::~MainMenuFrame()
{
    delete ui;
}

void MainMenuFrame::on_btnVentilationOverview_clicked()
{

}


void MainMenuFrame::on_btnVentilationConfig_clicked()
{
    VentilationConfigFrame *configFrame = new VentilationConfigFrame();

    FrameManager::instance()->setCurrentFrame(configFrame);
}


void MainMenuFrame::on_btnReturn_clicked()
{
    FrameManager::instance()->PreviousFrame();
}

