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
class SelectGraphFrame;
}

class SelectGraphFrame : public FrameBase
{
    Q_OBJECT

public:
    explicit SelectGraphFrame(QList<ProfileInfo> *infos, GraphType type, QWidget *parent = nullptr);
    ~SelectGraphFrame();
    QString getFrameName() override;
private slots:


    void on_btnReturn_clicked();

    void on_btnUp_clicked();

    void on_btnDown_clicked();

    void on_btnAdd_clicked();

private:
    Ui::SelectGraphFrame *ui;

    ProfileInfoModel *m_infoModel;
    QItemSelectionModel *m_selection;

    void selectRow(int row);

    GraphType m_graphType;
    GraphService *m_graphService;
};

