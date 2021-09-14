#pragma once

#include <QDialog>

#include <Network/GenericServices/VentilationService.h>

namespace Ui {
class ConfigValveDialog;
}

class ConfigValveDialog : public QDialog
{
    Q_OBJECT

public:
    enum ValveType:int
    {
        Valve = 0,
        Mine = 1
    };


    explicit ConfigValveDialog(ValveType valveType, QWidget *parent = nullptr);
    ~ConfigValveDialog();
private slots:
    void onServoStateReceived(bool isManual, float currPos, float setPoint);
    void updateState();
    void on_horizontalScrollBar_valueChanged(int value);

    void on_btnAccept_clicked();

    void on_btnReject_clicked();

    void on_btnAuto_pressed();

    void on_btnManual_pressed();

private:
    Ui::ConfigValveDialog *ui;

    QString m_profileKey;

    VentilationService *m_ventService;

    ValveType m_type;
    QTimer *m_updateTimer;
    // QWidget interface
protected:
    void closeEvent(QCloseEvent *event) override;
    void showEvent(QShowEvent *event) override;
};

