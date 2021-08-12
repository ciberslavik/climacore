#pragma once

#include "FrameBase.h"

#include <QWidget>
#include <Services/FrameManager.h>

namespace Ui {
class VentilationConfigFrame;
}

class VentilationConfigFrame : public FrameBase
{
    Q_OBJECT

public:
    explicit VentilationConfigFrame(QWidget *parent = nullptr);
    ~VentilationConfigFrame();

private:
    Ui::VentilationConfigFrame *ui;

    // FrameBase interface
public:
    QString getFrameName() override;
private slots:
    void on_btnSelectGraph_clicked();
};

