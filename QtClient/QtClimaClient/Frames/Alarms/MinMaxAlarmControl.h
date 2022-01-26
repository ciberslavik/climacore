#pragma once

#include <QWidget>

namespace Ui {
    class MinMaxAlarmControl;
}

class MinMaxAlarmControl : public QWidget
{
    Q_OBJECT

public:
    explicit MinMaxAlarmControl(QWidget *parent = nullptr);
    ~MinMaxAlarmControl();

private:
    Ui::MinMaxAlarmControl *ui;

    QRect m_borderRect;
    // QWidget interface
protected:
    virtual void paintEvent(QPaintEvent *event) override;
    virtual void resizeEvent(QResizeEvent *event) override;
};

