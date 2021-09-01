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

}


void ProductionFrame::on_btnStartGrowing_clicked()
{

}


void ProductionFrame::on_btnEndGrowing_clicked()
{

}


void ProductionFrame::on_btnReturn_clicked()
{
    FrameManager::instance()->PreviousFrame();
}

