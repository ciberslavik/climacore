#include "Graphs/SelectProfileFrame.h"
#include "VentilationConfigFrame.h"
#include "ui_VentilationConfigFrame.h"

#include <ApplicationWorker.h>

VentilationConfigFrame::VentilationConfigFrame(QWidget *parent) :
    FrameBase(parent),
    ui(new Ui::VentilationConfigFrame)
{
    ui->setupUi(this);
    setTitle("Настройка вентиляторов");
    INetworkService *service = ApplicationWorker::Instance()->GetNetworkService("VentilationService");
    if(service != nullptr)
    {
        m_ventService = dynamic_cast<VentilationService*>(service);
    }

}

VentilationConfigFrame::~VentilationConfigFrame()
{
    delete ui;
}

QString VentilationConfigFrame::getFrameName()
{
    return "VentilatilationConfigFrame";
}

void VentilationConfigFrame::on_btnSelectGraph_clicked()
{
   // SelectGraphFrame *selectFrame = new SelectGraphFrame();
    //FrameManager::instance()->setCurrentFrame(selectFrame);
}


void VentilationConfigFrame::on_btnReturn_clicked()
{

}


void VentilationConfigFrame::on_btnEdit_clicked()
{

}


void VentilationConfigFrame::on_btnAdd_clicked()
{

}


void VentilationConfigFrame::on_btnDelete_clicked()
{

}


void VentilationConfigFrame::on_btnCancel_clicked()
{

}


void VentilationConfigFrame::on_btnDown_clicked()
{

}


void VentilationConfigFrame::on_btnUp_clicked()
{

}

void VentilationConfigFrame::onFanInfoListReceived(QList<FanInfo> infos)
{

}

void VentilationConfigFrame::showEvent(QShowEvent *event)
{
    m_ventService->GetFanStateList();
}

