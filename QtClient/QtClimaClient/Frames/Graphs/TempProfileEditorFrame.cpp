#include "TempProfileEditorFrame.h"
#include "ui_TempProfileEditorFrame.h"

#include <Services/FrameManager.h>

TempProfileEditorFrame::TempProfileEditorFrame(ValueByDayProfile *profile, QWidget *parent) :
    FrameBase(parent),
    ui(new Ui::GraphEditorFrame)
{
    ui->setupUi(this);
    setTitle("Редактирование профиля температуры");
    m_profile = profile;
    m_model = new TempProfileModel(m_profile);

    ui->table->setModel(m_model);

    m_selection = ui->table->selectionModel();
}

TempProfileEditorFrame::~TempProfileEditorFrame()
{
    delete ui;
}

QString TempProfileEditorFrame::getFrameName()
{
    return "TempProfileEditorFrame";
}

void TempProfileEditorFrame::tableSelectionChanged()
{

}

void TempProfileEditorFrame::on_btnAccept_clicked()
{

    emit editComplete();
    FrameManager::instance()->PreviousFrame();
}


void TempProfileEditorFrame::on_btnCancel_clicked()
{

    emit editCanceled();
    FrameManager::instance()->PreviousFrame();
}


void TempProfileEditorFrame::on_btnAddPoint_clicked()
{
    ValueByDayPoint *point = new ValueByDayPoint();
    ValueByDayEditDialog *dialog = new ValueByDayEditDialog(FrameManager::instance()->MainWindow());

    dialog->setValue(point);
    dialog->setTitle("Добавление точки");
    dialog->setValueName("Температура");

    if(dialog->exec() == QDialog::Accepted)
    {
        m_profile->Points.append(*point);
    }

    dialog->deleteLater();
}

