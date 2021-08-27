#pragma once

#include <QWidget>
#include <Models/Graphs/ValueByDayProfile.h>

namespace Ui {
class GraphEditorFrame;
}

class TempProfileEditorFrame : public QWidget
{
    Q_OBJECT

public:
    explicit TempProfileEditorFrame(ValueByDayProfile *profile, QWidget *parent = nullptr);
    ~TempProfileEditorFrame();

private:
    Ui::GraphEditorFrame *ui;
    ValueByDayProfile *m_profile;
};

