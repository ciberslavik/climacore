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
    connect(m_graphService, &GraphService::TempProfileResponse, this, &SelectProfileFrame::TempGraphReceived);
    connect(m_graphService, &GraphService::TempInfosResponse, this, &SelectProfileFrame::TempInfosReceived);
    connect(m_graphService, &GraphService::TempProfileCreated, this, &SelectProfileFrame::TempProfileCreated);
    connect(m_graphService, &GraphService::TempProfileUpdated, this, &SelectProfileFrame::TempProfileUpdated);

    connect(m_graphService, &GraphService::VentProfileResponse, this, &SelectProfileFrame::VentGraphReceived);
    connect(m_graphService, &GraphService::ValveProfileResponse, this, &SelectProfileFrame::ValveGraphReceived);

    connect(m_graphService, &GraphService::VentInfosResponse, this, &SelectProfileFrame::VentInfosReceived);
    connect(m_graphService, &GraphService::ValveInfosResponse, this, &SelectProfileFrame::ValveInfosReceived);

    switch (m_profileType) {
    case ProfileType::Temperature:
        m_graphService->GetTempInfos();
        break;
    case ProfileType::Ventilation:
        m_graphService->GetVentInfos();
        break;
    case ProfileType::ValveByVent:
        m_graphService->GetValveInfos();
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

void SelectProfileFrame::TempInfosReceived(QList<ProfileInfo> infos)
{
    m_infoModel = new ProfileInfoModel(infos, this);

    ui->profilesTable->setModel(m_infoModel);

    ui->profilesTable->resizeColumnsToContents();
    ui->profilesTable->resizeRowsToContents();

    m_selection = ui->profilesTable->selectionModel();
    selectRow(0);
}

void SelectProfileFrame::VentInfosReceived(QList<ProfileInfo> infos)
{

}

void SelectProfileFrame::ValveInfosReceived(QList<ProfileInfo> infos)
{

}

void SelectProfileFrame::TempGraphReceived(ValueByDayProfile profile)
{
    drawTemperatureGraph(&profile);

    if(m_needEdit)
    {
        m_tempEditor = new TempProfileEditorFrame(profile);
        connect(m_tempEditor, &TempProfileEditorFrame::editComplete, this, &SelectProfileFrame::onTempProfileEditorCompleted);
        connect(m_tempEditor, &TempProfileEditorFrame::editCanceled, this, &SelectProfileFrame::on_ProfileEditorCanceled);
        FrameManager::instance()->setCurrentFrame(m_tempEditor);
    }
}

void SelectProfileFrame::VentGraphReceived(MinMaxByDayProfile profile)
{

}

void SelectProfileFrame::ValveGraphReceived(ValueByValueProfile profile)
{

}

void SelectProfileFrame::TempProfileCreated()
{
    m_graphService->GetTempInfos();
}

void SelectProfileFrame::TempProfileUpdated()
{
    m_graphService->GetTempInfos();
}

void SelectProfileFrame::onTempProfileEditorCompleted(const ValueByDayProfile &profile)
{
    m_graphService->UpdateTemperatureProfile(profile);
    qDebug()<< "Edit temperature accepted";
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

    switch(m_profileType)
    {
    case ProfileType::Temperature:
        m_needEdit = false;
        m_graphService->GetTemperatureProfile(key);
        break;
    case ProfileType::Ventilation:
        break;
    case ProfileType::ValveByVent:
        break;
    }
}

void SelectProfileFrame::drawTemperatureGraph(ValueByDayProfile *profile)
{
    QCustomPlot *plot = ui->plot;

    plot->addGraph();
    int pointCount = profile->Points.count();

    QVector<double> days(pointCount);
    QVector<double> temps(pointCount);

    for(int i = 0; i < pointCount; i++)
    {
        days[i] = profile->Points[i].Day;
        temps[i] = profile->Points[i].Value;
    }

    plot->graph(0)->setData(days, temps);
    plot->xAxis->setRange(1,60);
    plot->yAxis->setRange(10,50);

    plot->replot();
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
        ProfileInfo info;
        info.Name = name;
        info.CreationTime = QDateTime::currentDateTime();
        info.ModifiedTime = QDateTime::currentDateTime();

        switch (m_profileType) {
        case ProfileType::Temperature:
            m_graphService->CreateTemperatureProfile(info);
            break;
        case ProfileType::Ventilation:
            m_graphService->CreateVentilationProfile(info);
            break;
        case ProfileType::ValveByVent:
            m_graphService->CreateValveProfile(info);
            break;
        }
    }
}


void SelectProfileFrame::on_btnEdit_clicked()
{

    m_selection = ui->profilesTable->selectionModel();
    if(!m_selection->hasSelection())
        return;

    switch(m_profileType)
    {
    case ProfileType::Temperature:
    {
        m_needEdit = true;
        QModelIndex index = m_selection->selectedIndexes().at(0);
        if(index.row() >= 0)
        {
            QString key = m_infoModel->infos()->at(index.row()).Key;
            m_graphService->GetTemperatureProfile(key);
        }
    }
        break;
    case ProfileType::Ventilation:
        break;
    case ProfileType::ValveByVent:
        break;
    }
}


void SelectProfileFrame::on_btnAccept_clicked()
{
    QModelIndexList indexes = m_selection->selectedIndexes();
    int currentIndex = indexes.at(0).row();

    emit ProfileSelected(m_infoModel->infos()->at(currentIndex));
}

