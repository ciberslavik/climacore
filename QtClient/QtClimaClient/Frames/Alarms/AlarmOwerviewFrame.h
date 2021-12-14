#pragma once

#include <QWidget>

#include <Frames/FrameBase.h>

namespace Ui {
    class AlarmOwerviewFrame;
}

class AlarmOwerviewFrame : public FrameBase
{
    Q_OBJECT

public:
    explicit AlarmOwerviewFrame(QWidget *parent = nullptr);
    ~AlarmOwerviewFrame();

private:
    Ui::AlarmOwerviewFrame *ui;

    // FrameBase interface
public:
    virtual QString getFrameName() override;
private slots:
    void on_btnShowAlarmConfig_clicked();
    void on_btnReturn_clicked();
};

