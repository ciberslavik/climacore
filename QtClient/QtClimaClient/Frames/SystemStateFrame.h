#pragma once

#include "FrameBase.h"

#include <QTimer>
#include <QWidget>

#include <Models/SystemState.h>

#include <Network/GenericServices/SensorsService.h>

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
private slots:
    void onSystemStateUpdate(SensorsServiceResponse *data);
    void on_pushButton_3_clicked();
    void onTimerElapsed();
private:
    Ui::SystemStateFrame *ui;
    SensorsService *m_sensorsService;
    QTimer *m_updateTmer;
};

