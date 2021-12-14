#pragma once

#include <QWidget>

#include <Frames/FrameBase.h>

namespace Ui {
    class VentilationPWMConfigFrame;
}

class VentilationPWMConfigFrame : public FrameBase
{
    Q_OBJECT

public:
    explicit VentilationPWMConfigFrame(QWidget *parent = nullptr);
    ~VentilationPWMConfigFrame();

private:
    Ui::VentilationPWMConfigFrame *ui;

    // FrameBase interface
public:
    virtual QString getFrameName() override;
};

