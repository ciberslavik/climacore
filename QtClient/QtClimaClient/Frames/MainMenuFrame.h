#pragma once

#include "FrameBase.h"

#include <QWidget>

namespace Ui {
class MainMenuFrame;
}

class MainMenuFrame : public FrameBase
{
    Q_OBJECT

public:
    explicit MainMenuFrame(QWidget *parent = nullptr);
    ~MainMenuFrame();
    QString getFrameName() override
    {
        return "MainMenuFrame";
    }
private slots:
    void on_btnVentilationOverview_clicked();

    void on_btnVentilationConfig_clicked();

    void on_btnReturn_clicked();

    void on_btnTemperature_clicked();


    void on_btnLightConfig_clicked();

private:
    Ui::MainMenuFrame *ui;
};

