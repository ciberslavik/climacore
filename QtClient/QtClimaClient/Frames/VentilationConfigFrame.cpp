#include "Graphs/SelectProfileFrame.h"
#include "VentilationConfigFrame.h"
#include "ui_VentilationConfigFrame.h"

#include <ApplicationWorker.h>
#include <TimerPool.h>

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
    ui->tableView->setModel(m_infosModel);
    m_selection = ui->tableView->selectionModel();

    INetworkService *service = ApplicationWorker::Instance()->GetNetworkService("VentilationControllerService");
    if(service != nullptr)
    {
        m_ventService = dynamic_cast<VentilationService*>(service);
    }

    connect(m_ventService, &VentilationService::FanInfoListReceived, this, &VentilationConfigFrame::onFanInfoListReceived);

    m_currentState = EditorState::Initialize;
    m_running = false;

}

VentilationConfigFrame::~VentilationConfigFrame()
{
    disconnect(m_ventService, &VentilationService::FanInfoListReceived, this, &VentilationConfigFrame::onFanInfoListReceived);

    delete ui;
}

QString VentilationConfigFrame::getFrameName()
{
    return "VentilatilationConfigFrame";
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
        int prevPrio = m_infos[m_infosModel->getRowKey(index)].Priority;

        dlg->setInfo(m_infos[m_infosModel->getRowKey(index)]);

        if(dlg->exec() == QDialog::Accepted)
        {
            FanInfo editedInfo = dlg->getInfo();

            //m_infos[editedInfo.Key] = editedInfo;

            foreach (const FanInfo &fi, m_infos.values())
            {
                if(fi.Priority == editedInfo.Priority)
                {
                    m_infos[fi.Key].Priority = prevPrio;

                    break;
                }
            }
            m_infos[editedInfo.Key] = editedInfo;
            m_dataChanged = true;
            m_infosModel->updateFanInfoList(m_infos.values());
            //ui->tableView->update();
        }
    }
    delete dlg;
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

void VentilationConfigFrame::onFanInfoListReceived(QMap<QString, FanInfo> infos)
{
    if(!m_running)
    {
        m_infos = QMap<QString, FanInfo>(infos);

        m_infosModel->setFanInfoList(m_infos.values());

        m_dataChanged = false;

        ui->tableView->resizeColumnsToContents();
        ui->tableView->resizeRowsToContents();
        m_running = true;
        connect(TimerPool::instance()->getUpdateTimer(), &QTimer::timeout, this, &VentilationConfigFrame::onTimeout);
        TimerPool::instance()->getUpdateTimer()->setInterval(2000);
    }
    else
    {
        if(m_infos.count() == infos.count())
        {
            int totalPerf = 0;
            foreach (const FanInfo &info, infos.values())
            {
                FanInfo tmp = m_infos[info.Key];
                tmp.State = info.State;
                tmp.Mode = info.Mode;
                if(tmp.IsAnalog)
                {
                    tmp.AnalogPower = info.AnalogPower;
                }
                if(!tmp.Hermetised)
                    totalPerf += tmp.Performance * tmp.FanCount;

                m_infos[info.Key] = tmp;
            }
            m_infosModel->updateFanInfoList(m_infos.values());
            ui->lblTotalPerf->setText(QString::number(totalPerf));
        }
    }
}

void VentilationConfigFrame::selectRow(int index)
{
    QModelIndex left;
    QModelIndex right;

    left = m_infosModel->index(index, 1, QModelIndex());
    right = m_infosModel->index(index, m_infosModel->columnCount()-1, QModelIndex());

    QItemSelection selection(left, right);
    m_selection->clear();
    m_selection->select(selection, QItemSelectionModel::Select);
    ui->tableView->verticalScrollBar()->setValue(index);
}

void VentilationConfigFrame::updateStatus(const QList<FanInfo> &infos)
{

}

void VentilationConfigFrame::updateInfo(const QList<FanInfo> &infos)
{

}

void VentilationConfigFrame::closeEvent(QCloseEvent *event)
{
    disconnect(TimerPool::instance()->getUpdateTimer(), &QTimer::timeout, this, &VentilationConfigFrame::onTimeout);
    Q_UNUSED(event)
}

void VentilationConfigFrame::showEvent(QShowEvent *event)
{
    Q_UNUSED(event)
    m_ventService->GetFanInfoList();
}


void VentilationConfigFrame::on_btnAccept_clicked()
{
    m_ventService->UpdateFanInfoList(m_infos);
}

void VentilationConfigFrame::onTimeout()
{
    if(m_running)
    {
        m_ventService->GetFanInfoList();
    }
}

