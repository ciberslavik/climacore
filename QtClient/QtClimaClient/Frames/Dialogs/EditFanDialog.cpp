#include "EditFanDialog.h"
#include "inputdigitdialog.h"
#include "inputtextdialog.h"
#include "ui_EditFanDialog.h"

#include <Services/FrameManager.h>

#include <Network/INetworkService.h>
#include "ApplicationWorker.h"

EditFanDialog::EditFanDialog(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::EditFanDialog)
{
    ui->setupUi(this);

    INetworkService *service = ApplicationWorker::Instance()->GetNetworkService("DeviceProviderService");
    if(service != nullptr)
    {
        m_devProvider = dynamic_cast<DeviceProviderService*>(service);
    }
    connect(m_devProvider, &DeviceProviderService::RelayListReceived, this, &EditFanDialog::onRelayListReceived);
    m_devProvider->GetRelayList();

    connect(ui->txtCount, &QClickableLineEdit::clicked, this, &EditFanDialog::onTxtPerfOrCont);
    connect(ui->txtPerf, &QClickableLineEdit::clicked, this, &EditFanDialog::onTxtPerfOrCont);
    connect(ui->txtMinPerf, &QClickableLineEdit::clicked, this, &EditFanDialog::onTxtDigitClicked);
    connect(ui->txtFanName, &QClickableLineEdit::clicked, this, &EditFanDialog::onTxtTextClicked);

    for(int i = 0; i < 11; i++)
    {
        ui->cboPrio->addItem(QString::number(i));
    }
}

EditFanDialog::~EditFanDialog()
{
    delete ui;
}

FanInfo *EditFanDialog::getInfo()
{

    m_fanInfo->FanName = ui->txtFanName->text();
    m_fanInfo->FanCount = ui->txtCount->text().toInt();
    m_fanInfo->Performance = ui->txtPerf->text().toInt();
    m_fanInfo->StartValue = ui->txtMinPerf->text().toInt();
    m_fanInfo->RelayName = ui->cboRelaySelect->currentText();
    m_fanInfo->Priority = ui->cboPrio->currentText().toInt();
    return m_fanInfo;
}

void EditFanDialog::setInfo(FanInfo *info)
{
    m_fanInfo = info;

    ui->txtFanName->setText(m_fanInfo->FanName);
    ui->txtCount->setText(QString::number(m_fanInfo->FanCount));
    ui->txtPerf->setText(QString::number(m_fanInfo->Performance));
    ui->txtMinPerf->setText(QString::number(m_fanInfo->StartValue));
    ui->cboRelaySelect->setCurrentText(m_fanInfo->RelayName);
    ui->cboPrio->setCurrentText(QString::number(m_fanInfo->Priority));

    ui->lblTotalPerf->setText(QString::number(m_fanInfo->Performance * m_fanInfo->FanCount));
}

void EditFanDialog::on_btnAccept_clicked()
{
    m_fanInfo->FanName = ui->txtFanName->text();
    m_fanInfo->FanCount = ui->txtCount->text().toInt();
    m_fanInfo->Performance = ui->txtPerf->text().toInt();
    m_fanInfo->StartValue = ui->txtMinPerf->text().toInt();
    m_fanInfo->RelayName = ui->cboRelaySelect->currentText();
    m_fanInfo->Priority = ui->cboPrio->currentText().toInt();
    accept();
}


void EditFanDialog::on_btnCancel_clicked()
{
    reject();
}

void EditFanDialog::onTxtDigitClicked()
{
    QLineEdit *textBox = static_cast<QLineEdit*>(sender());
    QString oldValue = textBox->text();
    InputDigitDialog *dlg = new InputDigitDialog(textBox, this);

    if(dlg->exec()==QDialog::Rejected)
    {
        textBox->setText(oldValue);
    }
    dlg->deleteLater();
}

void EditFanDialog::onTxtPerfOrCont()
{
    QLineEdit *textBox = static_cast<QLineEdit*>(sender());
    QString oldValue = textBox->text();
    InputDigitDialog *dlg = new InputDigitDialog(textBox, this);

    if(dlg->exec()==QDialog::Rejected)
    {
        textBox->setText(oldValue);
    }
    dlg->deleteLater();

    int totalPerf = ui->txtPerf->text().toInt() * ui->txtCount->text().toInt();
    ui->lblTotalPerf->setText(QString::number(totalPerf));
}

void EditFanDialog::onTxtTextClicked()
{
    QLineEdit *textBox = static_cast<QLineEdit*>(sender());
    InputTextDialog *dlg = new InputTextDialog(this);
    dlg->setText(textBox->text());
    if(dlg->exec()==QDialog::Accepted)
    {
        textBox->setText(dlg->getText());
    }
    dlg->deleteLater();
}

void EditFanDialog::onRelayListReceived(QList<RelayInfo> relayList)
{
    ui->cboRelaySelect->clear();


    for(int i = 0; i<relayList.count(); i++)
    {
        ui->cboRelaySelect->addItem(relayList.at(i).Name);

    }
}



