#pragma once

#include "ValveProfileEditorFrame.h"

#include <Frames/FrameBase.h>
#include <QItemSelectionModel>
#include <QWidget>

#include <Models/Graphs/MinMaxByDayProfile.h>
#include <Models/Graphs/ProfileInfo.h>
#include <Models/Dialogs/ProfileInfoModel.h>
#include <Network/GenericServices/GraphService.h>
#include <Services/FrameManager.h>
#include <Frames/Graphs/ProfileType.h>
#include <Frames/Graphs/TempProfileEditorFrame.h>
#include <Frames/Graphs/VentProfileEditorFrame.h>

namespace Ui {
class SelectProfileFrame;
}

class SelectProfileFrame : public FrameBase
{
    Q_OBJECT

public:
    explicit SelectProfileFrame(const ProfileType &profileType, QWidget *parent = nullptr);
    ~SelectProfileFrame();
    QString getFrameName() override;
    void setSelectedKey(const QString &key);

signals:
    void ProfileSelected(ProfileInfo profileInfo);
private slots:

    void ProfileInfosReceived(QList<ProfileInfo> infos);

    void TempGraphReceived(ValueByDayProfile profile);
    void TempProfileCreated();
    void TempProfileUpdated();

    void VentGraphReceived(MinMaxByDayProfile profile);
    void VentProfileCreated();
    void VentProfileUpdated();

    void ValveGraphReceived(ValueByValueProfile profile);
    void ValveProfileCreated();
    void ValveProfileUpdated();


    void onTempProfileEditorCompleted();
    void onVentProfileEditorCompleted();
    void onValveProfileEditorCompleted();


    void on_btnReturn_clicked();
    void on_btnUp_clicked();
    void on_btnDown_clicked();
    void on_btnAdd_clicked();
    void on_btnEdit_clicked();
    void on_btnAccept_clicked();

    void on_btnDelete_clicked();

private:
    Ui::SelectProfileFrame *ui;

    ProfileInfoModel *m_infoModel;
    QItemSelectionModel *m_selection;

    void selectRow(int row);

    void drawTemperatureGraph(ValueByDayProfile *profile);
    void drawVentilationGraph(MinMaxByDayProfile *profile);
    void drawValveGraph(ValueByValueProfile *profile);
    ProfileType m_profileType;
    bool m_needEdit = false;
    GraphService *m_graphService;
    QString m_selectedKey;
    ValueByDayProfile *m_curTempProfile;
    MinMaxByDayProfile *m_curVentProfile;
    ValueByValueProfile *m_currValveProfile;

    TempProfileEditorFrame *m_tempEditor;
    VentProfileEditorFrame *m_ventEditor;
    ValveProfileEditorFrame *m_valveEditor;
};

