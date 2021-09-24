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

    // FrameBase interface
public:
    virtual QString getFrameName() override{return "HystoryMenuFrame";}
private slots:
    void on_btnReturn_clicked();
};

