#pragma once

#include "FrameBase.h"

#include <QWidget>

#include <Models/Graphs/ProfileInfo.h>

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

private:
    Ui::TemperatureConfigFrame *ui;


    // FrameBase interface
public:
    QString getFrameName() override;
    SelectProfileFrame *m_ProfileSelector;
};

