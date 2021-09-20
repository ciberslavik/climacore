#pragma once

#include "FrameBase.h"

#include <QWidget>

namespace Ui {
class VentControllerConfigFrame;
}

class VentControllerConfigFrame : public FrameBase
{
    Q_OBJECT

public:
    explicit VentControllerConfigFrame(QWidget *parent = nullptr);
    ~VentControllerConfigFrame();

private:
    Ui::VentControllerConfigFrame *ui;

    // FrameBase interface
public:
    QString getFrameName() override { return "VentControllerConfigFrame"; }
private slots:
    void on_btnReturn_clicked();
};

