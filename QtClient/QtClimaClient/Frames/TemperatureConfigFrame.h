#pragma once

#include "FrameBase.h"

#include <QWidget>

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

    void on_btnSelectGraph_clicked();

private:
    Ui::TemperatureConfigFrame *ui;


    // FrameBase interface
public:
    QString getFrameName() override;
};

