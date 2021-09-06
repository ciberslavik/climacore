
#include <Models/FanState.h>
#pragma once

#include <QWidget>
#include <QMap>
#include "Controls/FanWidget.h"
#include "Frames/FrameBase.h"
#include "Services/FrameManager.h"
#include "Frames/Graphs/SelectProfileFrame.h"
#include "Network/GenericServices/VentilationService.h"

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
    void on_btnSelectProfile_clicked();
    void onProfileSelectorComplete(ProfileInfo profileInfo);

    void onFanStatesReceived(QList<FanState> states);

    void onFanStateChanged(FanStateEnum_t newState);
    void onFanModeChanged(FanMode_t newMode);
    void on_btnConfigure_clicked();

private:
    Ui::VentilationOverviewFrame *ui;
    SelectProfileFrame *m_ProfileSelector;

    QMap<QString, FanWidget*> m_fanWidgets;
    QMap<QString, FanState*> m_fanStates;

    VentilationService *m_ventService;


    void createFanWidgets();
    void removeFanWidgets();
};

