#include "TemperatureConfigFrame.h"
#include "ui_TemperatureConfigFrame.h"

#include <Services/FrameManager.h>

#include <Frames/Dialogs/HeaterConfigDialog.h>

#include <ApplicationWorker.h>

TemperatureConfigFrame::TemperatureConfigFrame(QWidget *parent) :
    FrameBase(parent),
    ui(new Ui::TemperatureConfigFrame),
    m_ProfileSelector(nullptr),
    m_heaterService(nullptr)
{
    ui->setupUi(this);
    connect(ui->txtMinTemp, &QClickableLineEdit::clicked, this, &TemperatureConfigFrame::onTxtClicked);
    connect(ui->txtMaxTemp, &QClickableLineEdit::clicked, this, &TemperatureConfigFrame::onTxtClicked);
    connect(ui->txtCorrection, &QClickableLineEdit::clicked, this, &TemperatureConfigFrame::onTxtClicked);

    INetworkService *service = ApplicationWorker::Instance()->GetNetworkService("HeaterService");
    if(service != nullptr)
    {
        m_heaterService = dynamic_cast<HeaterControllerService*>(service);
    }
}

TemperatureConfigFrame::~TemperatureConfigFrame()
{
    delete ui;
}

void TemperatureConfigFrame::on_btnReturn_clicked()
{
    FrameManager::instance()->PreviousFrame();
}

void TemperatureConfigFrame::onProfileSelected(ProfileInfo profileInfo)
{
    ui->lblProfileName->setText(profileInfo.Name);
}

QString TemperatureConfigFrame::getFrameName()
{
    return "TemperatureConfigFrame";
}


void TemperatureConfigFrame::on_btnSelectGraph_clicked()
{
    m_ProfileSelector = new SelectProfileFrame(ProfileType::Temperature);
    connect(m_ProfileSelector,
            &SelectProfileFrame::ProfileSelected,
            this,
            &TemperatureConfigFrame::onProfileSelected);

    FrameManager::instance()->setCurrentFrame(m_ProfileSelector);
}


void TemperatureConfigFrame::on_btnHeater1_clicked()
{
    HeaterConfigDialog *dlg = new HeaterConfigDialog(FrameManager::instance()->MainWindow());
    if(dlg->exec()==QDialog::Accepted)
    {

    }
}


void TemperatureConfigFrame::on_btnHeater2_clicked()
{
    HeaterConfigDialog *dlg = new HeaterConfigDialog(FrameManager::instance()->MainWindow());
    if(dlg->exec()==QDialog::Accepted)
    {

    }
}

void TemperatureConfigFrame::onTxtClicked()
{
    QLineEdit *txt = static_cast<QLineEdit*>(sender());
    QString oldValue = txt->text();
    InputDigitDialog dlg(txt,FrameManager::instance()->MainWindow());
    if(dlg.exec()==QDialog::Rejected)
    {
        txt->setText(oldValue);
    }

}

