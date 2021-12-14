#include "EditLightProfileDialog.h"
#include "ui_EditLightProfileDialog.h"

EditLightProfileDialog::EditLightProfileDialog(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::EditLightProfileDialog)
{
    ui->setupUi(this);
}

EditLightProfileDialog::~EditLightProfileDialog()
{
    delete ui;
}

void EditLightProfileDialog::on_btnAccept_clicked()
{
    m_Name = ui->txtName->text();
    m_Description = ui->txtDescription->text();
    accept();
}


void EditLightProfileDialog::on_btnReject_clicked()
{

}


const QString &EditLightProfileDialog::Name() const
{
    return m_Name;
}

void EditLightProfileDialog::setName(const QString &newName)
{
    if (m_Name == newName)
        return;
    m_Name = newName;
    ui->txtName->setText(m_Name);
    emit NameChanged();
}

const QString &EditLightProfileDialog::Description() const
{
    return m_Description;
}

void EditLightProfileDialog::setDescription(const QString &newDescription)
{
    if (m_Description == newDescription)
        return;
    m_Description = newDescription;
    ui->txtDescription->setText(m_Description);
    emit DescriptionChanged();
}

void EditLightProfileDialog::setTitle(const QString &title)
{
    ui->lblTitle->setText(title);
}
