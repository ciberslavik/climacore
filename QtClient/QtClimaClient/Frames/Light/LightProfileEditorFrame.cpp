#include "LightProfileEditorFrame.h"
#include "ui_LightProfileEditorFrame.h"
#include "Services/FrameManager.h"
#include "Frames/Dialogs/LineEditorDialog.h"

#include <Frames/Dialogs/EditTimerDialog.h>
#include <Frames/Dialogs/MessageDialog.h>

#include <ApplicationWorker.h>
LightProfileEditorFrame::LightProfileEditorFrame(LightTimerProfile *profile, QWidget *parent) :
    FrameBase(parent),
    ui(new Ui::LightProfileEditorFrame),
    m_profile(profile)
{
    ui->setupUi(this);
    ui->txtProfileName->setText(m_profile->Name);
    ui->txtDescription->setText(m_profile->Description);

    m_infoModel = new TimerProfileModel(m_profile);

    ui->tableView->setModel(m_infoModel);

    connect(ui->tableView->selectionModel(), &QItemSelectionModel::selectionChanged, this, &LightProfileEditorFrame::onSelectionChanged);
    INetworkService *service = ApplicationWorker::Instance()->GetNetworkService("LightControllerService");
    if(service != nullptr)
    {
        m_lightService = dynamic_cast<LightControllerService*>(service);
    }
    ui->btnAddDay->setToolTip("Добавить новый день");
    ui->btnEditDay->setToolTip("Изменить выделенный день");
    ui->btnRemoveDay->setToolTip("Удалить выделенный день");

    ui->btnAddTimer->setToolTip("Добавить новый таймер");
    ui->btnEditTimer->setToolTip("Изменить выделенный таймер");
    ui->btnRemoveTimer->setToolTip("Удалить выделенный таймер");
    m_isModified = false;
    ui->btnAccept->setEnabled(false);
}

LightProfileEditorFrame::~LightProfileEditorFrame()
{
    delete ui;
}

QString LightProfileEditorFrame::getFrameName()
{
    return "LightProfileEditorFrame";
}

void LightProfileEditorFrame::on_btnReturn_clicked()
{
    FrameManager::instance()->PreviousFrame();
}


void LightProfileEditorFrame::on_btnAccept_clicked()
{
    m_lightService->UpdateProfile(*m_profile);
    m_isModified = false;
    FrameManager::instance()->PreviousFrame();
}


void LightProfileEditorFrame::on_btnAddDay_clicked()
{

    LineEditorDialog *dlg = new LineEditorDialog(FrameManager::instance()->MainWindow());
    dlg->setTitle("Добавление дня (0 - 50)");
    dlg->setValueName("День");
    QValidator *validator = new QIntValidator(0, 50, dlg);
    dlg->setValidator(validator);

    if(dlg->exec()==QDialog::Accepted)
    {
        //ui->tableView->setModel(nullptr);
        LightTimerDay day;
        day.DayNumber = dlg->value().toInt();
        m_profile->Days.append(day);
        m_infoModel->DataUpdated();
        ui->tableView->update();

        if(!m_isModified)
        {
            m_isModified = true;
            ui->btnAccept->setEnabled(true);
        }
    }
    delete dlg;
}


void LightProfileEditorFrame::on_btnEditDay_clicked()
{
    QItemSelectionModel *sel = ui->tableView->selectionModel();
    LightTimerDay *day;
    if(sel->selectedIndexes().count()>0)
    {
        day = &m_profile->Days[sel->selectedIndexes().at(0).column()];

        LineEditorDialog *dlg = new LineEditorDialog(FrameManager::instance()->MainWindow());
        dlg->setTitle("Редактирование дня № " + QString::number(day->DayNumber));
        dlg->setValueName("День");
        dlg->setValue(QString::number(day->DayNumber));

        QValidator *validator = new QIntValidator(0, 50, dlg);
        dlg->setValidator(validator);

        if(dlg->exec() == QDialog::Accepted)
        {
            day->DayNumber = dlg->value().toInt();
            m_infoModel->DataUpdated();
            ui->tableView->update();

            if(!m_isModified)
            {
                m_isModified = true;
                ui->btnAccept->setEnabled(true);
            }
        }
    }

}


void LightProfileEditorFrame::on_btnRemoveDay_clicked()
{
    QItemSelectionModel *sel = ui->tableView->selectionModel();
    LightTimerDay *day;
    if(sel->selectedIndexes().count()>0)
    {
        day = &m_profile->Days[sel->selectedIndexes().at(0).column()];
        MessageDialog *dlg = new MessageDialog("Удаление",
                                               "Вы действительно хотите удалить день № " + QString::number(day->DayNumber) + ",\nи содержащиеся в нем таймеры",
                                               MessageDialog::YesNoDialog);
        if(dlg->exec()==QDialog::Accepted)
        {
            m_profile->Days.removeAt(sel->selectedIndexes().at(0).column());
            m_infoModel->DataUpdated();
            if(!m_isModified)
            {
                m_isModified = true;
                ui->btnAccept->setEnabled(true);
            }
        }
    }
}




void LightProfileEditorFrame::on_btnAddTimer_clicked()
{
    EditTimerDialog *dlg = new EditTimerDialog(FrameManager::instance()->MainWindow());

    LightTimerDay *day;
    QItemSelectionModel *sel = ui->tableView->selectionModel();

    if(sel->selectedIndexes().count()>0)
    {
        day = &m_profile->Days[sel->selectedIndexes().at(0).column()];
    }
    else
    {
        day = &m_profile->Days[m_profile->Days.count() - 1];
    }

    dlg->setTitle("День:" + QString::number(day->DayNumber));
    if(dlg->exec() == QDialog::Accepted)
    {

        LightTimerItem tmr;
        tmr.OnTime = dlg->onTime();
        tmr.OffTime = dlg->offTime();
        day->Timers.append(tmr);
        m_infoModel->DataUpdated();
        ui->tableView->resizeColumnsToContents();

        if(!m_isModified)
        {
            m_isModified = true;
            ui->btnAccept->setEnabled(true);
        }
    }

    delete dlg;
}


void LightProfileEditorFrame::on_btnRemoveTimer_clicked()
{
    QItemSelectionModel *selectionModel = ui->tableView->selectionModel();
    if(selectionModel->selectedIndexes().count()>0)
    {
        int row = selectionModel->selectedIndexes().at(0).row();
        int col = selectionModel->selectedIndexes().at(0).column();
        LightTimerItem timerItem = m_profile->Days[col].Timers[row];

        MessageDialog *dlg = new MessageDialog("Удаление",
                                               "Вы действительно хотите удалить таймер\n" + timerItem.OnTime.toString("hh:mm") + "-"+timerItem.OffTime.toString("hh:mm"),
                                               MessageDialog::DialogType::YesNoDialog);
        if(dlg->exec() == QDialog::Accepted)
        {
            m_profile->Days[col].Timers.removeAt(row);
            m_infoModel->DataUpdated();

            if(!m_isModified)
            {
                m_isModified = true;
                ui->btnAccept->setEnabled(true);
            }
        }

        delete dlg;
    }
}

void LightProfileEditorFrame::onSelectionChanged(const QItemSelection &selected, const QItemSelection &deselected)
{
    if(selected.indexes().count() > 0)
    {
        int row = selected.indexes().at(0).row();
        int col = selected.indexes().at(0).column();

        qDebug() << "Selected c:" << col << " r:" << row;
    }
}


void LightProfileEditorFrame::on_btnEditTimer_clicked()
{
    QItemSelectionModel *selectionModel = ui->tableView->selectionModel();
    if(selectionModel->selectedIndexes().count()>0)
    {
        int row = selectionModel->selectedIndexes().at(0).row();
        int col = selectionModel->selectedIndexes().at(0).column();

        LightTimerItem timerItem = m_profile->Days[col].Timers[row];

        EditTimerDialog *dlg = new EditTimerDialog(FrameManager::instance()->MainWindow());
        dlg->setTimerInfo(timerItem);
        dlg->setTitle("День:" + QString::number(m_profile->Days[col].DayNumber));

        if(dlg->exec() == QDialog::Accepted)
        {
            LightTimerItem tmr;
            tmr.OnTime = dlg->onTime();
            tmr.OffTime = dlg->offTime();
            m_profile->Days[col].Timers[row] = tmr;
            m_infoModel->DataUpdated();

            if(!m_isModified)
            {
                m_isModified = true;
                ui->btnAccept->setEnabled(true);
            }
        }
    }
}

