#pragma once

#include "Frames/FrameBase.h"

#include <QItemSelectionModel>
#include <QWidget>

#include <Network/GenericServices/LightControllerService.h>
#include "Models/Timers/SelectTimerProfileInfoModel.h"

namespace Ui {
    class SelectTimerProfileFrame;
}

class SelectTimerProfileFrame : public FrameBase
{
    Q_OBJECT
public:
    explicit SelectTimerProfileFrame(QWidget *parent = nullptr);
    ~SelectTimerProfileFrame();
    virtual QString getFrameName() override;
private:
    Ui::SelectTimerProfileFrame *ui;
    LightControllerService *m_lightService;

    LightTimerProfile *m_editProfile = nullptr;
    QList<LightTimerProfileInfo> m_infos;
    SelectTimerProfileInfoModel *m_infosModel;
    bool m_needEditProfile;
private slots:
    void on_btnReturn_clicked();
    void on_btnAddProfile_clicked();
    void on_btnEditProfile_clicked();
    void on_btnRemoveProfile_clicked();
    void on_btnAccept_clicked();
    void onSelectionChanged(const QItemSelection &selected, const QItemSelection &deselected);
    void LightProfileCreated(const LightTimerProfile &profile);
    void LightProfileListReceived(const QList<LightTimerProfileInfo> &infos);
    void LightProfileReceived(const LightTimerProfile &profile);
};

