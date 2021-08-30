#include "SelectProfileFrame.h"
#include "ui_SelectProfileFrame.h"

#include <ApplicationWorker.h>
#include <QDateTime>

#include <Frames/Dialogs/inputtextdialog.h>

SelectProfileFrame::SelectProfileFrame(const ProfileType &profileType, QWidget *parent) :
    FrameBase(parent),
    ui(new Ui::SelectProfileFrame)
{
    m_profileType = profileType;
    ui->setupUi(this);
    setTitle("Выбор графика");

    INetworkService *service = ApplicationWorker::Instance()->GetNetworkService("GraphProviderService");
    if(service !=nullptr)
    {
        m_graphService = dynamic_cast<GraphService*>(service);
    }
    connect(m_graphService, &GraphService::TempProfileResponse, this, &SelectProfileFrame::TemperatureGraphReceived);

    switch (m_profileType) {
    case ProfileType::Temperature:
        m_graphService->GetTempInfos();
        connect(m_graphService, &GraphService::TempInfosResponse, this, &SelectProfileFrame::ProfileInfosReceived);
        break;
    case ProfileType::Ventilation:
        break;
    case ProfileType::ValveByVent:
        break;
    case ProfileType::MineByVent:
        break;


    }
    //


}

SelectProfileFrame::~SelectProfileFrame()
{
    delete ui;
}

QString SelectProfileFrame::getFrameName()
{
    return "SelectProfileFrame";
}

void SelectProfileFrame::ProfileInfosReceived(QList<ProfileInfo> *infos)
{
    m_infoModel = new ProfileInfoModel(infos, this);

    ui->profilesTable->setModel(m_infoModel);

    ui->profilesTable->resizeColumnsToContents();
    ui->profilesTable->resizeRowsToContents();

    m_selection = ui->profilesTable->selectionModel();
    selectRow(0);
}

void SelectProfileFrame::TemperatureGraphReceived(ValueByDayProfile *profile)
{
    m_curTempProfile = profile;

    if(m_needEdit)
    {
        m_tempEditor = new TempProfileEditorFrame(m_curTempProfile);
        connect(m_tempEditor, &TempProfileEditorFrame::editComplete, this, &SelectProfileFrame::on_ProfileEditorCompleted);
        connect(m_tempEditor, &TempProfileEditorFrame::editCanceled, this, &SelectProfileFrame::on_ProfileEditorCanceled);
        FrameManager::instance()->setCurrentFrame(m_tempEditor);


    }


}

void SelectProfileFrame::on_ProfileEditorCompleted()
{
    m_graphService->UpdateTemperatureProfile(m_curTempProfile);
    qDebug()<< "Edit accepted";
}

void SelectProfileFrame::on_ProfileEditorCanceled()
{

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

    QString key = m_infoModel->infos()->at(row).Key;
    loadGraph(key);

    switch(m_profileType)
    {
    case ProfileType::Temperature:
        loadTemperatureGraph(key);
        break;
    case ProfileType::Ventilation:
        break;
    case ProfileType::ValveByVent:
        break;
    case ProfileType::MineByVent:
        break;

    }
}

void SelectProfileFrame::loadGraph(const QString &key)
{

}

void SelectProfileFrame::loadTemperatureGraph(const QString &key)
{
    m_graphService->GetTemperatureProfile(key);
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


void SelectProfileFrame::on_btnEdit_clicked()
{
    switch(m_profileType)
    {
    case ProfileType::Temperature:
    {
        m_tempEditor = new TempProfileEditorFrame(m_curTempProfile);
        connect(m_tempEditor, &TempProfileEditorFrame::editComplete, this, &SelectProfileFrame::on_ProfileEditorCompleted);
        connect(m_tempEditor, &TempProfileEditorFrame::editCanceled, this, &SelectProfileFrame::on_ProfileEditorCanceled);
        FrameManager::instance()->setCurrentFrame(m_tempEditor);
    }
        break;
    case ProfileType::Ventilation:
        break;
    case ProfileType::ValveByVent:
        break;
    case ProfileType::MineByVent:
        break;

    }
}

