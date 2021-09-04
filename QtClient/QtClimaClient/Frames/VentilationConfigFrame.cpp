#include "Graphs/SelectProfileFrame.h"
#include "VentilationConfigFrame.h"
#include "ui_VentilationConfigFrame.h"

#include <ApplicationWorker.h>

#include <Frames/Dialogs/EditFanDialog.h>

#include <Services/FrameManager.h>

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
    EditFanDialog *dlg = new EditFanDialog(FrameManager::instance()->MainWindow());

    if(dlg->exec() == QDialog::Accepted)
    {

    }
}


void VentilationConfigFrame::on_btnAdd_clicked()
{
    EditFanDialog *dlg = new EditFanDialog(FrameManager::instance()->MainWindow());

    if(dlg->exec() == QDialog::Accepted)
    {
        FanInfo *newFan = new FanInfo();

        //newFan->FanName = dlg->get
    }
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

void VentilationConfigFrame::closeEvent(QCloseEvent *event)
{

}

void VentilationConfigFrame::showEvent(QShowEvent *event)
{
    m_ventService->GetFanInfoList();
}

