#ifndef FANSTATESWITCH_H
#define FANSTATESWITCH_H
#include "Models/FanControlsEnums.h"

#include <QDialog>
#include <QWidget>

namespace Ui {
    class FanModeSwitch;
}

class FanModeSwitch : public QDialog
{
    Q_OBJECT

public:
    explicit FanModeSwitch(QWidget *parent = nullptr);
    ~FanModeSwitch();

    void setFanMode(FanMode mode);
    FanMode fanMode();
    void setFanState(bool state);
    bool fanState(){return m_fanState;}
signals:
    void acceptMode(FanMode mode);
    void cancelEdit();
    void fanStateChanged(bool state);
    void fanModeChanged(FanMode_t mode);
private slots:
    void on_btnDisable_clicked();

    void on_btnManual_clicked();

    void on_btnAuto_clicked();

    void on_btnAccept_clicked();

    void on_btnCancel_clicked();

    void on_btnOn_clicked();

    void on_btnOff_clicked();

protected:
    void paintEvent(QPaintEvent *event) override;
private:
    Ui::FanModeSwitch *ui;

    FanMode m_mode;
    bool m_fanState = false;

    void checkButton();
};

#endif // FANSTATESWITCH_H
