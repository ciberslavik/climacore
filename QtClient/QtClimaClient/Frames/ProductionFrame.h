#pragma once

#include "FrameBase.h"
#include "Network/GenericServices/ProductionService.h"
#include "Frames/Dialogs/LivestockOperationDialog.h"
#include "ApplicationWorker.h"
#include <QWidget>

namespace Ui {
class ProductionFrame;
}

class ProductionFrame : public FrameBase
{
    Q_OBJECT

public:
    explicit ProductionFrame(QWidget *parent = nullptr);
    ~ProductionFrame();

private:
    Ui::ProductionFrame *ui;
    ProductionService *m_prodService;
    // FrameBase interface
public:
    QString getFrameName(){return "ProductionFrame";}
private slots:
    void on_btnPlending_clicked();
    void on_btnKill_clicked();
    void on_btnDeath_clicked();
    void on_btnRefraction_clicked();
    void on_btnPreparing_clicked();
    void on_btnStartGrowing_clicked();
    void on_btnEndGrowing_clicked();
    void on_btnReturn_clicked();

    void ProductionStopped(int state);
    void PreparingStarted(int state);
    void ProductionStarted(int state);
    void onProductionStateChanged(int state);

    // QWidget interface
protected:
    void showEvent(QShowEvent *event) override;
};

