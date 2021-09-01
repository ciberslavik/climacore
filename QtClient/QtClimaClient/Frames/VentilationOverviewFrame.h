
#include <Models/FanState.h>
#pragma once

#include <QWidget>
#include <QMap>
#include "Controls/FanWidget.h"
#include "Frames/FrameBase.h"
#include "Services/FrameManager.h"
#include "Frames/Graphs/SelectProfileFrame.h"

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
private:
    Ui::VentilationOverviewFrame *ui;
    SelectProfileFrame *m_ProfileSelector;

    QMap<QString, FanWidget*> m_fanWidgets;
    QMap<QString, FanState*> m_fanStates;
};

