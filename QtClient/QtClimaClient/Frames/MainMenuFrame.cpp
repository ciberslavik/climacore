#include "LightConfigFrame.h"
#include "MainMenuFrame.h"
#include "TemperatureOwerviewFrame.h"
#include "VentilationConfigFrame.h"
#include "VentilationOverviewFrame.h"
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
    qDebug()<<"MainMenuFrame deleted";
    delete ui;
}

void MainMenuFrame::on_btnVentilationOverview_clicked()
{
    VentilationOverviewFrame *ventFrame = new VentilationOverviewFrame();

    FrameManager::instance()->setCurrentFrame(ventFrame);
}


//void MainMenuFrame::on_btnVentilationConfig_clicked()
//{
//    //VentilationConfigFrame *configFrame = new VentilationConfigFrame();

//    //FrameManager::instance()->setCurrentFrame(configFrame);
//}


void MainMenuFrame::on_btnReturn_clicked()
{
    FrameManager::instance()->PreviousFrame();
}


void MainMenuFrame::on_btnTemperature_clicked()
{
     TemperatureOwerviewFrame *tempFrame = new TemperatureOwerviewFrame();

     FrameManager::instance()->setCurrentFrame(tempFrame);
}



void MainMenuFrame::on_btnLightConfig_clicked()
{
    LightConfigFrame *lightFrame = new LightConfigFrame();
    FrameManager::instance()->setCurrentFrame(lightFrame);
}


void MainMenuFrame::on_btnProduction_clicked()
{

}
