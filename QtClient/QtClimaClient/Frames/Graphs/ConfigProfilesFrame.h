#pragma once

#include "SelectProfileFrame.h"

#include <QWidget>

#include <Network/GenericServices/SchedulerControlService.h>

namespace Ui {
class ConfigProfilesFrame;
}

class ConfigProfilesFrame : public FrameBase
{
    Q_OBJECT

public:
    explicit ConfigProfilesFrame(QWidget *parent = nullptr);
    ~ConfigProfilesFrame();

private slots:
    void on_btnReturn_clicked();
    void onSchedulerInfoReceived(SchedulerProfilesInfo info);

    void on_btnSelectTempGraph_clicked();

    void on_btnSelectVentGraph_clicked();

    void on_btnSelectValveGraph_clicked();

    void on_btnSelectMineGraph_clicked();

    void onProfileSelected(ProfileInfo info);
private:
    Ui::ConfigProfilesFrame *ui;
    SchedulerControlService *m_schedService;
    int m_currentProfile;
    SelectProfileFrame *m_selector;

    SchedulerProfilesInfo m_profilesInfo;
    // FrameBase interface
public:
    QString getFrameName() override{return "ConfigProfilesFrame";}
};

