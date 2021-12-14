#pragma once

#include "Frames/FrameBase.h"

#include <QItemSelection>
#include <QWidget>

#include <Models/Timers/LightTimerProfile.h>
#include <Models/Timers/TimerProfileModel.h>

namespace Ui {
    class LightProfileEditorFrame;
}

class LightProfileEditorFrame : public FrameBase
{
    Q_OBJECT

public:
    explicit LightProfileEditorFrame(LightTimerProfile *profile, QWidget *parent = nullptr);
    ~LightProfileEditorFrame();

    virtual QString getFrameName() override;
private:
    Ui::LightProfileEditorFrame *ui;
    LightTimerProfile *m_profile;
    TimerProfileModel *m_infoModel;
signals:
    void EditorAccepted();
    void EditorRejected();
private slots:
    void on_btnReturn_clicked();
    void on_btnAccept_clicked();
    void on_btnAddDay_clicked();
    void on_btnEditDay_clicked();
    void on_btnRemoveDay_clicked();

    void on_btnAddTimer_clicked();
    void on_btnRemoveTimer_clicked();
    void onSelectionChanged(const QItemSelection &selected, const QItemSelection &deselected);
};

