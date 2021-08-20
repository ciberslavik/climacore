#pragma once

#include "FrameBase.h"

#include <QWidget>
#include <QDebug>

namespace Ui {
class LightConfigFrame;
}

class LightConfigFrame : public FrameBase
{
    Q_OBJECT

public:
    explicit LightConfigFrame(QWidget *parent = nullptr);
    ~LightConfigFrame();

private:
    Ui::LightConfigFrame *ui;

    // FrameBase interface
public:
    QString getFrameName() override;
private slots:
    void on_btnReturn_clicked();
};

