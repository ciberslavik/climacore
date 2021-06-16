#pragma once

#include "FrameBase.h"

#include <QWidget>

namespace Ui {
class MainMenuFrame;
}

class MainMenuFrame : public FrameBase
{
    Q_OBJECT

public:
    explicit MainMenuFrame(QWidget *parent = nullptr);
    ~MainMenuFrame();
    QString getFrameName() override
    {
        return "MainMenuFrame";
    }
private:
    Ui::MainMenuFrame *ui;
};

