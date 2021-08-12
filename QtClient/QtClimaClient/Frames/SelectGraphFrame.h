#pragma once

#include "FrameBase.h"

#include <QWidget>

namespace Ui {
class SelectGraphFrame;
}

class SelectGraphFrame : public FrameBase
{
    Q_OBJECT

public:
    explicit SelectGraphFrame(QWidget *parent = nullptr);
    ~SelectGraphFrame();

private:
    Ui::SelectGraphFrame *ui;

    // FrameBase interface
public:
    QString getFrameName() override;
};

