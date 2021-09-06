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

    m_infosModel = new FanInfosModel(&m_infos);

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
    FrameManager::instance()->PreviousFrame();
}


void VentilationConfigFrame::on_btnEdit_clicked()
{
    EditFanDialog *dlg = new EditFanDialog(FrameManager::instance()->MainWindow());

    if(dlg->exec() == QDialog::Accepted)
    {
        FanInfo *newFan = dlg->getInfo();

        m_ventService->CreateOrUpdateFan(*newFan);

        delete newFan;
        ui->tableView->update();
    }
}


void VentilationConfigFrame::on_btnAdd_clicked()
{
    EditFanDialog *dlg = new EditFanDialog(FrameManager::instance()->MainWindow());

    if(dlg->exec() == QDialog::Accepted)
    {
        FanInfo *newFan = dlg->getInfo();

        m_ventService->CreateOrUpdateFan(*newFan);

        delete newFan;

        ui->tableView->update();
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
    QModelIndexList indexes = m_selection->selectedIndexes();
    int currentIndex = indexes.at(0).row();
    if(currentIndex == (m_infosModel->rowCount()-1))
        selectRow(currentIndex);
    else
        selectRow(currentIndex + 1);
}


void VentilationConfigFrame::on_btnUp_clicked()
{
    QModelIndexList indexes = m_selection->selectedIndexes();
    int currentIndex = indexes.at(0).row();
    if(currentIndex == 0)
        selectRow(currentIndex);
    else
        selectRow(currentIndex - 1);
}

void VentilationConfigFrame::onFanInfoListReceived(QList<FanInfo> infos)
{
    m_infos = infos;

    ui->tableView->setModel(m_infosModel);

    m_selection = ui->tableView->selectionModel();
}

void VentilationConfigFrame::selectRow(int index)
{
    QModelIndex left;
    QModelIndex right;

    left = m_infosModel->index(index, 0, QModelIndex());
    right = m_infosModel->index(index, m_infosModel->columnCount()-1, QModelIndex());

    QItemSelection selection(left, right);
    m_selection->clear();
    m_selection->select(selection, QItemSelectionModel::Select);
}

void VentilationConfigFrame::closeEvent(QCloseEvent *event)
{

}

void VentilationConfigFrame::showEvent(QShowEvent *event)
{
    m_ventService->GetFanInfoList();
}

