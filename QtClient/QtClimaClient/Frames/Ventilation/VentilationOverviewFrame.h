

#pragma once
#include <QTimer>
#include <QWidget>
#include <QMap>
#include "Controls/FanWidget.h"
#include "Frames/FrameBase.h"
#include "Services/FrameManager.h"
#include "Frames/Graphs/SelectProfileFrame.h"
#include "Network/GenericServices/VentilationService.h"
#include "TimerPool.h"

namespace Ui {
class VentilationOverviewFrame;
}

class VentilationOverviewFrame : public FrameBase
{
    Q_OBJECT

public:
    explicit VentilationOverviewFrame(QWidget *parent = nullptr);
    ~VentilationOverviewFrame();



    // QWidget interface
protected:
    virtual void closeEvent(QCloseEvent *event) override;
    virtual void showEvent(QShowEvent *event) override;

    // FrameBase interface
public:
    virtual QString getFrameName() override{return "VentilationOverviewFrame";}
private slots:
    void on_pushButton_clicked();
    void onFanInfosReceived(QMap<QString, FanInfo> infos);

    void onEditFanStateChanged(const QString &fanKey, FanStateEnum_t newState);
    void onEditFanModeChanged(const QString &fanKey, FanMode_t newMode);
    void onEditFanValueChanged(const QString &fanKey, const float &value);
    void onValveStateReceived(bool isManual, float currPos, float setPoint);
    void onMineStateReceived(bool isManual, float currPos, float setPoint);
    void onVentilationStatusReceived(float valvePos,float valveSetpoint,float minePos, float mineSetpoint,float ventilationSp);
    void on_btnConfigure_clicked();

    void onBeginEditFan(const QString &fanKey);
    void onAcceptEditFan(const QString &fanKey);
    void onCancelEditFan(const QString &fanKey);
    void on_btnConfigMine_clicked();

    void on_btnConfigValve_clicked();

    void on_btnControllerConfig_clicked();

    void onUpdateTimer();
    void on_btnVentPWMConfig_clicked();

    void on_btnResetAlarms_clicked();

private:
    Ui::VentilationOverviewFrame *ui;
    SelectProfileFrame *m_ProfileSelector;

    QMap<QString, FanWidget*> m_fanWidgets;
    QMap<QString, FanInfo> m_fanInfos;

    VentilationService *m_ventService;
    bool m_needConfig;
    FanInfo m_editedOld;

    int m_updateCounter;
    QTimer *m_updateTimer;
    void createFanWidgets();
    void removeFanWidgets();
    void updateFanWidgets();
};

