#include "HeaterControllerFrame.h"
#include "ui_HeaterControllerFrame.h"

#include <Services/FrameManager.h>
#include <ApplicationWorker.h>
#include <TimerPool.h>
#include <Frames/Dialogs/inputdigitdialog.h>

HeaterControllerFrame::HeaterControllerFrame(QWidget *parent) :
    FrameBase(parent),
    ui(new Ui::HeaterControllerFrame)
{
    ui->setupUi(this);

    ui->cboControlZone1->addItem("Фронт",0);
    ui->cboControlZone1->addItem("Тыл",1);
    ui->cboControlZone2->addItem("Фронт",0);
    ui->cboControlZone2->addItem("Тыл",1);

    connect(ui->txtCorrection1, &QClickableLineEdit::clicked, this, &HeaterControllerFrame::onTxtCorrection1Clicked);
    connect(ui->txtCorrection2, &QClickableLineEdit::clicked, this, &HeaterControllerFrame::onTxtCorrection2Clicked);

    connect(ui->txtDeltaOn1, &QClickableLineEdit::clicked, this, &HeaterControllerFrame::onTxtDeltaOn1Clicked);
    connect(ui->txtDeltaOff1, &QClickableLineEdit::clicked, this, &HeaterControllerFrame::onTxtDeltaOff1Clicked);
    connect(ui->txtDeltaOn2, &QClickableLineEdit::clicked, this, &HeaterControllerFrame::onTxtDeltaOn2Clicked);
    connect(ui->txtDeltaOff2, &QClickableLineEdit::clicked, this, &HeaterControllerFrame::onTxtDeltaOff2Clicked);


    INetworkService *service = ApplicationWorker::Instance()->GetNetworkService("HeaterControllerService");
    if(service != nullptr)
    {
        m_heaterService = dynamic_cast<HeaterControllerService*>(service);
    }

    connect(m_heaterService, &HeaterControllerService::HeaterParamsListReceived, this, &HeaterControllerFrame::onHeaterParamsListReceived);
    connect(m_heaterService, &HeaterControllerService::HeaterStateListReceived, this, &HeaterControllerFrame::onHeaterStateListReceived);

    m_heaterService->GetHeaterParams();
}

HeaterControllerFrame::~HeaterControllerFrame()
{
    delete ui;
}


void HeaterControllerFrame::on_btnReturn_clicked()
{
    FrameManager::instance()->PreviousFrame();
}

void HeaterControllerFrame::onHeaterStateListReceived(float setpoint, float front, float rear,  QList<HeaterState> heaterStates)
{
    m_setpoint = setpoint;
    if(heaterStates.at(0).IsRunning)
    {
        ui->lblState1->setText("Вкл.");
        ui->lblState1->setStyleSheet("QLabel { background-color: lightgreen }");
    }
    else
    {
        ui->lblState1->setStyleSheet("QLabel { background-color: lightgray }");
        ui->lblState1->setText("Выкл.");
    }
    if(heaterStates.at(1).IsRunning)
    {
        ui->lblState2->setStyleSheet("QLabel { background-color: lightgreen }");
        ui->lblState2->setText("Вкл.");
    }
    else
    {
        ui->lblState2->setStyleSheet("QLabel { background-color: lightgray }");
        ui->lblState2->setText("Выкл.");
    }
    ui->lblTempSetPoint->setText(QString::number(setpoint,'f',2) + " °C");
    ui->lblFrontTemp->setText(QString::number(front, 'f', 2) + " °C");
    ui->lblRearTemp->setText(QString::number(rear, 'f', 2) + " °C");

    float corected1 = setpoint + m_heaters[0].Correction;
    ui->lblHeater1SetPoint->setText(QString::number(corected1,'f',2) + " °C");
    float corected2 = setpoint + m_heaters[1].Correction;
    ui->lblHeater2SetPoint->setText(QString::number(corected2,'f',2) + " °C");
}

void HeaterControllerFrame::onHeaterParamsListReceived(QList<HeaterParams> heaterParams)
{
    m_heaters = heaterParams;


    ui->txtDeltaOn1->setText(QString::number(m_heaters[0].DeltaOn, 'f', 2));
    ui->txtDeltaOff1->setText(QString::number(m_heaters[0].DeltaOff, 'f', 2));
    ui->txtCorrection1->setText(QString::number(m_heaters[0].Correction, 'f', 2));
    ui->cboControlZone1->setCurrentIndex(m_heaters[0].ControlZone);

    ui->txtDeltaOn2->setText(QString::number(m_heaters[1].DeltaOn, 'f', 2));
    ui->txtDeltaOff2->setText(QString::number(m_heaters[1].DeltaOff, 'f', 2));
    ui->txtCorrection2->setText(QString::number(m_heaters[1].Correction, 'f', 2));
    ui->cboControlZone2->setCurrentIndex(m_heaters[1].ControlZone);

}


//void HeaterControllerFrame::onHeaterUpdated(HeaterState state)
//{

//}

void HeaterControllerFrame::onTxtCorrection1Clicked()
{
    QLineEdit *txt = static_cast<QLineEdit*>(sender());
    QString old = txt->text();
    InputDigitDialog dlg(txt, this);
    dlg.setTitle("Коррекция Отопитель 1");
    int result = dlg.exec();
    if(result==QDialog::Rejected)
    {
        txt->setText(old);
    }
    else if(result==QDialog::Accepted)
    {
        txt->setStyleSheet("QLineEdit { background-color: yellow }");

        m_heat1modify = true;
    }
}

void HeaterControllerFrame::onTxtCorrection2Clicked()
{
    QLineEdit *txt = static_cast<QLineEdit*>(sender());
    QString old = txt->text();
    InputDigitDialog dlg(txt, this);
    dlg.setTitle("Коррекция Отопитель 2");
    int result = dlg.exec();
    if(result==QDialog::Rejected)
    {
        txt->setText(old);
    }
    else if(result==QDialog::Accepted)
    {
        if(txt->text() != old)
        {
            float sp = m_setpoint + txt->text().toFloat();
            ui->lblHeater2SetPoint->setText(QString::number(sp,'f',2) + " °C");
            txt->setStyleSheet("QLineEdit { background-color: yellow }");
            m_heat2modify = true;
        }
    }
}

void HeaterControllerFrame::onTxtDeltaOn1Clicked()
{
    QLineEdit *txt = static_cast<QLineEdit*>(sender());
    QString old = txt->text();
    InputDigitDialog dlg(txt, this);
    dlg.setTitle("Дельта ВКЛ. Отопитель 1");
    int result = dlg.exec();
    if(result==QDialog::Rejected)
    {
        txt->setText(old);
    }
    else if(result==QDialog::Accepted)
    {
        if(txt->text() != old)
        {
            float sp = m_setpoint + txt->text().toFloat();
            ui->lblHeater2SetPoint->setText(QString::number(sp,'f',2) + " °C");
            txt->setStyleSheet("QLineEdit { background-color: yellow }");
            m_heat1modify = true;
        }
    }
}

void HeaterControllerFrame::onTxtDeltaOff1Clicked()
{
    QLineEdit *txt = static_cast<QLineEdit*>(sender());
    QString old = txt->text();
    InputDigitDialog dlg(txt, this);
    dlg.setTitle("Дельта ВЫКЛ. Отопитель 1");
    int result = dlg.exec();
    if(result==QDialog::Rejected)
    {
        txt->setText(old);
    }
    else if(result==QDialog::Accepted)
    {
        if(txt->text() != old)
        {
            txt->setStyleSheet("QLineEdit { background-color: yellow }");
            m_heat1modify = true;
        }
    }
}

void HeaterControllerFrame::onTxtDeltaOn2Clicked()
{
    QLineEdit *txt = static_cast<QLineEdit*>(sender());
    QString old = txt->text();
    InputDigitDialog dlg(txt, this);
    dlg.setTitle("Дельта ВКЛ. Отопитель 2");
    int result = dlg.exec();
    if(result==QDialog::Rejected)
    {
        txt->setText(old);
    }
    else if(result==QDialog::Accepted)
    {
        if(txt->text() != old)
        {
            txt->setStyleSheet("QLineEdit { background-color: yellow }");
            m_heat2modify = true;
        }
    }
}

void HeaterControllerFrame::onTxtDeltaOff2Clicked()
{
    QLineEdit *txt = static_cast<QLineEdit*>(sender());
    QString old = txt->text();
    InputDigitDialog dlg(txt, this);
    dlg.setTitle("Дельта ВЫКЛ. Отопитель 2");
    int result = dlg.exec();
    if(result==QDialog::Rejected)
    {
        txt->setText(old);
    }
    else if(result==QDialog::Accepted)
    {
        if(txt->text() != old)
        {
            txt->setStyleSheet("QLineEdit { background-color: yellow }");
            m_heat2modify = true;
        }
    }
}


void HeaterControllerFrame::on_btnApply_clicked()
{
    m_heaters[0].Correction = ui->txtCorrection1->text().toFloat();
    ui->txtCorrection1->setStyleSheet("");

    m_heaters[0].DeltaOn = ui->txtDeltaOn1->text().toFloat();
    ui->txtDeltaOn1->setStyleSheet("");

    m_heaters[0].DeltaOff = ui->txtDeltaOff1->text().toFloat();
    ui->txtDeltaOff1->setStyleSheet("");

    m_heaters[0].ControlZone = ui->cboControlZone1->currentIndex();


    m_heaters[1].Correction = ui->txtCorrection2->text().toFloat();
    ui->txtCorrection2->setStyleSheet("");

    m_heaters[1].DeltaOn = ui->txtDeltaOn2->text().toFloat();
    ui->txtDeltaOn2->setStyleSheet("");

    m_heaters[1].DeltaOff = ui->txtDeltaOff2->text().toFloat();
    ui->txtDeltaOff2->setStyleSheet("");

    m_heaters[1].ControlZone = ui->cboControlZone2->currentIndex();


    m_heaterService->UpdateHeaterParams(m_heaters);
}

void HeaterControllerFrame::updateTimeout()
{
    m_heaterService->GetHeaterStates();
}



void HeaterControllerFrame::closeEvent(QCloseEvent *event)
{
    Q_UNUSED(event)
    disconnect(timerConnection);
}

void HeaterControllerFrame::showEvent(QShowEvent *event)
{
    Q_UNUSED(event)

    if(!TimerPool::instance()->getUpdateTimer()->isActive())
        TimerPool::instance()->getUpdateTimer()->start(1000);
    timerConnection = connect(TimerPool::instance()->getUpdateTimer(), &QTimer::timeout, this, &HeaterControllerFrame::updateTimeout);
}
