#pragma once

#include <QDialog>
#include <Models/HeaterState.h>

namespace Ui {
class HeaterConfigDialog;
}

class HeaterConfigDialog : public QDialog
{
    Q_OBJECT

public:
    explicit HeaterConfigDialog(QWidget *parent = nullptr);
    ~HeaterConfigDialog();

    void setState(HeaterState *state);
    HeaterState *getState();
signals:
    void onModeChanged(bool isManual);
    void onStateChanged(bool isRunning);
private slots:
    void on_btnAuto_toggled(bool checked);

    void on_btnManualOn_toggled(bool checked);



    void on_btnAccept_clicked();

    void on_btnCancel_clicked();

    void on_btnManual_clicked();

private:
    Ui::HeaterConfigDialog *ui;
    HeaterState *m_state;
};

