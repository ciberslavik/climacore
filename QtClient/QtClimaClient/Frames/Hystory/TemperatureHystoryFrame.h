#pragma once

#include <QWidget>

namespace Ui {
    class TemperatureHystoryFrame;
}

class TemperatureHystoryFrame : public QWidget
{
    Q_OBJECT

public:
    explicit TemperatureHystoryFrame(QWidget *parent = nullptr);
    ~TemperatureHystoryFrame();

private:
    Ui::TemperatureHystoryFrame *ui;
};

