#pragma once

#include <QDialog>
#include "Models/FanControlsEnums.h"
namespace Ui {
class AnalogModeSwitch;
}

class AnalogModeSwitch : public QDialog
{
    Q_OBJECT

public:
    explicit AnalogModeSwitch(QWidget *parent = nullptr);
    ~AnalogModeSwitch();
    bool getMode();
    void setMode(const FanModeEnum &mode);
    void setTitle(const QString &title);
    float getManualPower();
    void setManualPower(const float &power);

    float getMaxLimit();
    void setMaxLimit(const float &maxLimit);

    float getMinLimit();
    void setMinLimit(const float &minLimit);
signals:
    void AcceptEdit();
    void RejectEdit();
    void fanModeChanged(const FanModeEnum &mode);
    void fanStateChanged(const FanStateEnum &state);
    void fanValueChanged(const float &value);

private slots:
    void on_btnAccept_clicked();

    void on_btnReject_clicked();
    void onTxtClicked();
    void on_sldManualPower_sliderReleased();

    void on_btnAuto_toggled(bool checked);

    void on_sldManualPower_valueChanged(int value);

private:
    Ui::AnalogModeSwitch *ui;
};

