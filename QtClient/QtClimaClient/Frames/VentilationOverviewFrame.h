

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


    void onEditFanStateChanged(const QString &fanKey, FanStateEnum_t newState);
    void onEditFanModeChanged(const QString &fanKey, FanMode_t newMode);

    void onValveStateReceived(bool isManual, float currPos, float setPoint);
    void onMineStateReceived(bool isManual, float currPos, float setPoint);

    void on_btnConfigure_clicked();

    void onBeginEditFan(const QString &fanKey);
    void onAcceptEditFan(const QString &fanKey);
    void onCancelEditFan(const QString &fanKey);
    void on_btnConfigMine_clicked();

    void on_btnConfigValve_clicked();

    void on_btnControllerConfig_clicked();

    void onUpdateTimer();
private:
    Ui::VentilationOverviewFrame *ui;
    SelectProfileFrame *m_ProfileSelector;

    QMap<QString, FanWidget*> m_fanWidgets;
    QMap<QString, FanInfo> m_fanInfos;

    VentilationService *m_ventService;

    FanInfo m_editedOld;

    int m_updateCounter;
    QTimer *m_updateTimer;
    void createFanWidgets();
    void removeFanWidgets();
    void updateFanWidgets();
};

