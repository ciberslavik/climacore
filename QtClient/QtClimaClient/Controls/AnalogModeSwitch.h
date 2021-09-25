#pragma once

#include <QWidget>

namespace Ui {
class AnalogModeSwitch;
}

class AnalogModeSwitch : public QWidget
{
    Q_OBJECT

public:
    explicit AnalogModeSwitch(QWidget *parent = nullptr);
    ~AnalogModeSwitch();
    bool getMode();
    void setMode(const bool &mode);

    float getManualPower();
    void setManualPower(const float &power);

    float getMaxLimit();
    void setMaxLimit(const float &maxLimit);

    float getMinLimit();
    void setMinLimit(const float &minLimit);
signals:
    void AcceptEdit();
    void RejectEdit();
    void ModeChanged(bool isManual);
private slots:
    void on_btnAccept_clicked();

    void on_btnReject_clicked();
    void onTxtClicked();
    void on_sldManualPower_sliderReleased();

    void on_btnAuto_toggled(bool checked);

private:
    Ui::AnalogModeSwitch *ui;
};
