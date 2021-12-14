#include "LivestockOperationsFrame.h"
#include "ui_LivestockOperationsFrame.h"

#include <Services/FrameManager.h>

#include <ApplicationWorker.h>

LivestockOperationsFrame::LivestockOperationsFrame(QWidget *parent) :
    FrameBase(parent),
    ui(new Ui::LivestockOperationsFrame)
{
    ui->setupUi(this);

    INetworkService *service = ApplicationWorker::Instance()->GetNetworkService("LivestockService");
    if(service!=nullptr)
    {
        m_liveService = dynamic_cast<LivestockService*>(service);
    }
    connect(m_liveService, &LivestockService::OpListReceived,
            this, &LivestockOperationsFrame::LivestockOpListReceived);

    m_model = new LivestockOperationsModel();



}

LivestockOperationsFrame::~LivestockOperationsFrame()
{
    delete ui;
}


QString LivestockOperationsFrame::getFrameName()
{
    return "LivestockOperationsFrame";
}

void LivestockOperationsFrame::on_btnReturn_clicked()
{
    FrameManager::instance()->PreviousFrame();
}

void LivestockOperationsFrame::LivestockOpListReceived(const QList<LivestockOperation> &operations)
{
    m_model->setLivestockOperations(operations);

    ui->opTable->setModel(m_model);
    ui->opTable->resizeColumnsToContents();
}



void LivestockOperationsFrame::showEvent(QShowEvent *event)
{
    m_liveService->GetOpList();
    //ui->opTable->setModel(m_model);
}
