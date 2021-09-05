#include "TempProfileEditorFrame.h"
#include "ui_TempProfileEditorFrame.h"

#include <Services/FrameManager.h>

#include <Frames/Dialogs/inputtextdialog.h>

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

    ui->txtName->setText(profile->Info.Name);
    ui->txtDescription->setText((profile->Info.Description));

    ui->lblCreationDate->setText(profile->Info.CreationTime.toString("dd.MM.yyyy hh:mm"));
    ui->lblEditDate->setText(profile->Info.ModifiedTime.toString("dd.MM.yyyy hh:mm"));

    connect(ui->txtName, &QClickableLineEdit::clicked, this, &TempProfileEditorFrame::onTxtClicked);
    connect(ui->txtDescription, &QClickableLineEdit::clicked, this, &TempProfileEditorFrame::onTxtClicked);
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
    ui->table->update();
}

void TempProfileEditorFrame::onTxtClicked()
{
    QLineEdit *txt = dynamic_cast<QLineEdit*>(sender());
    InputTextDialog *dlg = new InputTextDialog(FrameManager::instance()->MainWindow());

    dlg->setText(txt->text());

    if(dlg->exec() == QDialog::Accepted)
    {
        txt->setText(dlg->getText());
    }

}

