#pragma once

#include <QWidget>

#include <Frames/FrameBase.h>

namespace Ui {
    class EngineMenuFrame;
}

class EngineMenuFrame : public FrameBase
{
    Q_OBJECT

public:
    explicit EngineMenuFrame(QWidget *parent = nullptr);
    ~EngineMenuFrame();

private:
    Ui::EngineMenuFrame *ui;

    // FrameBase interface
public:
    virtual QString getFrameName() override;
private slots:
    void on_btnIOOverview_clicked();
    void on_btnReturn_clicked();
};

