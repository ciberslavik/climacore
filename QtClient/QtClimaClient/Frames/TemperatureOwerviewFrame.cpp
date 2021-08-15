#include "TemperatureOwerviewFrame.h"
#include "ui_TemperatureOwerviewFrame.h"



TemperatureOwerviewFrame::TemperatureOwerviewFrame(QWidget *parent) :
    FrameBase(parent),
    ui(new Ui::TemperatureOwerviewFrame)
{
    ui->setupUi(this);
    setTitle("Обзор температуры");

    INetworkService *sensorsService = ApplicationWorker::Instance()->GetNetworkService("SensorsService");
    INetworkService *graphService = ApplicationWorker::Instance()->GetNetworkService("GraphProviderService");

    if(sensorsService != nullptr)
    {
        m_sensors = dynamic_cast<SensorsService*>(sensorsService);
    }
    if(graphService != nullptr)
    {
        m_graphService = dynamic_cast<GraphService*>(graphService);
    }

    m_graphService->TempInfosRequest();
}

TemperatureOwerviewFrame::~TemperatureOwerviewFrame()
{
    delete ui;
}

void TemperatureOwerviewFrame::on_btnConfigTemp_clicked()
{

}

QString TemperatureOwerviewFrame::getFrameName()
{

}

