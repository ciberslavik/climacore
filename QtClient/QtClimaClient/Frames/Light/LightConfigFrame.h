#pragma once

#include <QWidget>

namespace Ui {
    class LightConfigFrame;
}

class LightConfigFrame : public QWidget
{
    Q_OBJECT

public:
    explicit LightConfigFrame(QWidget *parent = nullptr);
    ~LightConfigFrame();

private:
    Ui::LightConfigFrame *ui;
};

