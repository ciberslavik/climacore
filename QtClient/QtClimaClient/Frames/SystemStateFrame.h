#pragma once

#include "FrameBase.h"

#include <QWidget>

#include <Models/SystemState.h>

namespace Ui {
class SystemStateFrame;
}

class SystemStateFrame : public FrameBase
{
    Q_OBJECT

public:
    explicit SystemStateFrame(SystemState *systemState, QWidget *parent = nullptr);
    ~SystemStateFrame();
    QString getFrameName() override
    {
        return "SystemStateFrame";
    }
private slots:
    void onSystemStateUpdate();
private:
    Ui::SystemStateFrame *ui;
    SystemState *m_SystemState;
};

