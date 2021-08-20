#include "SelectGraphFrame.h"
#include "ui_SelectGraphFrame.h"

#include <ApplicationWorker.h>

SelectGraphFrame::SelectGraphFrame(QString graphType, QWidget *parent) :
    FrameBase(parent),
    ui(new Ui::SelectGraphFrame)
{
    ui->setupUi(this);
    setTitle("Выбор графика");
    INetworkService *service;
    service = ApplicationWorker::Instance()->GetNetworkService("GraphService");
    GraphService *graphService = dynamic_cast<GraphService*>(service);


    if(graphType == "Temperature")
    {
        connect(graphService, &GraphService::TempInfosResponse, this, &SelectGraphFrame::TempInfosResponse);
        graphService->TempInfosRequest();
    }

    m_graphInfosModel = new SelectGraphModel();

    ui->listView->setModel(m_graphInfosModel);
}

SelectGraphFrame::~SelectGraphFrame()
{
    delete ui;
}

QString SelectGraphFrame::getFrameName()
{
    return "SelectGraphFrame";
}

void SelectGraphFrame::TempInfosResponse(QList<GraphInfo> *infos)
{

}
