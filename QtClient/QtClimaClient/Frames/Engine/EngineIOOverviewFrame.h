#pragma once

#include "IOOverviewModel.h"

#include <QWidget>

#include <Frames/FrameBase.h>

#include <Network/GenericServices/IONetworkService.h>

namespace Ui {
    class EngineIOOverviewFrame;
}

class EngineIOOverviewFrame : public FrameBase
{
    Q_OBJECT

public:
    explicit EngineIOOverviewFrame(QWidget *parent = nullptr);
    ~EngineIOOverviewFrame();

private:
    Ui::EngineIOOverviewFrame *ui;
    IONetworkService *m_io;
    IOOverviewModel *m_model;
private slots:
    void onPinInfosReceived(const QList<PinInfo> &infos);
    // FrameBase interface
    void on_btnReturn_clicked();

public:
    virtual QString getFrameName() override;
};

