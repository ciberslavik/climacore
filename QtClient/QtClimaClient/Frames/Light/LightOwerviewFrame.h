#pragma once

#include "Frames/FrameBase.h"

#include <QWidget>

#include <Models/Timers/LightTimerProfile.h>
#include "Network/GenericServices/LightControllerService.h"

namespace Ui {
    class LightOwerviewFrame;
}

class LightOwerviewFrame : public FrameBase
{
    Q_OBJECT

public:
    explicit LightOwerviewFrame(QWidget *parent = nullptr);
    ~LightOwerviewFrame();
    void setLightProfile(const LightTimerProfile &profile);
private:
    Ui::LightOwerviewFrame *ui;
    LightTimerProfile m_profile;
    LightControllerService *m_lightController;
    // FrameBase interface
public:
    virtual QString getFrameName() override;
private slots:
    void on_btnReturn_clicked();
    void on_btnLightConfig_clicked();
};

