#pragma once

#include <QWidget>

namespace Ui {
class VentilationOverviewFrame;
}

class VentilationOverviewFrame : public QWidget
{
    Q_OBJECT

public:
    explicit VentilationOverviewFrame(QWidget *parent = nullptr);
    ~VentilationOverviewFrame();

private:
    Ui::VentilationOverviewFrame *ui;
};

