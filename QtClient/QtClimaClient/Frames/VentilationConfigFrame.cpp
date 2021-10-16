#include "Graphs/SelectProfileFrame.h"
#include "VentilationConfigFrame.h"
#include "ui_VentilationConfigFrame.h"

#include <ApplicationWorker.h>

#include <Frames/Dialogs/EditFanDialog.h>

#include <Services/FrameManager.h>
#include <QScrollBar>

VentilationConfigFrame::VentilationConfigFrame(QWidget *parent) :
    FrameBase(parent),
    ui(new Ui::VentilationConfigFrame)
{
    ui->setupUi(this);
    setTitle("Настройка вентиляторов");

    m_infosModel = new FanInfosModel();
    // ui->tableView->setModel(m_infosModel);
    m_selection = ui->tableView->selectionModel();

}

VentilationConfigFrame::~VentilationConfigFrame()
{
    disconnect(m_ventService, &VentilationService::FanInfoListReceived, this, &VentilationConfigFrame::onFanInfoListReceived);
    disconnect(m_ventService, &VentilationService::CreateOrUpdateComplete, this, &VentilationConfigFrame::onCreateOrUpdateComplete);
    delete ui;
}

QString VentilationConfigFrame::getFrameName()
{
    return "VentilatilationConfigFrame";
}

void VentilationConfigFrame::setService(VentilationService *service)
{
    m_ventService = service;
    connect(m_ventService, &VentilationService::FanInfoListReceived, this, &VentilationConfigFrame::onFanInfoListReceived);
    connect(m_ventService, &VentilationService::CreateOrUpdateComplete, this, &VentilationConfigFrame::onCreateOrUpdateComplete);
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
    m_selection = ui->tableView->selectionModel();

    EditFanDialog *dlg = new EditFanDialog(FrameManager::instance()->MainWindow());
    QModelIndexList indexes = m_selection->selectedIndexes();
    if(indexes.count() > 0)
    {
        int index = indexes.at(0).row();

        dlg->setInfo(&m_infos[index]);

        if(dlg->exec() == QDialog::Accepted)
        {

            m_ventService->CreateOrUpdateFan(m_infos[index]);
            qSort(m_infos.begin(),m_infos.end(),
                  [](const FanInfo &a, const FanInfo &b) -> bool { return a.Priority < b.Priority; });

            ui->tableView->update();
        }
    }
    delete dlg;
}


void VentilationConfigFrame::on_btnAdd_clicked()
{
    FanInfo newFan;
    EditFanDialog *dlg = new EditFanDialog(FrameManager::instance()->MainWindow());
    dlg->setInfo(&newFan);

    if(dlg->exec() == QDialog::Accepted)
    {
        ui->tableView->setModel(nullptr);

        m_ventService->CreateOrUpdateFan(newFan);

        m_infos.append(newFan);

        qSort(m_infos.begin(),m_infos.end(),
              [](const FanInfo &a, const FanInfo &b) -> bool { return a.Priority < b.Priority; });

        m_infosModel->setFanInfoList(&m_infos);
        ui->tableView->setModel(m_infosModel);
    }
    delete dlg;
}


void VentilationConfigFrame::on_btnDelete_clicked()
{

}


void VentilationConfigFrame::on_btnCancel_clicked()
{

}


void VentilationConfigFrame::on_btnDown_clicked()
{
    m_selection = ui->tableView->selectionModel();
    QModelIndexList indexes = m_selection->selectedIndexes();
    if(indexes.count() > 0)
    {
        int currentIndex = indexes.at(0).row();
        if(currentIndex == (m_infosModel->rowCount()-1))
            selectRow(currentIndex);
        else
            selectRow(currentIndex + 1);
    }
    else
        selectRow(0);
}


void VentilationConfigFrame::on_btnUp_clicked()
{
    m_selection = ui->tableView->selectionModel();
    QModelIndexList indexes = m_selection->selectedIndexes();
    if(indexes.count() > 0)
    {
        int currentIndex = indexes.at(0).row();
        if(currentIndex == 0)
            selectRow(currentIndex);
        else
            selectRow(currentIndex - 1);
    }
    else
        selectRow(0);
}

void VentilationConfigFrame::onFanInfoListReceived(QList<FanInfo> infos)
{
    m_infos = infos;

    qSort(m_infos.begin(),m_infos.end(),
          [](const FanInfo &a, const FanInfo &b) -> bool { return a.Priority < b.Priority; });

    m_infosModel->setFanInfoList(&m_infos);

    ui->tableView->setModel(m_infosModel);
    ui->tableView->resizeColumnsToContents();
    ui->tableView->resizeRowsToContents();
}

void VentilationConfigFrame::onCreateOrUpdateComplete()
{
    m_ventService->GetFanInfoList();
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
    ui->tableView->verticalScrollBar()->setValue(index);
}

void VentilationConfigFrame::closeEvent(QCloseEvent *event)
{

}

void VentilationConfigFrame::showEvent(QShowEvent *event)
{
    m_ventService->GetFanInfoList();
}

