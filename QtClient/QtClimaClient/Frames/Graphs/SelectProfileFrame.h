#pragma once

#include <Frames/FrameBase.h>
#include <QItemSelectionModel>
#include <QWidget>

#include <Models/Graphs/ProfileInfo.h>
#include <Models/Dialogs/ProfileInfoModel.h>
#include <Network/GenericServices/GraphService.h>
#include <Services/FrameManager.h>
#include <Frames/Graphs/GraphType.h>

namespace Ui {
class SelectProfileFrame;
}

class SelectProfileFrame : public FrameBase
{
    Q_OBJECT

public:
    explicit SelectProfileFrame(QList<ProfileInfo> *infos, GraphType type, QWidget *parent = nullptr);
    ~SelectProfileFrame();
    QString getFrameName() override;
private slots:


    void on_btnReturn_clicked();

    void on_btnUp_clicked();

    void on_btnDown_clicked();

    void on_btnAdd_clicked();

private:
    Ui::SelectProfileFrame *ui;

    ProfileInfoModel *m_infoModel;
    QItemSelectionModel *m_selection;

    void selectRow(int row);
    void loadGraph(const QString &key);
    GraphType m_graphType;
    GraphService *m_graphService;


};

