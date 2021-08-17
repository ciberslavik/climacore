#pragma once

#include <QWidget>
#include <Frames/FrameBase.h>
#include <ApplicationWorker.h>
#include <Network/GenericServices/GraphService.h>
#include <Network/GenericServices/SensorsService.h>
#include <TimerPool.h>

namespace Ui {
class TemperatureOwerviewFrame;
}

class TemperatureOwerviewFrame :  public FrameBase
{
    Q_OBJECT

public:
    explicit TemperatureOwerviewFrame(QWidget *parent = nullptr);
    ~TemperatureOwerviewFrame();

private slots:
    void on_btnConfigTemp_clicked();

private:
    Ui::TemperatureOwerviewFrame *ui;

    // FrameBase interface
public:
    QString getFrameName() override;
    SensorsService *m_sensors;
    GraphService *m_graphService;
};

