#include "TempProfileEditorFrame.h"
#include "ui_TempProfileEditorFrame.h"

#include <Services/FrameManager.h>

#include <Frames/Dialogs/inputtextdialog.h>

TempProfileEditorFrame::TempProfileEditorFrame(const ValueByDayProfile &profile, QWidget *parent) :
    FrameBase(parent),
    ui(new Ui::GraphEditorFrame)
{
    ui->setupUi(this);
    setTitle("Редактирование профиля температуры");
    m_profile = profile;
    m_model = new TempProfileModel(&m_profile);

    ui->table->setModel(m_model);
    ui->table->resizeColumnsToContents();
    m_selection = ui->table->selectionModel();

    ui->table->selectColumn(0);
    ui->txtName->setText(m_profile.Info.Name);
    ui->txtDescription->setText((m_profile.Info.Description));

    ui->lblCreationDate->setText(m_profile.Info.CreationTime.toString("dd.MM.yyyy hh:mm"));
    ui->lblEditDate->setText(m_profile.Info.ModifiedTime.toString("dd.MM.yyyy hh:mm"));

    connect(ui->txtName, &QClickableLineEdit::clicked, this, &TempProfileEditorFrame::onTxtClicked);
    connect(ui->txtDescription, &QClickableLineEdit::clicked, this, &TempProfileEditorFrame::onTxtClicked);
}

TempProfileEditorFrame::~TempProfileEditorFrame()
{
    disconnect(ui->txtName, &QClickableLineEdit::clicked, this, &TempProfileEditorFrame::onTxtClicked);
    disconnect(ui->txtDescription, &QClickableLineEdit::clicked, this, &TempProfileEditorFrame::onTxtClicked);
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
    m_profile.Info.Name = ui->txtName->text();
    m_profile.Info.Description = ui->txtDescription->text();

    emit editComplete(m_profile);
    FrameManager::instance()->PreviousFrame();
}


void TempProfileEditorFrame::on_btnCancel_clicked()
{
    emit editCanceled();
    FrameManager::instance()->PreviousFrame();
}


void TempProfileEditorFrame::on_btnAddPoint_clicked()
{
    ValueByDayPoint point;
    ValueByDayEditDialog *dialog = new ValueByDayEditDialog(FrameManager::instance()->MainWindow());

    dialog->setValue(&point);
    dialog->setTitle("Добавление точки");
    dialog->setValueName("Температура");

    if(dialog->exec() == QDialog::Accepted)
    {
        ui->table->setModel(nullptr);
        m_profile.Points.append(point);
        ui->table->setModel(m_model);
        ui->table->resizeColumnsToContents();
    }

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


void TempProfileEditorFrame::on_btnEditPoint_clicked()
{

}


void TempProfileEditorFrame::on_btnRemovePoint_clicked()
{

}


void TempProfileEditorFrame::on_btnLeft_clicked()
{
    m_selection = ui->table->selectionModel();
    QModelIndexList indexes = m_selection->selectedIndexes();
    int currentIndex = indexes.at(0).column();
    if(currentIndex == 0)
        selectColumn(currentIndex);
    else
        selectColumn(currentIndex - 1);
}


void TempProfileEditorFrame::on_btnRight_clicked()
{
    m_selection = ui->table->selectionModel();
    int currentIndex = 0;
    QModelIndexList indexes = m_selection->selectedIndexes();
    if(indexes.count() > 0)
    {

        currentIndex = indexes.at(0).column();

        if(currentIndex == (m_model->columnCount()-1))
            ui->table->selectColumn(currentIndex);
        else
            ui->table->selectColumn(currentIndex + 1);
    }
    else
        ui->table->selectColumn(0);
}

void TempProfileEditorFrame::selectColumn(int column)
{
    QModelIndex left;
    QModelIndex right;

    left = m_model->index(0, column, QModelIndex());
    right = m_model->index(m_model->rowCount() - 1, column, QModelIndex());

    QItemSelection selection(left, right);
    m_selection = ui->table->selectionModel();
    m_selection->clear();
    m_selection->select(selection, QItemSelectionModel::Select);
}

