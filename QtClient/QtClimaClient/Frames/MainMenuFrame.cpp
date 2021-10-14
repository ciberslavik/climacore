#include "LightConfigFrame.h"
#include "MainMenuFrame.h"
#include "ProductionFrame.h"
#include "TemperatureOwerviewFrame.h"
#include "VentilationConfigFrame.h"
#include "VentilationOverviewFrame.h"
#include "HystoryMenuFrame.h"
#include "HeaterControllerFrame.h"

#include "ui_MainMenuFrame.h"


#include <Services/FrameManager.h>

#include <Frames/Graphs/ConfigProfilesFrame.h>

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
    ProductionFrame *prodFrame = new ProductionFrame();

    FrameManager::instance()->setCurrentFrame(prodFrame);
}

void MainMenuFrame::on_btnEditProfiles_clicked()
{
    ConfigProfilesFrame *pofFrame = new ConfigProfilesFrame();

    FrameManager::instance()->setCurrentFrame(pofFrame);
}


void MainMenuFrame::on_btnShowHystoryMenu_clicked()
{
    HystoryMenuFrame *hystoryMenu = new HystoryMenuFrame();

    FrameManager::instance()->setCurrentFrame(hystoryMenu);
}

void MainMenuFrame::on_btnHeaterConfig_clicked()
{
    HeaterControllerFrame *heaterFrame = new HeaterControllerFrame();

    FrameManager::instance()->setCurrentFrame(heaterFrame);
}

