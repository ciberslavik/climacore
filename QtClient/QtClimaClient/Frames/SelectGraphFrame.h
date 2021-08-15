#pragma once

#include "FrameBase.h"

#include <QWidget>

#include <Models/Graphs/GraphInfo.h>

namespace Ui {
class SelectGraphFrame;
}

class SelectGraphFrame : public FrameBase
{
    Q_OBJECT

public:
    explicit SelectGraphFrame(QList<GraphInfo> *infos, QWidget *parent = nullptr);
    ~SelectGraphFrame();
    QString getFrameName() override;


private:
    Ui::SelectGraphFrame *ui;


    QList<GraphInfo> *m_infos;
};

