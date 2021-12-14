#include "LightProfileEditorFrame.h"
#include "ui_LightProfileEditorFrame.h"
#include "Services/FrameManager.h"
#include "Frames/Dialogs/LineEditorDialog.h"

#include <Frames/Dialogs/EditTimerDialog.h>
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
    }
    delete dlg;
}


void LightProfileEditorFrame::on_btnEditDay_clicked()
{

}


void LightProfileEditorFrame::on_btnRemoveDay_clicked()
{

}




void LightProfileEditorFrame::on_btnAddTimer_clicked()
{
    EditTimerDialog *dlg = new EditTimerDialog(FrameManager::instance()->MainWindow());


    if(dlg->exec() == QDialog::Accepted)
    {
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
        LightTimerItem tmr;
        tmr.OnTime = dlg->onTime();
        tmr.OffTime = dlg->offTime();
        day->Timers.append(tmr);
        m_infoModel->DataUpdated();
        ui->tableView->resizeColumnsToContents();
    }

    delete dlg;
}


void LightProfileEditorFrame::on_btnRemoveTimer_clicked()
{

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

