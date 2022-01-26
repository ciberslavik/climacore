#include "Frames/Light/LightProfileEditorFrame.h"
#include "SelectTimerProfileFrame.h"
#include "ui_SelectTimerProfileFrame.h"

#include <Services/FrameManager.h>

#include <Frames/Dialogs/EditLightProfileDialog.h>

#include <ApplicationWorker.h>

SelectTimerProfileFrame::SelectTimerProfileFrame(QWidget *parent) :
    FrameBase(parent),
    ui(new Ui::SelectTimerProfileFrame)
{
    ui->setupUi(this);

    INetworkService *service = ApplicationWorker::Instance()->GetNetworkService("LightControllerService");
    if(service != nullptr)
    {
        m_lightService = dynamic_cast<LightControllerService*>(service);
    }
    connect(m_lightService, &LightControllerService::ProfileCreated, this, &SelectTimerProfileFrame::LightProfileCreated);
    connect(m_lightService, &LightControllerService::ProfileReceived, this, &SelectTimerProfileFrame::LightProfileReceived);
    connect(m_lightService, &LightControllerService::PresetListReceived, this, &SelectTimerProfileFrame::LightProfileListReceived);

    m_infosModel = new SelectTimerProfileInfoModel();
    connect(ui->tableView->selectionModel(), &QItemSelectionModel::selectionChanged, this, &SelectTimerProfileFrame::onSelectionChanged);
    m_lightService->GetProfileInfoList();

    m_needEditProfile = false;
}

SelectTimerProfileFrame::~SelectTimerProfileFrame()
{
    disconnect(m_lightService, &LightControllerService::ProfileCreated, this, &SelectTimerProfileFrame::LightProfileCreated);
    delete m_infosModel;
    delete ui;
}


QString SelectTimerProfileFrame::getFrameName()
{
    return "SelectTimerProfileFrame";
}

void SelectTimerProfileFrame::on_btnReturn_clicked()
{
    FrameManager::instance()->PreviousFrame();
}


void SelectTimerProfileFrame::on_btnAddProfile_clicked()
{
    EditLightProfileDialog *dlg = new EditLightProfileDialog(FrameManager::instance()->MainWindow());
    dlg->setTitle("Создание нового профиля");
    dlg->setName("Новый профиль");

    if(dlg->exec() == QDialog::Accepted)
    {
        LightTimerProfile profile;
        profile.Name = dlg->Name();
        profile.Description = dlg->Description();

        m_lightService->CreateTimerProfile(dlg->Name(), dlg->Description());
    }

    delete dlg;
}


void SelectTimerProfileFrame::on_btnEditProfile_clicked()
{
    QItemSelectionModel *selection = ui->tableView->selectionModel();
    if(selection->selectedIndexes().count() > 0)
    {
        int index = selection->selectedIndexes().at(0).row();
        QString key = m_infos[index].Key;
        m_needEditProfile = true;
        m_lightService->GetProfile(key);
    }
}


void SelectTimerProfileFrame::on_btnRemoveProfile_clicked()
{

}


void SelectTimerProfileFrame::on_btnAccept_clicked()
{
    QItemSelectionModel *selection = ui->tableView->selectionModel();
    if(selection->selectedIndexes().count() > 0)
    {
        int row = selection->selectedIndexes().at(0).row();
        m_lightService->SetCurrentProfile(m_infos[row].Key);
    }

    FrameManager::instance()->PreviousFrame();
}

void SelectTimerProfileFrame::onSelectionChanged(const QItemSelection &selected, const QItemSelection &deselected)
{
    Q_UNUSED(deselected)
    if(selected.count() > 0)
    {
        QString key = m_infos[selected[0].indexes().at(0).row()].Key;
        m_lightService->GetProfile(key);
    }
}

void SelectTimerProfileFrame::LightProfileCreated(const LightTimerProfile &profile)
{
    m_editProfile = new LightTimerProfile(profile);
    LightProfileEditorFrame *frame = new LightProfileEditorFrame(m_editProfile);

    FrameManager::instance()->setCurrentFrame(frame);
}

void SelectTimerProfileFrame::LightProfileListReceived(const QList<LightTimerProfileInfo> &infos)
{
    m_infos = infos;
    m_infosModel->setProfileList(&m_infos);
    ui->tableView->setModel(m_infosModel);
    ui->tableView->resizeColumnsToContents();
    connect(ui->tableView->selectionModel(), &QItemSelectionModel::selectionChanged, this, &SelectTimerProfileFrame::onSelectionChanged);
}

void SelectTimerProfileFrame::LightProfileReceived(const LightTimerProfile &profile)
{
    ui->timerPresenter->setTimerProfile(profile);
    if(m_needEditProfile)
    {
        m_needEditProfile = false;
        m_editProfile = new LightTimerProfile(profile);
        LightProfileEditorFrame *frame = new LightProfileEditorFrame(m_editProfile);
        FrameManager::instance()->setCurrentFrame(frame);
    }
}

