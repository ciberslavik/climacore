#pragma once

#include <QWidget>
#include <Network/GenericServices/LivestockService.h>
#include "Models/LivestockOperationsModel.h"
#include "FrameBase.h"

namespace Ui {
    class LivestockOperationsFrame;
}

class LivestockOperationsFrame : public FrameBase
{
    Q_OBJECT

public:
    explicit LivestockOperationsFrame(QWidget *parent = nullptr);
    ~LivestockOperationsFrame();

private:
    Ui::LivestockOperationsFrame *ui;
    LivestockOperationsModel *m_model;
    LivestockService *m_liveService;
    // FrameBase interface
public:
    virtual QString getFrameName() override;
private slots:
    void on_btnReturn_clicked();
    void LivestockOpListReceived(const QList<LivestockOperation> &operations);
    // QWidget interface
protected:
    virtual void showEvent(QShowEvent *event) override;
};

