#include "SelectProfileFrame.h"
#include "ui_SelectProfileFrame.h"

#include <ApplicationWorker.h>
#include <QDateTime>

#include <Frames/Dialogs/MessageDialog.h>
#include <Frames/Dialogs/inputtextdialog.h>

SelectProfileFrame::SelectProfileFrame(const ProfileType &profileType, QWidget *parent) :
    FrameBase(parent),
    ui(new Ui::SelectProfileFrame),
    m_infoModel(nullptr),
    m_curTempProfile(nullptr),
    m_curVentProfile(nullptr),
    m_currValveProfile(nullptr)
{
    m_profileType = profileType;
    ui->setupUi(this);
    setTitle("Выбор графика");

    INetworkService *service = ApplicationWorker::Instance()->GetNetworkService("GraphProviderService");
    if(service !=nullptr)
    {
        m_graphService = dynamic_cast<GraphService*>(service);
    }

    connect(m_graphService, &GraphService::TempInfosResponse, this, &SelectProfileFrame::ProfileInfosReceived);
    connect(m_graphService, &GraphService::VentInfosResponse, this, &SelectProfileFrame::ProfileInfosReceived);
    connect(m_graphService, &GraphService::ValveInfosResponse, this, &SelectProfileFrame::ProfileInfosReceived);


    connect(m_graphService, &GraphService::TempProfileResponse, this, &SelectProfileFrame::TempGraphReceived);
    connect(m_graphService, &GraphService::TempProfileCreated, this, &SelectProfileFrame::TempProfileCreated);
    connect(m_graphService, &GraphService::TempProfileUpdated, this, &SelectProfileFrame::TempProfileUpdated);

    connect(m_graphService, &GraphService::VentProfileResponse, this, &SelectProfileFrame::VentGraphReceived);
    connect(m_graphService, &GraphService::VentProfileCreated, this, &SelectProfileFrame::VentProfileCreated);
    connect(m_graphService, &GraphService::VentProfileUpdated, this, &SelectProfileFrame::VentProfileUpdated);

    connect(m_graphService, &GraphService::ValveProfileResponse, this, &SelectProfileFrame::ValveGraphReceived);
    connect(m_graphService, &GraphService::ValveProfileCreated, this, &SelectProfileFrame::ValveProfileCreated);
    connect(m_graphService, &GraphService::ValveProfileUpdated,this, &SelectProfileFrame::ValveProfileUpdated);



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

void SelectProfileFrame::setSelectedKey(const QString &key)
{
    m_selectedKey = key;
}

void SelectProfileFrame::ProfileInfosReceived(QList<ProfileInfo> infos)
{

    if(m_infoModel != nullptr)
    {
        delete m_infoModel;
        m_infoModel = nullptr;
    }

    m_infoModel = new ProfileInfoModel(infos, this);

    ui->profilesTable->setModel(m_infoModel);

    ui->profilesTable->resizeColumnsToContents();
    ui->profilesTable->resizeRowsToContents();

    m_selection = ui->profilesTable->selectionModel();

    int i = 0;
    for(i = 0; i < infos.count(); i++)
    {
        if(infos[i].Key == m_selectedKey)
            break;
    }

    selectRow(i);
}



void SelectProfileFrame::TempGraphReceived(ValueByDayProfile profile)
{
    if(m_curTempProfile != nullptr)
    {
        delete m_curTempProfile;
        m_curTempProfile = nullptr;
    }

    m_curTempProfile = new ValueByDayProfile(profile);
    drawTemperatureGraph(m_curTempProfile);

    if(m_needEdit)
    {
        m_tempEditor = new TempProfileEditorFrame(m_curTempProfile);
        connect(m_tempEditor, &TempProfileEditorFrame::editComplete, this, &SelectProfileFrame::onTempProfileEditorCompleted);
        FrameManager::instance()->setCurrentFrame(m_tempEditor);
    }
}

void SelectProfileFrame::VentGraphReceived(MinMaxByDayProfile profile)
{
    if(m_curVentProfile != nullptr)
    {
        delete m_curVentProfile;
        m_curVentProfile = nullptr;
    }

    m_curVentProfile = new MinMaxByDayProfile(profile);
    drawVentilationGraph(m_curVentProfile);

    if(m_needEdit)
    {
        m_ventEditor = new VentProfileEditorFrame(m_curVentProfile);
        connect(m_ventEditor, &VentProfileEditorFrame::editComplete, this, &SelectProfileFrame::onVentProfileEditorCompleted);
        FrameManager::instance()->setCurrentFrame(m_ventEditor);
    }
}

void SelectProfileFrame::ValveGraphReceived(ValueByValueProfile profile)
{
    if(m_currValveProfile != nullptr)
    {
        delete m_currValveProfile;
        m_currValveProfile = nullptr;
    }

    m_currValveProfile = new ValueByValueProfile(profile);
    drawValveGraph(m_currValveProfile);

    if(m_needEdit)
    {
        m_valveEditor = new ValveProfileEditorFrame(m_currValveProfile);
        connect(m_valveEditor, &ValveProfileEditorFrame::editComplete, this, &SelectProfileFrame::onValveProfileEditorCompleted);
        FrameManager::instance()->setCurrentFrame(m_valveEditor);
    }
}

void SelectProfileFrame::ValveProfileCreated()
{
    m_graphService->GetValveInfos();
}

void SelectProfileFrame::ValveProfileUpdated()
{
    m_graphService->GetValveInfos();
}

void SelectProfileFrame::VentProfileCreated()
{
    m_graphService->GetVentInfos();
}

void SelectProfileFrame::VentProfileUpdated()
{
    m_graphService->GetVentInfos();
}

void SelectProfileFrame::TempProfileCreated()
{
    m_graphService->GetTempInfos();
}

void SelectProfileFrame::TempProfileUpdated()
{
    m_graphService->GetTempInfos();
}

void SelectProfileFrame::onTempProfileEditorCompleted()
{
    m_graphService->UpdateTemperatureProfile(*m_curTempProfile);
    qDebug() << "Edit temperature accepted";
}

void SelectProfileFrame::onVentProfileEditorCompleted()
{
    m_graphService->UpdateVentilationProfile(*m_curVentProfile);
    qDebug() << "Edit ventilation accepted";
}

void SelectProfileFrame::onValveProfileEditorCompleted()
{
    m_graphService->UpdateValveProfile(*m_currValveProfile);
    qDebug() << "Edit valve accepted";
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
        m_needEdit = false;
        m_graphService->GetVentilationProfile(key);
        break;
    case ProfileType::ValveByVent:
        m_needEdit = false;
        m_graphService->GetValveProfile(key);
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
    plot->graph(0)->setScatterStyle(QCPScatterStyle(QCPScatterStyle::ssCircle, 5));
    plot->graph(0)->setData(days, temps);
    plot->xAxis->setRange(10,60);
    plot->yAxis->setRange(0,50);

    plot->replot();
}

void SelectProfileFrame::drawVentilationGraph(MinMaxByDayProfile *profile)
{
    QCustomPlot *plot = ui->plot;

    plot->addGraph();
    int pointCount = profile->Points.count();

    QVector<double> days(pointCount);
    QVector<double> maxValues(pointCount);
    QVector<double> minValues(pointCount);

    for(int i = 0; i < pointCount; i++)
    {
        days[i] = profile->Points[i].Day;
        maxValues[i] = profile->Points[i].MaxValue;
        minValues[i] = profile->Points[i].MinValue;
    }

    plot->graph(0)->setScatterStyle(QCPScatterStyle(QCPScatterStyle::ssCircle, 5));
    plot->graph(0)->setData(days, maxValues);
    plot->addGraph();
    plot->graph(1)->setScatterStyle(QCPScatterStyle(QCPScatterStyle::ssCircle, 5));
    plot->graph(1)->setData(days, minValues);

    plot->xAxis->setRange(0,50);
    plot->yAxis->setRange(0,5);

    plot->replot();
}

void SelectProfileFrame::drawValveGraph(ValueByValueProfile *profile)
{
    QCustomPlot *plot = ui->plot;

    plot->addGraph();
    int pointCount = profile->Points.count();


    QVector<double> ValuesX(pointCount);
    QVector<double> ValuesY(pointCount);

    for(int i = 0; i < pointCount; i++)
    {
        ValuesX[i] = profile->Points[i].ValueX;
        ValuesY[i] = profile->Points[i].ValueY;
    }

    plot->graph(0)->setScatterStyle(QCPScatterStyle(QCPScatterStyle::ssCircle, 5));
    plot->graph(0)->setData(ValuesX, ValuesY);


    plot->xAxis->setRange(0,100);
    plot->yAxis->setRange(0,100);

    plot->replot();
}


void SelectProfileFrame::on_btnUp_clicked()
{
    m_selection = ui->profilesTable->selectionModel();
    QModelIndexList indexes = m_selection->selectedIndexes();
    if(indexes.count()>0)
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


void SelectProfileFrame::on_btnDown_clicked()
{
    m_selection = ui->profilesTable->selectionModel();
    QModelIndexList indexes = m_selection->selectedIndexes();
    if(indexes.count()>0)
    {
        int currentIndex = indexes.at(0).row();
        if(currentIndex == (m_infoModel->rowCount()-1))
            selectRow(currentIndex);
        else
            selectRow(currentIndex + 1);
    }
    else
        selectRow(0);
}


void SelectProfileFrame::on_btnAdd_clicked()
{
    InputTextDialog *inputDialog = new InputTextDialog();
    inputDialog->setTitle("Добавление графика");
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
    QModelIndex index = m_selection->selectedIndexes().at(0);
    if(index.row() >= 0)
    {
        int currIndex = index.row();
        QString key = m_infoModel->infos()->at(currIndex).Key;
        switch(m_profileType)
        {
        case ProfileType::Temperature:
        {
            m_needEdit = true;
            m_graphService->GetTemperatureProfile(key);
        }
            break;
        case ProfileType::Ventilation:
        {
            m_needEdit = true;
            m_graphService->GetVentilationProfile(key);
        }
            break;
        case ProfileType::ValveByVent:
        {
            m_needEdit = true;
            m_graphService->GetValveProfile(key);
        }
            break;
        }
    }
}


void SelectProfileFrame::on_btnAccept_clicked()
{
    QModelIndexList indexes = m_selection->selectedIndexes();
    int currentIndex = indexes.at(0).row();

    emit ProfileSelected(m_infoModel->infos()->at(currentIndex));
}


void SelectProfileFrame::on_btnDelete_clicked()
{
    m_selection = ui->profilesTable->selectionModel();
    QModelIndex index = m_selection->selectedIndexes().at(0);
    if(index.row() >= 0)
    {
        int currIndex = index.row();
        QString key = m_infoModel->infos()->at(currIndex).Key;
        QString profileName = m_infoModel->infos()->at(currIndex).Name;

        MessageDialog dlg("Удаление",
                          "Вы действительно хотите удалить профиль\n" + profileName,
                          MessageDialog::YesNoDialog,
                          FrameManager::instance()->MainWindow());
        if(dlg.exec() == QDialog::Accepted)
        {
            switch(m_profileType)
            {
            case ProfileType::Temperature:
                m_graphService->RemoveTemperatureProfile(key);
                break;
            case ProfileType::Ventilation:
                m_graphService->RemoveVentilationProfile(key);
                break;
            case ProfileType::ValveByVent:
                m_graphService->RemoveValveProfile(key);
                break;
            }
        }
    }
}

