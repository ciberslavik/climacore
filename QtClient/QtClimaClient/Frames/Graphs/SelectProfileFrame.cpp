#include "SelectProfileFrame.h"
#include "ui_SelectProfileFrame.h"

#include <ApplicationWorker.h>
#include <QDateTime>

#include <Frames/Dialogs/inputtextdialog.h>

SelectProfileFrame::SelectProfileFrame(QList<ProfileInfo> *infos, GraphType type, QWidget *parent) :
    FrameBase(parent),
    ui(new Ui::SelectProfileFrame)
{
    m_graphType = type;
    ui->setupUi(this);
    setTitle("Выбор графика");

    m_infoModel = new ProfileInfoModel(infos, this);

    ui->profilesTable->setModel(m_infoModel);

    ui->profilesTable->resizeColumnsToContents();
    ui->profilesTable->resizeRowsToContents();

    m_selection = ui->profilesTable->selectionModel();
    selectRow(0);

    INetworkService *service = ApplicationWorker::Instance()->GetNetworkService("GraphProviderService");
    if(service !=nullptr)
    {
        m_graphService = dynamic_cast<GraphService*>(service);
    }
}

SelectProfileFrame::~SelectProfileFrame()
{
    delete ui;
}

QString SelectProfileFrame::getFrameName()
{
    return "SelectProfileFrame";
}



void SelectProfileFrame::on_btnReturn_clicked()
{
    FrameManager::instance()->PreviousFrame();
}

void SelectProfileFrame::selectRow(int row)
{
    QModelIndex left;
    QModelIndex right;

    left = m_infoModel->index(row, 0, QModelIndex());
    right = m_infoModel->index(row, m_infoModel->columnCount()-1, QModelIndex());

    QItemSelection selection(left, right);
    m_selection->clear();
    m_selection->select(selection, QItemSelectionModel::Select);
}

void SelectProfileFrame::loadGraph(const QString &key)
{

}


void SelectProfileFrame::on_btnUp_clicked()
{
    QModelIndexList indexes = m_selection->selectedIndexes();
    int currentIndex = indexes.at(0).row();
    if(currentIndex == 0)
        selectRow(currentIndex);
    else
        selectRow(currentIndex - 1);
}


void SelectProfileFrame::on_btnDown_clicked()
{
    QModelIndexList indexes = m_selection->selectedIndexes();
    int currentIndex = indexes.at(0).row();
    if(currentIndex == (m_infoModel->rowCount()-1))
        selectRow(currentIndex);
    else
        selectRow(currentIndex + 1);
}


void SelectProfileFrame::on_btnAdd_clicked()
{
    InputTextDialog *inputDialog = new InputTextDialog();
    if(inputDialog->exec() == QDialog::Accepted)
    {
        QString name = inputDialog->getText();
        qDebug() << "Name:" << name;
        ProfileInfo *info = new ProfileInfo();
        info->Name = name;
        info->CreationTime = QDateTime::currentDateTime();
        info->ModifiedTime = QDateTime::currentDateTime();
        m_graphService->CreateTemperatureProfile(info);
    }
}

