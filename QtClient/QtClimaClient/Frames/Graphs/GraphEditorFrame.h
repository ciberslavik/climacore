#pragma once

#include <QWidget>

namespace Ui {
class GraphEditorFrame;
}

class GraphEditorFrame : public QWidget
{
    Q_OBJECT

public:
    explicit GraphEditorFrame(QWidget *parent = nullptr);
    ~GraphEditorFrame();

private:
    Ui::GraphEditorFrame *ui;
};

