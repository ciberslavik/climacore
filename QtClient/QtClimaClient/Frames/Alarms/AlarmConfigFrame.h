#pragma once

#include <QWidget>

namespace Ui {
    class AlarmConfigFrame;
}

class AlarmConfigFrame : public QWidget
{
    Q_OBJECT

public:
    explicit AlarmConfigFrame(QWidget *parent = nullptr);
    ~AlarmConfigFrame();

private:
    Ui::AlarmConfigFrame *ui;
};

