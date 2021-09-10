#include "ProductionFrame.h"
#include "ui_ProductionFrame.h"

#include <Services/FrameManager.h>

ProductionFrame::ProductionFrame(QWidget *parent) :
    FrameBase(parent),
    ui(new Ui::ProductionFrame)
{
    ui->setupUi(this);
    INetworkService *service = ApplicationWorker::Instance()->GetNetworkService("ProductionService");
    if(service != nullptr)
    {
        m_prodService = dynamic_cast<ProductionService*>(service);
    }

    service = ApplicationWorker::Instance()->GetNetworkService("LivestockService");
    if(service != nullptr)
    {
        m_liveService = dynamic_cast<LivestockService*>(service);
    }
    connect(m_prodService, &ProductionService::PreparingStarted, this, &ProductionFrame::PreparingStarted);
    connect(m_prodService, &ProductionService::ProductionStopped, this, &ProductionFrame::ProductionStopped);
    connect(m_prodService, &ProductionService::ProductionStarted, this, &ProductionFrame::PreparingStarted);
    connect(m_prodService, &ProductionService::ProductionStateChanged,this, &ProductionFrame::onProductionStateChanged);

    connect(m_liveService, &LivestockService::LivestockUpdated, this, &ProductionFrame::LivestockStateChanged);
    ui->lblDead->setText("0");
    ui->lblKilled->setText("0");
    ui->lblTotalPlending->setText("0");

}

ProductionFrame::~ProductionFrame()
{
    delete ui;
}

void ProductionFrame::on_btnPlending_clicked()
{
    LivestockOperationDialog *dlg = new LivestockOperationDialog(FrameManager::instance()->MainWindow());
    dlg->setTitle("Посадка");

    if(dlg->exec()==QDialog::Accepted)
    {

    }
    dlg->deleteLater();
}


void ProductionFrame::on_btnKill_clicked()
{
    LivestockOperationDialog *dlg = new LivestockOperationDialog(FrameManager::instance()->MainWindow());
    dlg->setTitle("Убой");

    if(dlg->exec()==QDialog::Accepted)
    {

    }
    dlg->deleteLater();
}


void ProductionFrame::on_btnDeath_clicked()
{
    LivestockOperationDialog *dlg = new LivestockOperationDialog(FrameManager::instance()->MainWindow());
    dlg->setTitle("Падежъ");

    if(dlg->exec()==QDialog::Accepted)
    {

    }
    dlg->deleteLater();
}


void ProductionFrame::on_btnRefraction_clicked()
{
    LivestockOperationDialog *dlg = new LivestockOperationDialog(FrameManager::instance()->MainWindow());
    dlg->setTitle("Разрядка");

    if(dlg->exec()==QDialog::Accepted)
    {

    }
    dlg->deleteLater();
}


void ProductionFrame::on_btnPreparing_clicked()
{
    QLineEdit *txt = new QLineEdit();
    InputDigitDialog dlg(txt, FrameManager::instance()->MainWindow());

    if(dlg.exec()==QDialog::Accepted)
    {
        float val = txt->text().toFloat();
        m_prodService->StartPreparing(val);
    }
}


void ProductionFrame::on_btnStartGrowing_clicked()
{
    m_prodService->StartProduction();
}


void ProductionFrame::on_btnEndGrowing_clicked()
{
    m_prodService->StopProduction();
}


void ProductionFrame::on_btnReturn_clicked()
{
    FrameManager::instance()->PreviousFrame();
}

void ProductionFrame::ProductionStopped(int state)
{

}

void ProductionFrame::PreparingStarted(int state)
{

}

void ProductionFrame::ProductionStarted(int state)
{

}

void ProductionFrame::onProductionStateChanged(ProductionState state)
{
    switch (state.State) {
    case 0:
        ui->btnDeath->setEnabled(false);
        ui->btnKill->setEnabled(false);
        ui->btnRefraction->setEnabled(false);
        ui->btnPlending->setEnabled(false);
        ui->btnEndGrowing->setEnabled(false);
        ui->btnPreparing->setEnabled(true);
        ui->btnStartGrowing->setEnabled(true);
        break;
    case 1:
        ui->btnDeath->setEnabled(false);
        ui->btnKill->setEnabled(false);
        ui->btnRefraction->setEnabled(false);
        ui->btnPlending->setEnabled(false);
        ui->btnEndGrowing->setEnabled(true);
        ui->btnPreparing->setEnabled(true);
        ui->btnStartGrowing->setEnabled(true);
        break;
    case 2:
        ui->btnDeath->setEnabled(true);
        ui->btnKill->setEnabled(true);
        ui->btnRefraction->setEnabled(true);
        ui->btnPlending->setEnabled(true);
        ui->btnEndGrowing->setEnabled(true);
        ui->btnPreparing->setEnabled(false);
        ui->btnStartGrowing->setEnabled(true);
        break;
    }

    ui->lblStartDate->setText(state.StartDate.toString("dd.MM.yyyy hh:mm"));
}

void ProductionFrame::LivestockStateChanged(LivestockState state)
{
    ui->lblDead->setText(QString::number(state.TotalDeadHeads));
    ui->lblKilled->setText(QString::number(state.TotalKilledHeads));
    ui->lblTotalPlending->setText(QString::number(state.TotalPlantedHeads));

}

void ProductionFrame::showEvent(QShowEvent *event)
{
    m_prodService->GetProductionState();
}

