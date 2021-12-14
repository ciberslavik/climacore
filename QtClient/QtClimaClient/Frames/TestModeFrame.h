#pragma once

#include <QWidget>
#include <Frames/FrameBase.h>
#include <Models/Graphs/ProfileInfo.h>
#include <Models/Timers/TimerProfileModel.h>

namespace Ui {
class TestModeFrame;
}

class TestModeFrame : public FrameBase
{
    Q_OBJECT

public:
    explicit TestModeFrame(QWidget *parent = nullptr);
    ~TestModeFrame();

private slots:
    void on_btnSelectProfileTest_clicked();

    void on_btnSelectGraphTest_clicked();

    void on_pushButton_clicked();

private:
    Ui::TestModeFrame *ui;
    // QWidget interface
protected:
    void closeEvent(QCloseEvent *event) override;
    void showEvent(QShowEvent *event) override;

    // FrameBase interface
public:
    QString getFrameName() override{return "TestModeFrame";}
};

