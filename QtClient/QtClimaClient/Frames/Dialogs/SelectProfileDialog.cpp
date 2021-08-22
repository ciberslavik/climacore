#include "SelectProfileDialog.h"
#include "ui_SelectProfileDialog.h"



SelectProfileDialog::SelectProfileDialog(QList<ProfileInfo> *infos, QWidget *parent):
    QDialog(parent),
    ui(new Ui::SelectProfileDialog)
{
    ui->setupUi(this);
    m_profiles = infos;

    m_model = new ProfileInfoModel(infos, this);

    ui->tableView->setModel(m_model);

    m_selection = ui->tableView->selectionModel();
   selectRow(0);
}

SelectProfileDialog::~SelectProfileDialog()
{
    delete ui;
}

const QString& SelectProfileDialog::SelectedProfile()
{
    QModelIndexList indexes = m_selection->selectedIndexes();
    int index = indexes.at(0).row();
    return m_profiles->at(index).Key;
}

void SelectProfileDialog::setSelectedProfile(const QString &profileKey)
{


    int index = m_model->getRowNumber(profileKey);

    selectRow(index);
}

void SelectProfileDialog::on_btnUp_clicked()
{
    QModelIndexList indexes = m_selection->selectedIndexes();
    int currentIndex = indexes.at(0).row();
    if(currentIndex == 0)
        selectRow(currentIndex);
    else
        selectRow(currentIndex - 1);
}


void SelectProfileDialog::on_btnDown_clicked()
{
    QModelIndexList indexes = m_selection->selectedIndexes();
    int currentIndex = indexes.at(0).row();
    if(currentIndex == (m_model->rowCount()-1))
        selectRow(currentIndex);
    else
        selectRow(currentIndex + 1);

}


void SelectProfileDialog::selectRow(int row)
{
    QModelIndex left;
    QModelIndex right;

    left = m_model->index(row, 0, QModelIndex());
    right = m_model->index(row, 1, QModelIndex());

    QItemSelection selection(left, right);
    m_selection->clear();
    m_selection->select(selection, QItemSelectionModel::Select);
}


void SelectProfileDialog::on_btnAccept_clicked()
{
    accept();
}


void SelectProfileDialog::on_btnCancel_clicked()
{
    reject();
}

