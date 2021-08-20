#pragma once

#include <QWidget>
#include <Frames/FrameBase.h>
#include <ApplicationWorker.h>
#include <Network/GenericServices/GraphService.h>
#include <Network/GenericServices/SensorsService.h>
#include <Network/GenericServices/SystemStatusService.h>
#include <TimerPool.h>

namespace Ui {
class TemperatureOwerviewFrame;
}

class TemperatureOwerviewFrame :  public FrameBase
{
    Q_OBJECT

public:
    explicit TemperatureOwerviewFrame(QWidget *parent = nullptr);
    ~TemperatureOwerviewFrame();

private slots:
    void on_btnConfigTemp_clicked();
    void onUpdateTimeout();
    void onTempStateRecv(TemperatureStateResponse *response);
    void on_btnReturn_clicked();
    void onCorrectionClicked();

private:
    Ui::TemperatureOwerviewFrame *ui;

    // FrameBase interface
public:
    QString getFrameName() override;
    SystemStatusService *m_systemStatus;
    QTimer *m_updateTimer;


    // QWidget interface
protected:
    void closeEvent(QCloseEvent *event) override;
    void showEvent(QShowEvent *event) override;
};

