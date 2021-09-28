#ifndef FANSTATESWITCH_H
#define FANSTATESWITCH_H
#include "Models/FanControlsEnums.h"

#include <QDialog>
#include <QWidget>
#include <QDebug>

namespace Ui {
    class FanModeSwitch;
}

class FanModeSwitch : public QDialog
{
    Q_OBJECT

public:
    explicit FanModeSwitch(QWidget *parent = nullptr);
    ~FanModeSwitch();
    void setTitle(const QString &title);
    void setFanMode(FanModeEnum mode);
    FanModeEnum fanMode();
    void setFanState(FanStateEnum_t state);
    FanStateEnum_t fanState(){return m_fanState;}
signals:
    void acceptMode();
    void cancelEdit();
    void fanStateChanged(FanStateEnum_t state);
    void fanModeChanged(FanMode_t mode);
private slots:

    void on_btnAccept_clicked();

    void on_btnCancel_clicked();

    void on_btnOn_clicked();

    void on_btnOff_clicked();

    void on_btnAuto_toggled(bool checked);

    void on_btnManual_toggled(bool checked);

    void on_btnDisable_toggled(bool checked);

protected:
    void paintEvent(QPaintEvent *event) override;
private:
    Ui::FanModeSwitch *ui;

    FanModeEnum m_fanMode;
    FanStateEnum_t m_fanState;

};

#endif // FANSTATESWITCH_H
