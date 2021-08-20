#pragma once

#include "FrameBase.h"

#include <QTimer>
#include <QWidget>

#include <Models/SystemState.h>
#include <Services/FrameManager.h>
#include <Network/GenericServices/SensorsService.h>

#include <Network/GenericServices/SystemStatusService.h>

namespace Ui {
class SystemStateFrame;
}

class SystemStateFrame : public FrameBase
{
    Q_OBJECT

public:
    explicit SystemStateFrame(QWidget *parent = nullptr);
    ~SystemStateFrame();
    QString getFrameName() override
    {
        return "SystemStateFrame";
    }
    void closeEvent(QCloseEvent *event) override;
    void showEvent(QShowEvent *ev)override;
private slots:
    void onClimatStateUpdate(ClimatStatusResponse *data);
    void onTimerElapsed();

    void on_btnMainMenu_clicked();

private:
    Ui::SystemStateFrame *ui;
    SystemStatusService *m_statusService;
    QTimer *m_updateTmer;
};

