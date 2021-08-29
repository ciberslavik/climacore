#pragma once

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
private slots:
    void ProfileInfosReceived(QList<ProfileInfo> *infos);
    void TemperatureGraphReceived(ValueByDayProfile *profile);

    void on_ProfileEditorCompleted();
    void on_ProfileEditorCanceled();

    void on_btnReturn_clicked();
    void on_btnUp_clicked();
    void on_btnDown_clicked();
    void on_btnAdd_clicked();
    void on_btnEdit_clicked();
private:
    Ui::SelectProfileFrame *ui;

    ProfileInfoModel *m_infoModel;
    QItemSelectionModel *m_selection;

    void selectRow(int row);
    void loadGraph(const QString &key);
    void loadTemperatureGraph(const QString &key);
    ProfileType m_profileType;
    bool m_needEdit = false;
    GraphService *m_graphService;

    ValueByDayProfile *m_curTempProfile;
    MinMaxByDayProfile *m_curVentProfile;

};

