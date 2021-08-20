#pragma once

#include <QWidget>
#include <Frames/FrameBase.h>
namespace Ui {
class VentilationOverviewFrame;
}

class VentilationOverviewFrame : public FrameBase
{
    Q_OBJECT

public:
    explicit VentilationOverviewFrame(QWidget *parent = nullptr);
    ~VentilationOverviewFrame();

private:
    Ui::VentilationOverviewFrame *ui;

    // QWidget interface
protected:
    virtual void closeEvent(QCloseEvent *event) override;
    virtual void showEvent(QShowEvent *event) override;

    // FrameBase interface
public:
    virtual QString getFrameName() override{return "VentilationOverviewFrame";}
};

