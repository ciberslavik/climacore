#pragma once

#include "FrameBase.h"

#include <QWidget>

namespace Ui {
class VentelationStateFrame;
}

class VentelationStateFrame : public FrameBase
{
    Q_OBJECT

public:
    explicit VentelationStateFrame(QWidget *parent = nullptr);
    ~VentelationStateFrame();
    QString getFrameName() override
    {
        return "VentelationStateFrame";
    }
private:
    Ui::VentelationStateFrame *ui;
};

