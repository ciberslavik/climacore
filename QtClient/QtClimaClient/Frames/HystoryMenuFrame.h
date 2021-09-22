#pragma once

#include <QWidget>
#include "FrameBase.h"
namespace Ui {
    class HystoryMenuFrame;
}

class HystoryMenuFrame : public FrameBase
{
    Q_OBJECT

public:
    explicit HystoryMenuFrame(QWidget *parent = nullptr);
    ~HystoryMenuFrame();

private:
    Ui::HystoryMenuFrame *ui;
};

