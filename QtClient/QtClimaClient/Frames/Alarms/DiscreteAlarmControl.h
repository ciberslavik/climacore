#pragma once

#include <QWidget>

namespace Ui {
    class DiscreteAlarmControl;
}

class DiscreteAlarmControl : public QWidget
{
    Q_OBJECT

public:
    explicit DiscreteAlarmControl(QWidget *parent = nullptr);
    ~DiscreteAlarmControl();

private:
    Ui::DiscreteAlarmControl *ui;
};

