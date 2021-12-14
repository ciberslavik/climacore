#include "EngineIOOverviewFrame.h"
#include "ui_EngineIOOverviewFrame.h"
#include "ApplicationWorker.h"

#include <Services/FrameManager.h>

EngineIOOverviewFrame::EngineIOOverviewFrame(QWidget *parent) :
    FrameBase(parent),
    ui(new Ui::EngineIOOverviewFrame)
{
    ui->setupUi(this);
    m_model = new IOOverviewModel();
    INetworkService *service = ApplicationWorker::Instance()->GetNetworkService("IONetworkService");
    if(service != nullptr)
    {
        m_io = dynamic_cast<IONetworkService*>(service);
    }

    connect(m_io, &IONetworkService::onPinInfosReceived, this, &EngineIOOverviewFrame::onPinInfosReceived);
    m_io->getPinInfos();
}

EngineIOOverviewFrame::~EngineIOOverviewFrame()
{
    disconnect(m_io, &IONetworkService::onPinInfosReceived, this, &EngineIOOverviewFrame::onPinInfosReceived);
    delete ui;
    delete m_model;
}

void EngineIOOverviewFrame::onPinInfosReceived(const QList<PinInfo> &infos)
{
    m_model->setPinInfos(infos);

    ui->tableView->setModel(m_model);
}


QString EngineIOOverviewFrame::getFrameName()
{
    return "EngineIOOverviewFrame";
}

void EngineIOOverviewFrame::on_btnReturn_clicked()
{
    FrameManager::instance()->PreviousFrame();
}

