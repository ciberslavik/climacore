#pragma once

#include "Frames/FrameBase.h"
#include "Network/GenericServices/SchedulerControlService.h"

#include <QWidget>

#include <Network/GenericServices/VentilationService.h>

namespace Ui {
class VentControllerConfigFrame;
}

class VentControllerConfigFrame : public FrameBase
{
    Q_OBJECT

public:
    explicit VentControllerConfigFrame(QWidget *parent = nullptr);
    ~VentControllerConfigFrame();
    QString getFrameName() override { return "VentControllerConfigFrame"; }

private:
    Ui::VentControllerConfigFrame *ui;
    SchedulerControlService *m_scheduler;
    VentilationService *m_ventilation;
    FanInfo m_analogFan;
    VentilationParams m_ventParams;
private slots:
    void on_btnReturn_clicked();
    void onTxtClicked();
    void onSchedulerProcessInfoReceived(SchedulerProcessInfo info);
    void onVentilationParamsReceived(VentilationParams params);
    void onVentilationParamsUpdated(VentilationParams params);
    void onFanInfoReceived(FanInfo info);
    void onFanUpdated();
    // QWidget interface
    void on_btnApply_clicked();

protected:
    virtual void showEvent(QShowEvent *event) override;
};

