#pragma once

#include <Frames/FrameBase.h>
#include <QWidget>

#include <Models/Graphs/GraphInfo.h>
#include <Models/Graphs/SelectGraphModel.h>

#include <Network/GenericServices/GraphService.h>

namespace Ui {
class SelectGraphFrame;
}

class SelectGraphFrame : public FrameBase
{
    Q_OBJECT

public:
    explicit SelectGraphFrame(QString graphType, QWidget *parent = nullptr);
    ~SelectGraphFrame();
    QString getFrameName() override;
private slots:
    void TempInfosResponse(QList<GraphInfo> *infos);

private:
    Ui::SelectGraphFrame *ui;

    QList<GraphInfo> *m_infos;
    GraphService *m_graphService;
    SelectGraphModel *m_graphInfosModel;
};

