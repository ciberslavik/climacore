#pragma once

#include "FrameBase.h"

#include <QWidget>

#include <Models/Graphs/ProfileInfo.h>
#include <Network/GenericServices/HeaterControllerService.h>
#include <Network/GenericServices/SchedulerControlService.h>
#include <Frames/Graphs/SelectProfileFrame.h>

namespace Ui {
class TemperatureConfigFrame;
}

class TemperatureConfigFrame : public FrameBase
{
    Q_OBJECT

public:
    explicit TemperatureConfigFrame(QWidget *parent = nullptr);
    ~TemperatureConfigFrame();

private slots:
    void on_btnReturn_clicked();
    void onProfileSelected(ProfileInfo profileInfo);
    void on_btnSelectGraph_clicked();

    void on_btnHeater1_clicked();

    void on_btnHeater2_clicked();
    void onTxtClicked();

    void onHeater1StateChanged(bool isRunning);
    void onHeater1ModeChanged(bool isManual);

    void onHeater2StateChanged(bool isRunning);
    void onHeater2ModeChanged(bool isManual);

    void onHeaterStateReceived(HeaterState state);
    void onHeaterStateListReceived(QList<HeaterState> states);
private:
    Ui::TemperatureConfigFrame *ui;


    // FrameBase interface
public:
    QString getFrameName() override;
    SelectProfileFrame *m_ProfileSelector;

    HeaterControllerService *m_heaterService;
    SchedulerControlService *m_scheduler;
    QList<HeaterState> m_states;
};

